#nullable enable
// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System;
using CsvHelper.Configuration;
using System.Linq;

namespace CsvHelper.TypeConversion
{
    /// <summary>
    /// Converts an <see cref="object"/> to and from a <see cref="string"/>.
    /// </summary>
    public class DefaultTypeConverter : ITypeConverter, ISpanTypeConverter
    {
		object? ISpanTypeConverter.ConvertFromSpan(ReadOnlySpan<char> text, IReaderRow row, MemberMapData memberMapData)
		{
			if (memberMapData.UseDefaultOnConversionFailure && memberMapData.IsDefaultSet && memberMapData.Member.MemberType() == memberMapData.Default?.GetType())
			{
				return memberMapData.Default;
			}

			if (!row.Configuration.ExceptionMessagesContainRawData)
			{
				text = $"Hidden because {nameof(IParserConfiguration.ExceptionMessagesContainRawData)} is false.";
			}

			var message =
				$"The conversion cannot be performed.{Environment.NewLine}" +
				$"    Text: '{text}'{Environment.NewLine}" +
				$"    MemberName: {memberMapData.Member?.Name}{Environment.NewLine}" +
				$"    MemberType: {memberMapData.Member?.MemberType().FullName}{Environment.NewLine}" +
				$"    TypeConverter: '{memberMapData.TypeConverter?.GetType().FullName}'";
			throw new TypeConverterException(this, memberMapData, text.ToString(), row.Context, message);
		}

		/// <inheritdoc/>
		public virtual object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
			=> ((ISpanTypeConverter)this).ConvertFromSpan(text, row, memberMapData);

		/// <inheritdoc/>
		public virtual string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
				if (memberMapData.TypeConverterOptions.NullValues.Count > 0)
				{
					return memberMapData.TypeConverterOptions.NullValues[0];
				}

                return string.Empty;
            }

            if (value is IFormattable formattable)
            {
                var format = memberMapData.TypeConverterOptions.Formats?.FirstOrDefault();
                return formattable.ToString(format, memberMapData.TypeConverterOptions.CultureInfo);
            }

            return value?.ToString() ?? string.Empty;
        }

		bool ISpanTypeConverter.TryFormat(object? value, Span<char> destination, out int charsWritten, IWriterRow row, MemberMapData memberMapData)
		{
			bool success;

			if (value == null)
			{
				if (memberMapData.TypeConverterOptions.NullValues.Count > 0)
				{
					string nullValue = memberMapData.TypeConverterOptions.NullValues[0];
					success = nullValue.TryCopyTo(destination);
					charsWritten = success ? nullValue.Length : 0;
					return success;
				}

				charsWritten = 0;
				return true;
			}

			if (value is ISpanFormattable spanFormattable)
			{
				var format = memberMapData.TypeConverterOptions.Formats?.FirstOrDefault();
				return spanFormattable.TryFormat(destination, out charsWritten, format, memberMapData.TypeConverterOptions.CultureInfo);
			}

			string valueString;

			if (value is IFormattable formattable)
			{
				var format = memberMapData.TypeConverterOptions.Formats?.FirstOrDefault();
				valueString = formattable.ToString(format, memberMapData.TypeConverterOptions.CultureInfo);
			}
			else
			{
				valueString = value.ToString() ?? "";
			}

			success = valueString.TryCopyTo(destination);
			charsWritten = success ? valueString.Length : 0;
			return success;
		}
	}
}
