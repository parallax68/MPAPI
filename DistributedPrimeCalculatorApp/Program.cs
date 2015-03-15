using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MPAPI;

namespace DistributedPrimeCalculatorApp
{
  class Program
  {
    static void Main(string[] args)
    {
      //get the variable parameters needed. This can also come
      //from a configuration file
      string modusOperandi = "";
      while (modusOperandi != "m" && modusOperandi != "s")
      {
        Console.Write("[m]aster or [s]lave > ");
        modusOperandi = Console.ReadLine();
      }
      Console.Write("This nodes port number > ");
      int port = int.Parse(Console.ReadLine());

      //Get the information in the config file
      string regServerAddress = ConfigurationManager.AppSettings["RegistrationServerAddress"];
      int regServerPort = int.Parse(ConfigurationManager.AppSettings["RegistrationServerPort"]);

      using (Node node = new Node())
      {
				if (modusOperandi == "m") {
					Log.Info ("Server IP= " + regServerAddress);
					Log.Info ("Server Port= " + regServerPort.ToString ());
					Log.Info ("Node Port= " + port.ToString ());
					//open the node in master mode and start the main worker
					node.OpenDistributed<MainWorker> (regServerAddress, regServerPort, port);
				} else {
					Log.Info ("Server IP= " + regServerAddress);
					Log.Info ("Server Port= " + regServerPort.ToString ());
					Log.Info ("Node Port= " + port.ToString ());
					//open the node in slave mode
					node.OpenDistributed (regServerAddress, regServerPort, port);
				}
        /* Since the node spawns new workers in separate threads, and as
         * background threads, we have to prevent the main thread from
         * terminating here. */
        Console.ReadLine();
      }
    }
  }
}
