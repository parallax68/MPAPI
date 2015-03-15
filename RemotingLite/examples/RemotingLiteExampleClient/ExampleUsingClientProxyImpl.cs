using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using CommonTypes;
using System.Net.Sockets;

namespace RemotingLiteExampleClient
{
    /// <summary>
    /// This class shows an example of how to use our own client proxy implementation
    /// that inherits from ClientBase.
    /// </summary>
    public class ExampleUsingClientProxyImpl
    {
        public void Start()
        {
            // This is an example that connects to the local host. See MSDN documentation.
            // This will work if you run both client and host on the same machine.
			//IPEndPoint endpoint = new IPEndPoint(getMyFirstIPv4Address(), 8000);
			IPAddress i = IPAddress.Parse ("192.168.0.103");
			IPEndPoint endpoint = new IPEndPoint(i, 8000);

			Console.WriteLine (">> Adresse de Dns.GetHostEntry : " + endpoint.Address);

            // When using our own implementation we can use the "using" construct since ClientBase (and
            // thus our implementation) implements IDisposable.
            using (ClientProxyImpl client = new ClientProxyImpl(endpoint))
            {
                //make a few calls to the host
                Console.WriteLine(client.Sum(2, 3));
                Console.WriteLine(client.Sum(2, 3, 4, 5, 6, 7, 8, 9));
                Console.WriteLine(client.ToUpper("this string used to be lower case"));
                string str = "this was a lower case string";
                client.MakeStringUpperCase(ref str);
                Console.WriteLine(str);
                string lowerCaseString;
                client.MakeStringLowerCase("THIS WAS AN UPPER CASE STRING", out lowerCaseString);
                Console.WriteLine(lowerCaseString);
                Rectangle rect = new Rectangle(30, 40);
                Console.WriteLine(String.Format("Area before call : {0}", rect.Area));
                client.CalculateArea(ref rect);
                Console.WriteLine(String.Format("Area after call : {0}", rect.Area));

                long b;
                client.Square(123, out b);
                Console.WriteLine(string.Format("123 squared is {0}", b));
            }
        }
		static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		static string GetString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
		public static IPAddress getMyFirstIPv4Address()
		{
			IPHostEntry host;
			host = Dns.GetHostEntry(Dns.GetHostName());

			foreach (IPAddress ip in host.AddressList)
			{
				// filter IPv4
				if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return ip;
				}
			}
			return null;
		}

    }
}
