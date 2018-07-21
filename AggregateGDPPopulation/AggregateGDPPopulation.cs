using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace AggregateGDPPopulation
{
	public class AggregateGDPPopulation

	{
		string filepath = Environment.CurrentDirectory + @"data/datafile.csv";
		public static string ContinentMapperFilePath = Environment.CurrentDirectory + @"/data";

		public static string Readfile(string filepath)
		{
			string CsvString = "";
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					CsvString = sr.ReadToEnd();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return CsvString;
		}

		public static Dictionary<string, string> ParseJson(string jsonstring) {
			Dictionary<string, string> ContinentMapper = new Dictionary<string,string>();
			ContinentMapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonstring);
			return ContinentMapper;

		}

		public static Dictionary<string, string> aggregate() {
			

			Dictionary<string, string> ContinentMapper = new Dictionary<string, string>();
			ContinentMapper = ParseJson(Readfile(ContinentMapperFilePath));
			Console.WriteLine(ContinentMapper);

			return ContinentMapper;


		}

		
	}
}
