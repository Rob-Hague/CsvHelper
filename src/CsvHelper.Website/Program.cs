// Copyright 2009-2024 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Docs;

namespace CsvHelper.Docs
{
	class Program
	{
		static async Task<int> Main(string[] args) => await Bootstrapper
			.Factory
			.CreateDocs(args)
			.AddSourceFiles(@"../../CsvHelper/**/{!.git,!bin,!obj,}/**/*.cs")
			.RunAsync();
	}
}
