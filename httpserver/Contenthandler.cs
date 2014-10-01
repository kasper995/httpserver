﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Contenthandler
    {
        private string _exstensions;

        public Contenthandler(string exstensions)
        {
            _exstensions = exstensions;
        }

        public string ex()
        {
            if (_exstensions == ".HTML")
            {
                return output + "text/html";
            }
            if (_exstensions == ".doc")
            {
                return output + "application/msword";
            }
            if (_exstensions == ".gif")
            {
                return output + "image/gif";
            }
            if (_exstensions == ".jpg")
            {
                return output + "image/jpeg";
            }
            if (_exstensions == ".pdf")
            {
                return output + "application/pdf";
            }
            if (_exstensions == ".css")
            {
                return output + "text/css";
            }
            if (_exstensions == ".xml")
            {
                return output + "text/xml";
            }
            if (_exstensions == ".jar")
            {
                return output + "application/x-java-archive";
            }

            return output + "application/octet-stream";

        }

    }
}