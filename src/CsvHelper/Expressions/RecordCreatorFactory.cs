// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsvHelper.Expressions
{

	public interface IRecordCreator
	{
		object CreateRecord(IReaderRow row);
	}

	public interface IRecordCreator<out T> : IRecordCreator
	{
		new T CreateRecord(IReaderRow row);
	}

	public interface IRecordCreatorFactory
	{
		bool CanCreate(Type recordType);

		IRecordCreator<T> GetRecordCreator<T>();

		IRecordCreator GetRecordCreator(Type recordType);
	}



	/// <summary>
	/// Factory to create record creators.
	/// </summary>
	public class RecordCreatorFactory : IRecordCreatorFactory
	{
		private readonly Stack<IRecordCreatorFactory> _factories;
		private readonly Dictionary<Type, IRecordCreator> _creators;

		private readonly DynamicRecordCreator dynamicRecordCreator;
		private readonly ObjectRecordCreator objectRecordCreator;

		/// <summary>
		/// Initializes a new instance using the given reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public RecordCreatorFactory(CsvReader reader)
		{
			dynamicRecordCreator = new DynamicRecordCreator(reader);
			objectRecordCreator = new ObjectRecordCreator(reader);

			_factories.Push(new PrimitiveRecordCreatorCreator(reader));
		}

		public void RegisterRecordCreator<T>(IRecordCreator<T> recordCreator)
		{
			_creators[typeof(T)] = recordCreator;
		}

		public bool CanCreate(Type recordType)
		{
			if (_creators.ContainsKey(recordType))
			{
				return true;
			}

			foreach (IRecordCreatorFactory factory in _factories)
			{
				if (factory.CanCreate(recordType))
				{
					return true;
				}
			}

			return false;
		}

		public IRecordCreator<T> GetRecordCreator<T>()
		{
			if (_creators.TryGetValue(typeof(T), out IRecordCreator recordCreator))
			{
				if (recordCreator is IRecordCreator<T> genericRecordCreator)
				{
					return genericRecordCreator;
				}

				// Cache a generic wrapper
				genericRecordCreator = new GenericRecordCreatorWrapper<T>(recordCreator);
				_creators[typeof(T)] = genericRecordCreator;
				return genericRecordCreator;
			}

			foreach (IRecordCreatorFactory factory in _factories)
			{
				if (factory.CanCreate(typeof(T)))
				{
					return factory.GetRecordCreator<T>();
				}
			}

			throw new Exception("something");
		}

		private sealed class GenericRecordCreatorWrapper<T> : IRecordCreator<T>
		{
			private readonly IRecordCreator _recordCreator;

			public GenericRecordCreatorWrapper(IRecordCreator recordCreator)
			{
				_recordCreator = recordCreator;
			}

			public T CreateRecord(IReaderRow row) => (T)_recordCreator.CreateRecord(row);

			object IRecordCreator.CreateRecord(IReaderRow row) => _recordCreator.CreateRecord(row);
		}

		public IRecordCreator GetRecordCreator(Type recordType)
		{
			if (_creators.TryGetValue(recordType, out IRecordCreator recordCreator))
			{
				return recordCreator;
			}

			foreach (IRecordCreatorFactory factory in _factories)
			{
				if (factory.CanCreate(recordType))
				{
					return factory.GetRecordCreator(recordType);
				}
			}

			throw new Exception("something");
		}

		/// <summary>
		/// Creates a record creator for the given record type.
		/// </summary>
		/// <param name="recordType">The record type.</param>
		public virtual RecordCreator MakeRecordCreator(Type recordType)
		{
			if (recordType == typeof(object))
			{
				return dynamicRecordCreator;
			}

			if (recordType.GetTypeInfo().IsPrimitive)
			{
				//return primitiveRecordCreator;
			}

			return objectRecordCreator;
		}
	}
}
