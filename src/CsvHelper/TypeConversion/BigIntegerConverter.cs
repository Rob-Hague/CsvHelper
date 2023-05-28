// Copyright 2009-2022 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CsvHelper.TypeConversion
{
    /// <summary>
    /// Converts a <see cref="BigInteger"/> to and from a <see cref="string"/>.
    /// </summary>
    public class BigIntegerConverter : DefaultTypeConverter, ISpanTypeConverter<BigInteger>
    {
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

		public BigInteger ConvertFromSpan(ReadOnlySpan<char> text, IReaderRow row, MemberMapData memberMapData)
		{
			var numberStyle = memberMapData.TypeConverterOptions.NumberStyles ?? NumberStyles.Integer;

			if (BigInteger.TryParse(text, numberStyle, memberMapData.TypeConverterOptions.CultureInfo, out var bi))
			{
				return bi;
			}

			return (BigInteger)((ISpanTypeConverter)this).ConvertFromSpan(text, row, memberMapData);
		}

		public bool TryFormat(BigInteger value, Span<char> destination, out int charsWritten, IWriterRow row, MemberMapData memberMapData)
		{
			var format = memberMapData.TypeConverterOptions.Formats?.FirstOrDefault();
			return value.TryFormat(destination, out charsWritten, format, memberMapData.TypeConverterOptions.CultureInfo);
		}
	}
}
