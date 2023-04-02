// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CsvHelper.Expressions
{
	/// <summary>
	/// Creates objects.
	/// </summary>
	public class ObjectRecordCreator : RecordCreator
	{
		/// <summary>
		/// Initializes a new instance using the given reader.
		/// </summary>
		/// <param name="reader"></param>
		public ObjectRecordCreator(CsvReader reader) : base(reader) { }

		/// <summary>
		/// Creates a <see cref="Delegate"/> of type <see cref="Func{T}"/>
		/// that will create a record of the given type using the current
		/// reader row.
		/// </summary>
		/// <param name="recordType">The record type.</param>
		protected override Delegate CreateCreateRecordDelegate(Type recordType)
		{
			if (Reader.Context.Maps[recordType] == null)
			{
				Reader.Context.Maps.Add(Reader.Context.AutoMap(recordType));
			}

			var map = Reader.Context.Maps[recordType];

			Expression body;

			if (map.ParameterMaps.Count > 0)
			{
				// This is a constructor parameter type.
				var arguments = new List<Expression>();
				ExpressionManager.CreateConstructorArgumentExpressionsForMapping(map, arguments);

				var args = new GetConstructorArgs(map.ClassType);
				body = Expression.New(Reader.Configuration.GetConstructor(args), arguments);
			}
			else
			{
				var assignments = new List<MemberAssignment>();
				ExpressionManager.CreateMemberAssignmentsForMapping(map, assignments);

				if (assignments.Count == 0)
				{
					throw new ReaderException(Reader.Context, $"No members are mapped for type '{recordType.FullName}'.");
				}

				body = ExpressionManager.CreateInstanceAndAssignMembers(recordType, assignments);
			}

			var funcType = typeof(Func<>).MakeGenericType(recordType);

			return Expression.Lambda(funcType, body).Compile();
		}
	}

	public class ObjectRecordCreatorCreator : IRecordCreatorFactory
	{
		/// <summary>
		/// The reader.
		/// </summary>
		protected CsvReader Reader { get; private set; }

		/// <summary>
		/// The expression manager.
		/// </summary>
		protected ExpressionManager ExpressionManager { get; private set; }

		/// <summary>
		/// Initializes a new instance using the given reader.
		/// </summary>
		/// <param name="reader"></param>
		public ObjectRecordCreatorCreator(CsvReader reader)
		{
			Reader = reader;
			ExpressionManager = new ExpressionManager(reader);
		}

		public IRecordCreator<T> GetRecordCreator<T>() => (IRecordCreator<T>)GetRecordCreator(typeof(T));

		/// <summary>
		/// Creates a <see cref="Delegate"/> of type <see cref="Func{T}"/>
		/// that will create a record of the given type using the current
		/// reader row.
		/// </summary>
		/// <param name="recordType">The record type.</param>
		public IRecordCreator GetRecordCreator(Type recordType)
		{
			if (Reader.Context.Maps[recordType] == null)
			{
				Reader.Context.Maps.Add(Reader.Context.AutoMap(recordType));
			}

			var map = Reader.Context.Maps[recordType];

			Expression body;

			if (map.ParameterMaps.Count > 0)
			{
				// This is a constructor parameter type.
				var arguments = new List<Expression>();
				ExpressionManager.CreateConstructorArgumentExpressionsForMapping(map, arguments);

				var args = new GetConstructorArgs(map.ClassType);
				body = Expression.New(Reader.Configuration.GetConstructor(args), arguments);
			}
			else
			{
				var assignments = new List<MemberAssignment>();
				ExpressionManager.CreateMemberAssignmentsForMapping(map, assignments);

				if (assignments.Count == 0)
				{
					throw new ReaderException(Reader.Context, $"No members are mapped for type '{recordType.FullName}'.");
				}

				body = ExpressionManager.CreateInstanceAndAssignMembers(recordType, assignments);
			}

			// TODO body ends up as Func<T> but we want Func<IReaderRow, T>

			Type funcType = typeof(Func<,>).MakeGenericType(typeof(IReaderRow), recordType);

			Delegate createRecord = Expression.Lambda(funcType, body).Compile();

			return (IRecordCreator)Activator.CreateInstance(typeof(DelegateRecordCreator<>).MakeGenericType(recordType), createRecord);
		}

		public bool CanCreate(Type recordType)
		{
			throw new NotImplementedException();
		}

		private class DelegateRecordCreator<T> : IRecordCreator<T>
		{
			private readonly Func<IReaderRow, T> _createRecord;

			public DelegateRecordCreator(Func<IReaderRow, T> createRecord)
			{
				_createRecord =	createRecord;
			}

			public T CreateRecord(IReaderRow row) => _createRecord(row);

			object IRecordCreator.CreateRecord(IReaderRow row) => _createRecord(row);
		}
	}

}
