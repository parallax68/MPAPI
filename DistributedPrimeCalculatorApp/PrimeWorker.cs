using System;
using MPAPI;

namespace DistributedPrimeCalculatorApp
{
	public class PrimeWorker : Worker
	{
		public override void Main ()
		{
			Log.Info ("Prime worker {0} online", Id);

			PrimeFinder primeFinder = new PrimeFinder ();
			Message msg;
			do {
				msg = Receive (); //block and wait for incoming messages
				switch (msg.MessageType) {
				case MessageTypes.Start:
					//request the first batch to process
					Send (msg.SenderAddress, MessageTypes.RequestBatch, null);
					break;

				case MessageTypes.ReplyBatch:
					//We have receives a batch from the main worker. Process it
					Batch batch = (Batch)msg.Content;
					primeFinder.CountPrimes (batch.Minimum, batch.Maximum, msg.SenderAddress);
					//request the next batch to process
					Send (msg.SenderAddress, MessageTypes.RequestBatch, null);
					break;

				default:
					//we do not care about all other messages
					break;
				}
			} while (msg.MessageType != MessageTypes.Terminate);

			Log.Info ("Prime worker {0} terminating", Id);
		}
	}
}
