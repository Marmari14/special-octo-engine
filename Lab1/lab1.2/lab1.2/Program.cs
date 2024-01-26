using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace lab1._2
{
    internal class Program
    {
        const int ECHO_PORT = 8080;

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) 
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        static void Main(string[] args)
        {
            string t = GetLocalIPAddress();
            Console.WriteLine("Your Name");
            string name = Console.ReadLine();
            Console.WriteLine("---Logged In---");

            try
            {
                TcpClient eClient = new TcpClient("127.0.0.1", ECHO_PORT);

                StreamReader readerStream = new StreamReader(eClient.GetStream());
                NetworkStream writeStream = eClient.GetStream();

                string dataToSend;

                dataToSend = name;
                dataToSend += "\r\n";

                byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                writeStream.Write(data, 0, data.Length);

                while (true) 
                { 
                    Console.Write(name + ":");

                    dataToSend = Console.ReadLine();
                    dataToSend += "\r\n";

                    data = Encoding.ASCII.GetBytes(dataToSend);
                    writeStream.Write(data, 0, data.Length);

                    if (dataToSend.IndexOf("QUIT") > -1)
                        break;

                    string returnData;

                    returnData = readerStream.ReadLine();
                    
                    Console.WriteLine("Server: " + returnData);
                
                }

                eClient.Close();    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
