using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedPrimeCalculatorApp
{
	[Serializable]
	public class Batch
	{
		private long _minimum;
		private long _maximum;

		public Batch (long minimum, long maximum)
		{
			_minimum = minimum;
			_maximum = maximum;
		}

		public long Maximum {
			get { return _maximum; }
		}

		public long Minimum {
			get { return _minimum; }
		}
	}
}
