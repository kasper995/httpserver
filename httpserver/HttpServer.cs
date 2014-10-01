using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;




namespace httpserver
{
    public class HttpServer
    {
        readonly string Line = "\r\n";
        private const string Version = "HTTP/1.0";
        public static readonly int DefaultPort = 8888; // den valgte port
        private const string RootCatalog = "C:\\Temp"; //rootcaltalog som angiver stien til filen
        readonly EventLog _mylog = new EventLog();
        

        readonly TcpListener _serverSocket = new TcpListener(IPAddress.Any, DefaultPort); // laver serveren
        
        public void StartServer()
        {
            
           
            while (true)
            {

                _serverSocket.Start(); //starter serveren
                TcpClient connectionSocket = _serverSocket.AcceptTcpClient(); //sætter sockets til at acceptere clienten
                Stream ns = connectionSocket.GetStream(); //laver en stream
                StreamReader sr = new StreamReader(ns); // laver en streamreader der hedder sr
                StreamWriter sw = new StreamWriter(ns) { AutoFlush = true }; // laver en streamwriter der hedder sw
                
                try
                {
                    string message = sr.ReadLine(); // læser htlm requesten
                    
                    if (message != null) // checker om requesten er tom
                    {
                        string[] words = message.Split(' '); //laver et array med ordene fra message så hvert ord 
                        string name = words[1].Replace("/", "\\"); // bytter alle / ud med \, 
                        string path = RootCatalog + name; // tager rootcatalog og dog samler dem i en ny string doh2
                        string extensions = Path.GetExtension(path);
                        var type = new Contenthandler(extensions);
                        
                       
                        if (File.Exists(path)) // checker om filen findes
                        {
                            
                                sw.Write("{0} 200 Ok\r\n", Version); // sender header til browseren
                                sw.Write("{0}", Line); // lineskift så den ved det er body der kommer som det næste
                                string ftext = File.ReadAllText(path); // samler filnen i en string der hedder ftext hvis den kan læses
                                sw.Write(ftext); // sender stringen til browseren så den kan læses
                            Console.WriteLine("{0} 200 ok",Version);
                                
                           
                            
                        }

                        else if (words[2] != "HTTP/1.1") //checker om http versionen er 1.1
                        {
                            sw.Write("{0} 400 Bad Request\r\n", Version); // sender header til browserensw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                            sw.Write("{0}", Line); // lineskift så den ved det er body der kommer som det næste
                            sw.Write("The Http protocol u have chosen are invalid"); // sender svar tilbage til browseren om at protokolen er forkert
                            Console.WriteLine("{0} 400 Bad Request", Version);
                        }

                        else if (words[0] == "GET" && words[0] == "POST") // hvis det først ord i din request ikke er post eller get så gør
                        {
                            sw.Write("{0} 400 Bad Request\r\n", Version); // sender header til browseren
                            sw.Write("{0}", Line); // lineskift så den ved det er body der kommer som det næste
                            sw.Write("Bad Request did not send a Get or Post request"); // sender svar tilbage til browseren om at protokolen er forkert
                            Console.WriteLine("{0} 400 Bad Request", Version);
                        }
                      
                        else
                        {
                            sw.Write("{0} 404 Not Found\r\n", Version); // sender header til browseren
                            sw.Write("{0}", Line); // lineskift så den ved det er body der kommer som det næste
                            sw.Write("the requested file or homepagde {0} do not exist", path); // sender svar tilbage til browseren om at serveren ikke har det de søger
                            Console.WriteLine("{0} 404 Not Found", Version);
                        }

                        string time = DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(); // laver en string der gemmer tiden lige nu
                        Console.WriteLine(time); // skriver tiden ud til consol
                        Console.WriteLine("someone searched for {0}", path); //skriver ud til consolen 
                        Console.WriteLine(type.Exstensiontype());
                    }
                    

                }

                catch (Exception)
                {
                    throw new Exception();
                }

                finally
                {
                    ns.Close(); // lukker streamen ns ned
                    connectionSocket.Close(); // lukker connectionsocket ned
                    _serverSocket.Stop(); // lukker serversocket ned
                }
            }
        }
    }
}

