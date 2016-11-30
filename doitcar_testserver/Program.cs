using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace doitcar_testserver{
    class Program{
        static void Main(string[] args){

            TcpListener server = null;
            Console.WriteLine("Starting server...");

            try {
                int port = 9003;
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(ip, port);

                server.Start();
                Console.WriteLine("Server started");

                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.WriteLine("Waiting for the client...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected");
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;

                    while((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Recieved: {0}", data);
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                    client.Close();
                }

            }
            catch(Exception exc)
            {
                Console.WriteLine("Error:\n" + exc.Message.ToString());
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
