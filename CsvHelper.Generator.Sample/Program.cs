using CsvHelper.Configuration.Attributes;

namespace CsvHelper.Generator.Sample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!xx");
		}
	}

	public class MyRecord
	{
		public string? Name { get; set; }
	}
	public class MyRecord2
	{
		public int Id { get; set; }
	}

	[RecordCreator(typeof(MyRecord))]
	[RecordCreator(typeof(MyRecord2))]
	public partial class MyRecordCreator
	{

	}
}
