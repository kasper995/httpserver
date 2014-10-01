using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    internal class HTTPService
    {

        private TcpClient connectionSocket;


        public HTTPService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        internal void SocketHandler()
        {
            Stream ns = connectionSocket.GetStream();
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns) {AutoFlush = true};


            connectionSocket.Close();

        }
    }
}
    

