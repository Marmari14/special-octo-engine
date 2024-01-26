using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace lab1._2_Server
{
    internal class Program
    {
        const int ECHO_PORT = 8080;
        public static int nClients = 0;

        static void Main(string[] args)
        {
            try
            {
                TcpListener clientListener = new TcpListener(ECHO_PORT);

                clientListener.Start();

                Console.WriteLine("Waiting for connections...");

                while (true) {
                    TcpClient client = clientListener.AcceptTcpClient();

                    ClientHandler cHandler = new ClientHandler(client);

                    Thread clientThread = new Thread(new ThreadStart(cHandler.RunClient));
                    clientThread.Start();
                    nClients++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
            }
        }

        public static string GetLocalAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) 
            {   
                if (ip.AddressFamily == AddressFamily.InterNetwork) 
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with Ipv4 address in the system!");
        }
    }
}
