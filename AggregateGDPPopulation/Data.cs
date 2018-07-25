using System;
using System.Collections.Generic;
using System.Text;

namespace AggregateGDPPopulation
{
	public class Data
	{
		public float GDP_2012 { get; set; }
		public float POPULATION_2012 { get; set; }

		public Data()
		{
			GDP_2012 = 0.0f;
			POPULATION_2012 = 0.0f;
		}

		public Boolean IsEqual(object obj)
		{
			Data data = (Data)obj;
			if (this.GDP_2012 == data.GDP_2012 && this.POPULATION_2012 == data.POPULATION_2012)
				return true;
			return false;
		}

	}
}
