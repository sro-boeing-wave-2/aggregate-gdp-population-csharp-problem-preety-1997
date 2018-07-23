using System;
using Xunit;
using AggregateGDPPopulation;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
       [Fact]

        async public void CompareFile()

        {

           await AggregateGDPPopulation.Aggregate(@"../../../../AggregateGDPPopulation/data/datafile.csv");

            string ActualOutput = File.ReadAllText("../../../expected-output.json");

            string ExpectedOutput = File.ReadAllText(@"../../../../AggregateGDPPopulation/output/output.json");

            Dictionary<string, Data> actual = JsonConvert.DeserializeObject<Dictionary<string, Data>>(ActualOutput);

            Dictionary<string, Data> expected = JsonConvert.DeserializeObject<Dictionary<string,Data>>(ExpectedOutput);

            foreach (var key in actual.Keys)

            {

                if (expected.ContainsKey(key))

                {

                    Assert.Equal(actual[key].GDP_2012, expected[key].GDP_2012);

                    Assert.Equal(actual[key].POPULATION_2012, expected[key].POPULATION_2012);

                }

                else

                {

                    Assert.True(false);

                }

            }

        }
    }
}
