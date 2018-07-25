using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace AggregateGDPPopulation
{
	public class CalculateAggregateGDPPopulation
	{
		public static string mapperFilePath = @"../../../../AggregateGDPPopulation/data/continent.json";
		public static string outputDirectory = @"../../../../AggregateGDPPopulation/output";
		public static string outputFilePath = @"../../../../AggregateGDPPopulation/output/output.json";

		async public Task Aggregate(string filePath)
		{
			Task<string> ReadCsvFile = FileUtility.ReadFileAsync(filePath);
			Task<string> ReadContinentFile = FileUtility.ReadFileAsync(mapperFilePath);
			await Task.WhenAll(ReadCsvFile, ReadContinentFile);
			var Mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(ReadContinentFile.Result);
			string[] DataProcessed = ReadCsvFile.Result.Replace("\"", String.Empty).Trim().Split('\n');
			string[] Headers = DataProcessed[0].Split(',');
			int indexOfPopulation = Array.IndexOf(Headers, "Population (Millions) 2012");
			int indexOfGDP = Array.IndexOf(Headers, "GDP Billions (USD) 2012");
			int indexOfCountries = Array.IndexOf(Headers, "Country Name");
			Dictionary<string, Data> aggregateDictionary = new Dictionary<string, Data>();
			for (int i = 1; i < ReadCsvFile.Result.Length; i++)

			{
				try
				{
					string[] row = DataProcessed[i].Split(',');
					string countryName = row[indexOfCountries];
					if (Mapper[countryName] != null)
					{
						if (!aggregateDictionary.ContainsKey(Mapper[countryName]))
						{
							aggregateDictionary.Add(Mapper[countryName], new Data());
						}
						aggregateDictionary[Mapper[countryName]].GDP_2012 += float.Parse(row[indexOfGDP]);
						aggregateDictionary[Mapper[countryName]].POPULATION_2012 += float.Parse(row[indexOfPopulation]);
					}
				}
				catch (Exception e)
				{
					
				}

			}
			//Console.WriteLine(aggregateDictionary);
			await FileUtility.WriteFileAsync(aggregateDictionary, outputDirectory, outputFilePath);
		}
	}
}
