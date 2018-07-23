using System;
using Xunit;
using AggregateGDPPopulation;
using System.Collections.Generic;
namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			string filepath = @"D:/workspace/aggregate-gdp-population-csharp-problem-preety-1997/AggregateGDPPopulation/data/datafile.csv";
			AggregateGDPPopulation.Aggregate(filepath);
        }
    }
}
