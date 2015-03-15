using System;

namespace DistributedPrimeCalculatorApp
{
	public class MessageTypes
	{
		public const int Terminate = 0;		//tells the prime worker to stop
		public const int Start = 1;			//initialize the prime workers
		public const int ReplyBatch = 2;	//the main worker sends a batch of numbers
		public const int RequestBatch = 3;	//the prime worker requests a new batch
		public const int Result = 4;		//the prime worker sends the count back
	}
}
