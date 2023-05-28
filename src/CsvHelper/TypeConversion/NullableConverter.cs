// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using System.Linq;
using CsvHelper.Configuration;

namespace CsvHelper.TypeConversion
{
	/// <summary>
	/// Converts a <see cref="Nullable{T}"/> to and from a <see cref="string"/>.
	/// </summary>
	public class NullableConverter<T> : DefaultTypeConverter, ISpanTypeConverter<T?> where T : struct
	{
		/// <summary>
		/// Gets the type converter for the underlying type.
		/// </summary>
		/// <value>
		/// The type converter.
		/// </value>
		public ITypeConverter UnderlyingTypeConverter { get; private set; }

		/// <summary>
		/// Creates a new <see cref="NullableConverter{T}"/> for the given <see cref="Nullable{T}"/> <see cref="Type"/>.
		/// </summary>
		/// <param name="typeConverterFactory">The type converter factory.</param>
		/// <exception cref="System.ArgumentException">type is not a nullable type.</exception>
		public NullableConverter(TypeConverterCache typeConverterFactory)
		{
			UnderlyingTypeConverter = typeConverterFactory.GetConverter(typeof(T));
		}

		/// <summary>
		/// Converts the string to an object.
		/// </summary>
		/// <param name="text">The string to convert to an object.</param>
		/// <param name="row">The <see cref="IReaderRow"/> for the current record.</param>
		/// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
		/// <returns>The object created from the string.</returns>
		public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
		{
			return ConvertFromSpan(text, row, memberMapData);
		}

		/// <summary>
		/// Converts the object to a string.
		/// </summary>
		/// <param name="value">The object to convert to a string.</param>
		/// <param name="row"></param>
		/// <param name="memberMapData"></param>
		/// <returns>The string representation of the object.</returns>
		public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
		{
			return UnderlyingTypeConverter.ConvertToString(value, row, memberMapData);
		}

		public T? ConvertFromSpan(ReadOnlySpan<char> text, IReaderRow row, MemberMapData memberMapData)
		{
			if (text.IsEmpty)
			{
				return null;
			}

			foreach (var nullValue in memberMapData.TypeConverterOptions.NullValues)
			{
				if (text.SequenceEqual(nullValue))
				{
					return null;
				}
			}

			if (UnderlyingTypeConverter is ISpanTypeConverter<T> spanTypeTConverter)
			{
				return spanTypeTConverter.ConvertFromSpan(text, row, memberMapData);
			}
			else if (UnderlyingTypeConverter is ISpanTypeConverter spanTypeConverter)
			{
				return (T?)spanTypeConverter.ConvertFromSpan(text, row, memberMapData);
			}

			return (T?)UnderlyingTypeConverter.ConvertFromString(text.ToString(), row, memberMapData);
		}

		public bool TryFormat(T? value, Span<char> destination, out int charsWritten, IWriterRow row, MemberMapData memberMapData)
		{
			if (!value.HasValue)
			{
				if (memberMapData.TypeConverterOptions.NullValues.Count > 0)
				{
					string nullValue = memberMapData.TypeConverterOptions.NullValues[0];
					bool success = nullValue.TryCopyTo(destination);
					charsWritten = success ? nullValue.Length : 0;
					return success;
				}

				charsWritten = 0;
				return true;
			}

			T t = value.Value;

			if (UnderlyingTypeConverter is ISpanTypeConverter<T> spanTypeTConverter)
			{
				return spanTypeTConverter.TryFormat(t, destination, out charsWritten, row, memberMapData);
			}
			else if (UnderlyingTypeConverter is ISpanTypeConverter spanTypeConverter)
			{
				return spanTypeConverter.TryFormat(t, destination, out charsWritten, row, memberMapData);
			}

			string? valueString = UnderlyingTypeConverter.ConvertToString(t, row, memberMapData);

			if (valueString.TryCopyTo(destination))
			{
				charsWritten = valueString.Length;
				return true;
			}

			charsWritten = 0;
			return false;
		}
	}
}
