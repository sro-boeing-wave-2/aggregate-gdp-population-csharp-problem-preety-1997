using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
namespace AggregateGDPPopulation
{
	public class AggregateGDPPopulation

	{

		public static string ReadFile(string filepath)
		{
			string CsvString = "";
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					CsvString  = sr.ReadToEnd();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return CsvString;
		}

		 public static void WriteFile(Dictionary<String, Data> aggregateDictionary)

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

                    writer.Write(JsonObject);

                }  

        }
		 public static void Aggregate(string filePath)

        {

            string mapperFilePath = @"../../../../AggregateGDPPopulation/data/continent.json";

            string ReadCsvFile = ReadFile(filePath);

            string ReadContinentFile = ReadFile(mapperFilePath);

            var Mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(ReadContinentFile);

            string[] DataProcessed = ReadCsvFile.Replace("\"", String.Empty).Trim().Split('\n');

            string[] headers = DataProcessed[0].Split(',');

            int indexOfPopulation = Array.IndexOf(headers, "Population (Millions) 2012");

            int indexOfGDP = Array.IndexOf(headers, "GDP Billions (USD) 2012");

            int indexOfCountries = Array.IndexOf(headers, "Country Name");

            Dictionary<string, Data> aggregateDictionary = new Dictionary<string, Data>();

            for (int i = 1; i < ReadCsvFile.Length; i++)

            {

				try

				{

					string[] row = DataProcessed[i].Split(',');

					string countryName = row[indexOfCountries];

					string nameOfContinent = Mapper[countryName];

					float Population = float.Parse(row[indexOfPopulation]);

					float Gdp = float.Parse(row[indexOfGDP]);
					if (nameOfContinent != null)
					{
						if (aggregateDictionary.ContainsKey(nameOfContinent))
						{
							aggregateDictionary[nameOfContinent].GDP_2012 += Gdp;
							aggregateDictionary[nameOfContinent].POPULATION_2012 += Population;
						}
						else
						{
							Data data = new Data() { GDP_2012=Gdp ,POPULATION_2012 =Population};
							aggregateDictionary.Add(nameOfContinent, data);
						}
					}
				}

				catch { }               

            }
			//Console.WriteLine(aggregateDictionary);
		  WriteFile(aggregateDictionary);
	     }
    }
}
