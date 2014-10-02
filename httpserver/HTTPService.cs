using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    internal class HttpService
    {
        private readonly TcpClient _connectionSocket;

        public HttpService(TcpClient connectionSocket)
        {
            _connectionSocket = connectionSocket;
        }

        internal void SocketHandler()
        {
            Stream ns = _connectionSocket.GetStream();
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns) {AutoFlush = true};
            _connectionSocket.Close();
        }
    }
}
    

