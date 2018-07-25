using System;
using Xunit;
using AggregateGDPPopulation;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
namespace AggregateGDPPopulation.Tests
{
	public class UnitTest1
	{
		[Fact]

		async public void CompareFile()

		{
			CalculateAggregateGDPPopulation aggregate = new CalculateAggregateGDPPopulation();
			await aggregate.Aggregate(@"../../../../AggregateGDPPopulation/data/datafile.csv");
			Task<string> ActualOutput = FileUtility.ReadFileAsync(CalculateAggregateGDPPopulation.outputFilePath);
			Task<string> ExpectedOutput = FileUtility.ReadFileAsync(@"../../../expected-output.json");
			await Task.WhenAll(ActualOutput, ExpectedOutput);
			Dictionary<string, Data> actual = JsonConvert.DeserializeObject<Dictionary<string, Data>>(ActualOutput.Result);
			Dictionary<string, Data> expected = JsonConvert.DeserializeObject<Dictionary<string, Data>>(ExpectedOutput.Result);
			bool dictionaryIsEqual = (actual.Keys.Count == expected.Keys.Count && actual.Keys.All(k => expected.ContainsKey(k) && actual[k].IsEqual(expected[k])));
			Assert.True(dictionaryIsEqual);

		}

	}
}

