﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocketApp
{
    class Program
    {


        static void Main(string[] args)
        {

            StartServer();
            

        }

        public static void StartServer()
        {
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            //IPHostEntry host = Dns.GetHostEntry("192.168.0.10");
            IPAddress ipAddress = IPAddress.Parse("192.168.0.10");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);


            try
            {

                // Create a Socket that will use Tcp protocol      
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();
                Console.WriteLine("Waiting for a connection...");
                // Incoming data from the client.    
                string data = null;
                byte[] bytes = new byte[1024];

                
                Console.WriteLine("Waiting for a connection...");
                bytes = new byte[2000000000];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine("if içinde");
                }
                

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
}
