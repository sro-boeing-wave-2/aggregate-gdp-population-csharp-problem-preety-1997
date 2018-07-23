using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace AggregateGDPPopulation
{
	public class AggregateGDPPopulation

	{

		async public static Task<string> ReadFile(string filepath)
		{
			string CsvString = "";
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					CsvString = await sr.ReadToEndAsync();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return CsvString;
		}

		async public static Task WriteFile(Dictionary<String, Data> aggregateDictionary)

		{

			string outputDirectory = @"../../../../AggregateGDPPopulation/output";
			string outputFilePath = @"../../../../AggregateGDPPopulation/output/output.json";

			if (!Directory.Exists(outputDirectory))

			{

				Directory.CreateDirectory(outputDirectory);

			}

			string JsonObject = JsonConvert.SerializeObject(aggregateDictionary);

			using (StreamWriter writer = new StreamWriter(outputFilePath))

			{

				await writer.WriteAsync(JsonObject);

			}

		}
		async public static Task Aggregate(string filePath)

		{

			string mapperFilePath = @"../../../../AggregateGDPPopulation/data/continent.json";

			var ReadCsvFile = ReadFile(filePath);

			var ReadContinentFile = ReadFile(mapperFilePath);
			await Task.WhenAll(ReadCsvFile,ReadContinentFile);

			var Mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(ReadContinentFile.Result);

			string[] DataProcessed = ReadCsvFile.Result.Replace("\"", String.Empty).Trim().Split('\n');

			string[] headers = DataProcessed[0].Split(',');

			int indexOfPopulation = Array.IndexOf(headers, "Population (Millions) 2012");

			int indexOfGDP = Array.IndexOf(headers, "GDP Billions (USD) 2012");

			int indexOfCountries = Array.IndexOf(headers, "Country Name");

			Dictionary<string, Data> aggregateDictionary = new Dictionary<string, Data>();

			for (int i = 1; i < ReadCsvFile.Result.Length; i++)

			{

				try

				{

					string[] row = DataProcessed[i].Split(',');

					string countryName = row[indexOfCountries];

					string nameOfContinent = Mapper[countryName];

					float Population = float.Parse(row[indexOfPopulation]);

					float Gdp = float.Parse(row[indexOfGDP]);
					
						try
						{

							aggregateDictionary[nameOfContinent].GDP_2012 += Gdp;
							aggregateDictionary[nameOfContinent].POPULATION_2012 += Population;
						}

						catch
						{
							Data data = new Data() { GDP_2012 = Gdp, POPULATION_2012 = Population };
							aggregateDictionary.Add(nameOfContinent, data);
						}

					
				}

				catch { }

			}
			//Console.WriteLine(aggregateDictionary);
			await WriteFile(aggregateDictionary);
		}
	}
}
