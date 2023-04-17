using System;

namespace CsvHelper.Configuration.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class RecordCreatorAttribute : Attribute
	{
#pragma warning disable IDE0060 // Remove unused parameter
		public RecordCreatorAttribute(Type type)
		{ }
#pragma warning restore IDE0060
	}
}
