using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AggregateGDPPopulation
{
	public class FileUtility
	{
		async public static Task<string> ReadFileAsync(string filepath)
		{
			string csvString = "";
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					csvString = await sr.ReadToEndAsync();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return csvString;
		}

		async public static Task WriteFileAsync(Dictionary<String, Data> aggregateDictionary, string outputDirectory, string outputFilePath)

		{
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
		public static string ReadFile(string filepath)
		{
			string csvString = "";
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					csvString = sr.ReadToEnd();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return csvString;
		}

		public static void WriteFile(Dictionary<String, Data> aggregateDictionary, string outputDirectory, string outputFilePath)

		{
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
	}
}
