// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace CsvHelper.Expressions
{
	/// <summary>
	/// Creates primitive records.
	/// </summary>
	public class PrimitiveRecordCreatorCreator : IRecordCreatorFactory
	{
		private readonly CsvReader _reader;

		public PrimitiveRecordCreatorCreator(CsvReader reader)
		{
			_reader = reader;
		}

		public bool CanCreate(Type recordType) => recordType.IsPrimitive;

		public IRecordCreator<T> GetRecordCreator<T>()
		{
			var memberMapData = new MemberMapData(null)
			{
				Index = 0,
				TypeConverter = _reader.Context.TypeConverterCache.GetConverter(typeof(T)),
				TypeConverterOptions = TypeConverterOptions.Merge(new TypeConverterOptions { CultureInfo = _reader.Configuration.CultureInfo }, _reader.Context.TypeConverterOptionsCache.GetOptions(typeof(T)))
			};

			return new PrimitiveRecordCreator<T>(memberMapData);
		}

		public IRecordCreator GetRecordCreator(Type recordType)
		{
			var memberMapData = new MemberMapData(null)
			{
				Index = 0,
				TypeConverter = _reader.Context.TypeConverterCache.GetConverter(recordType),
				TypeConverterOptions = TypeConverterOptions.Merge(new TypeConverterOptions { CultureInfo = _reader.Configuration.CultureInfo }, _reader.Context.TypeConverterOptionsCache.GetOptions(recordType))
			};

			return new PrimitiveRecordCreator(memberMapData);
		}

		private class PrimitiveRecordCreator : IRecordCreator
		{
			private readonly MemberMapData _memberMapData;

			public PrimitiveRecordCreator(MemberMapData memberMapData)
			{
				_memberMapData = memberMapData;
			}

			public object CreateRecord(IReaderRow row) => _memberMapData.TypeConverter.ConvertFromString(row[0], row, _memberMapData);
		}

		private sealed class PrimitiveRecordCreator<T> : PrimitiveRecordCreator, IRecordCreator<T>
		{
			public PrimitiveRecordCreator(MemberMapData memberMapData) : base(memberMapData)
			{ }

			T IRecordCreator<T>.CreateRecord(IReaderRow row) => (T)CreateRecord(row);
		}
	}

}
