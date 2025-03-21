﻿// Copyright 2009-2024 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvHelper;

/// <summary>
/// Defines methods used to read parsed data
/// from a CSV file row.
/// </summary>
public interface IReaderRow
{
	/// <summary>
	/// Gets the column count of the current row.
	/// This should match <see cref="IParser.Count"/>.
	/// </summary>
	int ColumnCount { get; }

	/// <summary>
	/// Gets the field index the reader is currently on.
	/// </summary>
	int CurrentIndex { get; }

	/// <summary>
	/// Gets the header record.
	/// </summary>
	string[]? HeaderRecord { get; }

	/// <summary>
	/// Gets the parser.
	/// </summary>
	IParser Parser { get; }

	/// <summary>
	/// Gets the reading context.
	/// </summary>
	CsvContext Context { get; }

	/// <summary>
	/// Gets or sets the configuration.
	/// </summary>
	IReaderConfiguration Configuration { get; }

	/// <summary>
	/// Gets the raw field at position (column) index.
	/// </summary>
	/// <param name="index">The zero based index of the field.</param>
	/// <returns>The raw field.</returns>
	string? this[int index] { get; }

	/// <summary>
	/// Gets the raw field at position (column) name.
	/// </summary>
	/// <param name="name">The named index of the field.</param>
	/// <returns>The raw field.</returns>
	string? this[string name] { get; }

	/// <summary>
	/// Gets the raw field at position (column) name.
	/// </summary>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the field.</param>
	/// <returns>The raw field.</returns>
	string? this[string name, int index] { get; }

	/// <summary>
	/// Gets the raw field at position (column) index.
	/// </summary>
	/// <param name="index">The zero based index of the field.</param>
	/// <returns>The raw field.</returns>
	string? GetField(int index);

	/// <summary>
	/// Gets the raw field at position (column) name.
	/// </summary>
	/// <param name="name">The named index of the field.</param>
	/// <returns>The raw field.</returns>
	string? GetField(string name);

	/// <summary>
	/// Gets the raw field at position (column) name and the index
	/// instance of that field. The index is used when there are
	/// multiple columns with the same header name.
	/// </summary>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <returns>The raw field.</returns>
	string? GetField(string name, int index);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="index">The index of the field.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, int index);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, string name);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, string name, int index);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The type of the field.</param>
	/// <param name="index">The index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, int index, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, string name, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> using
	/// the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <returns>The field converted to <paramref name="type"/>.</returns>
	object? GetField(Type type, string name, int index, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T>(int index);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T>(string name);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position 
	/// (column) name and the index instance of that field. The index 
	/// is used when there are multiple columns with the same header name.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <returns></returns>
	T? GetField<T>(string name, int index);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index using
	/// the given <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T>(int index, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name using
	/// the given <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T>(string name, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position 
	/// (column) name and the index instance of that field. The index 
	/// is used when there are multiple columns with the same header name.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T>(string name, int index, ITypeConverter converter);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index using
	/// the given <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T, TConverter>(int index) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name using
	/// the given <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T, TConverter>(string name) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position 
	/// (column) name and the index instance of that field. The index 
	/// is used when there are multiple columns with the same header name.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <returns>The field converted to <typeparamref name="T"/>.</returns>
	T? GetField<T, TConverter>(string name, int index) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position (column) index.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField(Type type, int index, out object? field);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position (column) name.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField(Type type, string name, out object? field);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position 
	/// (column) name and the index instance of that field. The index 
	/// is used when there are multiple columns with the same header name.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField(Type type, string name, int index, out object? field);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position (column) index
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField(Type type, int index, ITypeConverter converter, out object? field);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating if the get was successful.</returns>
	bool TryGetField(Type type, string name, ITypeConverter converter, out object? field);

	/// <summary>
	/// Gets the field converted to the specified <see cref="Type"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the field.</param>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <paramref name="type"/>.</param>
	/// <param name="field">The field converted to <paramref name="type"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField(Type type, string name, int index, ITypeConverter converter, out object? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(int index, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(string name, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position 
	/// (column) name and the index instance of that field. The index 
	/// is used when there are multiple columns with the same header name.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(string name, int index, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index
	/// using the specified <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(int index, ITypeConverter converter, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(string name, ITypeConverter converter, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="converter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T>(string name, int index, ITypeConverter converter, out T? field);

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) index
	/// using the specified <see cref="ITypeConverter" />.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="index">The zero based index of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T, TConverter>(int index, out T? field) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating if the get was successful.</returns>
	bool TryGetField<T, TConverter>(string name, out T? field) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the field converted to <typeparamref name="T"/> at position (column) name
	/// using the specified <see cref="ITypeConverter"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the field.</typeparam>
	/// <typeparam name="TConverter">The <see cref="ITypeConverter"/> used to convert the field to <typeparamref name="T"/>.</typeparam>
	/// <param name="name">The named index of the field.</param>
	/// <param name="index">The zero based index of the instance of the field.</param>
	/// <param name="field">The field converted to <typeparamref name="T"/>.</param>
	/// <returns>A value indicating whether the get was successful.</returns>
	bool TryGetField<T, TConverter>(string name, int index, out T? field) where TConverter : ITypeConverter;

	/// <summary>
	/// Gets the record converted into <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the record.</typeparam>
	/// <returns>The record converted to <typeparamref name="T"/>.</returns>
	T GetRecord<T>();

	/// <summary>
	/// Get the record converted into <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The <see cref="Type"/> of the record.</typeparam>
	/// <param name="anonymousTypeDefinition">The anonymous type definition to use for the record.</param>
	/// <returns>The record converted to <typeparamref name="T"/>.</returns>
	T GetRecord<T>(T anonymousTypeDefinition);

	/// <summary>
	/// Gets the record.
	/// </summary>
	/// <param name="type">The <see cref="Type"/> of the record.</param>
	/// <returns>The record.</returns>
	object GetRecord(Type type);
}
