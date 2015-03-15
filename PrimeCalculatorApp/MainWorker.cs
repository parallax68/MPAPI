using System;
using MPAPI;
using System.Collections.Generic;

namespace PrimeCalculatorApp
{
  public class MainWorker : Worker
  {
    public override void Main()
    {
      Log.Info("Main worker online");

      //set up some preconditions
      long max = 9999999;
      long batchSize = 50000; //the number of numbers to test in each batch
      long currentMinimum = 2;
      long primeCount = 0;

      // Spawn the prime workers. Spawn one for each processor/processing
      // core in the machine.
      List<WorkerAddress> workers = new List<WorkerAddress>();
      int processorCount = Node.GetProcessorCount();
      for (int i = 0; i < processorCount; i++)
      {
        WorkerAddress worker = Spawn<PrimeWorker>();
        workers.Add(worker);
        Monitor(worker); //receive messages when the worker terminates
      }

      //start timing the operation
      DateTime dtStart = DateTime.Now;

      //Initialize all workers
      Broadcast(MessageTypes.Start, null);

      //start listening for requests and results
      Message msg;
      do
      {
        msg = Receive();
        switch (msg.MessageType)
        {
          case MessageTypes.RequestBatch:
            //a worker has requested the next batch
            //check if there are any left to process
            if (currentMinimum <= max)
            {
              long currentMax = currentMinimum + batchSize;
              currentMax = currentMax > max ? max : currentMax;
              Send(msg.SenderAddress, MessageTypes.ReplyBatch, new Batch(currentMinimum, currentMax));
              currentMinimum = currentMax + 1;
            }
            else
              Send(msg.SenderAddress, MessageTypes.Terminate, null);
            break;
          case MessageTypes.Result:
            primeCount += (long)msg.Content;
            break;
          case SystemMessages.WorkerTerminated:
            //a worker terminated - remove it from the list
            workers.Remove(msg.SenderAddress);
            break;
          case SystemMessages.WorkerTerminatedAbnormally:
            //a worker terminated abnormally - remove it from the list
            workers.Remove(msg.SenderAddress);
            Log.Error("Worker {0} terminated abnormally", msg.SenderAddress);
            break;
          default:
            break;
        }
      }
      while (workers.Count > 0); //keep listening until all workers are terminated
      TimeSpan tsTotal = DateTime.Now - dtStart;

      Log.Info("Found {0} primes between 2 and {1}", primeCount, max);
      Log.Info("Computation took {0} seconds", tsTotal.TotalSeconds);
      Log.Info("Main worker terminating");
    }
  }
}
