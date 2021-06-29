using System;
using System.Threading;

namespace ChatServer
{
    public class ServerManager
    {
        static Thread listenThread;
        static Server server;
        static void Main(string[] args)
        {
            RunServer();
        }

        private static void RunServer() 
        {
            try
            {
                server = new Server();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
