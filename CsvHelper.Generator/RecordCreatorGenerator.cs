using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Immutable;
using System.Threading;
using System.Diagnostics;

namespace CsvHelper.Generator
{
	[Generator(LanguageNames.CSharp)]
	public class RecordCreatorGenerator : IIncrementalGenerator
	{
		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
			IncrementalValueProvider<ImmutableArray<object?>> results =
				context.SyntaxProvider

				.ForAttributeWithMetadataName(
					"CsvHelper.Configuration.Attributes.RecordCreatorAttribute",
					static (node, _) => node is ClassDeclarationSyntax,
					GetRegexMethodDataOrFailureDiagnostic)

				.Collect();

			context.RegisterSourceOutput(results, (context, results) =>
			{
				// dotnet build -c DebugGenerator --no-incremental
#if DEBUGGENERATOR
				if (!Debugger.IsAttached)
				{
					Debugger.Launch();
				}
#endif
				context.AddSource("CsvGenerator.g.cs", "// Hello World! 222x");
			});
		}

		private static object? GetRegexMethodDataOrFailureDiagnostic(
		   GeneratorAttributeSyntaxContext context,
		   CancellationToken cancellationToken)
		{
#if DEBUGGENERATOR
			if (!Debugger.IsAttached)
			{
				Debugger.Launch();
			}
#endif
			ClassDeclarationSyntax cds = (ClassDeclarationSyntax)context.TargetNode;

			string? ns = context.TargetSymbol.ContainingNamespace?.ToDisplayString(
				SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted));

			var x = $"{cds.Identifier}{cds.TypeParameterList}";

			INamedTypeSymbol type = (INamedTypeSymbol)context.Attributes[0].ConstructorArguments[0].Value;

			ImmutableArray<ISymbol> members = type.GetMembers();

			return context.TargetSymbol.Name;
		}
	}
}
