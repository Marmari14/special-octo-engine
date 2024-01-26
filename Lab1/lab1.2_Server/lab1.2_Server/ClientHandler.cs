using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace lab1._2_Server
{
    internal class ClientHandler
    {
        public TcpClient clientSocket;

        public ClientHandler(TcpClient clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public void RunClient() {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writerStream = clientSocket.GetStream();

            string returnData = readerStream.ReadLine();
            string name = returnData;

            Console.WriteLine("Welcome " + name + " to the server");

            while (true) { 
                returnData = readerStream.ReadLine();

                if (returnData.IndexOf("QUIT") > -1)
                {
                    Console.WriteLine("Goodbuy" + name);
                    break;
                }

                Console.WriteLine(name + " : " + returnData);
                returnData += "\r\n";

                byte[] dataWrite = Encoding.ASCII.GetBytes(returnData);
                writerStream.Write(dataWrite, 0, dataWrite.Length);
            }

            clientSocket.Close();
        }
    }
}
