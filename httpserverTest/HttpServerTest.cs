﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using httpserver;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace httpserverTest
{

    [TestClass]

    public class HttpServerTest
    {
        private const string CrLf = "\r\n";

        [TestMethod]
        public void TestGet()
        {
            String line = GetFirstLine("GET /file.txt HTTP/1.0");
            Assert.AreEqual("HTTP/1.0 200 OK", line);

            line = GetFirstLine("GET /fileDoesNotExist.txt HTTP/1.0");
            Assert.AreEqual("HTTP/1.0 404 Not Found", line);
        }


        [TestMethod]
        public void TestGetIllegalRequest()
        {
            String line = GetFirstLine("GET /file.txt HTTP 1.0");
            Assert.AreEqual("HTTP/1.0 400 Bad Request", line);
        }

        [TestMethod]
        public void TestGetIllegalMethodName()
        {
            String line = GetFirstLine("PLET /file.txt HTTP/1.0");
            Assert.AreEqual("HTTP/1.0 400 Bad Request", line);
        }

        [TestMethod]
        public void TestGetIllegalProtocol()
        {
            String line = GetFirstLine("GET /file.txt HTTP/1.2");
            Assert.AreEqual("HTTP/1.0 400 Bad Request", line);
        }

        [TestMethod]
        public void TestMethodNotImplemented()
        {
            String line = GetFirstLine("POST /file.txt HTTP/1.0");
            Assert.AreEqual("HTTP/1.0 200 xxx", line);
        }

        /// <summary>
        /// Private helper method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static String GetFirstLine(String request)
        {
            var th1 = new HttpServer();
            var t1 = new Thread(th1.StartServer);
            
            t1.Start();
            var client = new TcpClient("localhost", HttpServer.DefaultPort);
            NetworkStream networkStream = client.GetStream();

            var toServer = new StreamWriter(networkStream, Encoding.UTF8);
            toServer.Write(request + CrLf);
            toServer.Write(CrLf);
            toServer.Flush();

            var fromServer = new StreamReader(networkStream);
            String firstline = fromServer.ReadLine();
            
            
            toServer.Close();
            fromServer.Close();
            client.Close();
            return firstline;

        }
    }
}
