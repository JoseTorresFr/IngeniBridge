﻿using IngeniBridge.Core;
using IngeniBridge.Core.MetaHelper;
using IngeniBridge.Core.Mining;
using IngeniBridge.Core.Serialization;
using IngeniBridge.Core.Service;
using IngeniBridge.Core.StagingData;
using IngeniBridge.Core.Storage;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IngeniBridge.TestServer
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger ( System.Reflection.MethodBase.GetCurrentMethod ().DeclaringType );
        public static string url = "http://demo.ingenibridge.com/";
        static int Main ( string [] args )
        {
            XmlConfigurator.Configure ( LogManager.GetRepository ( Assembly.GetEntryAssembly () ), new FileInfo ( "log4net.config" ) );
            int ret = 0;
            try
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo ( Assembly.GetEntryAssembly ().Location );
                Program.log.Info ( fvi.ProductName + " v" + fvi.FileVersion + " -- " + fvi.LegalCopyright );
                Program.log.Info ( "Starting " + Assembly.GetEntryAssembly ().GetName ().Name + " v" + Assembly.GetEntryAssembly ().GetName ().Version );
                //Console.Write ( "Login => " );
                //string login = Console.ReadLine ();
                //Console.Write ( "Password => " );
                //string password = "";
                //while ( true )
                //{
                //    var key = Console.ReadKey ( true );
                //    if ( key.Key == ConsoleKey.Enter ) break;
                //    password += key.KeyChar;
                //}
                HttpClientHandler handler = new HttpClientHandler () { UseDefaultCredentials = true };
                HttpClient client = new HttpClient ();
                client.BaseAddress = new Uri ( Program.url );
                client.DefaultRequestHeaders.Accept.Add ( new MediaTypeWithQualityHeaderValue ( "application/json" ) );
                //var byteArray = Encoding.ASCII.GetBytes ( login + ":" + password );
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ( "Basic", Convert.ToBase64String ( byteArray ) );
                Program.log.Info ( "Connecting => " + Program.url );
                MethodRest.Launch ( client );
                MethodRestProxy.Launch ( client );
                MethodMapping.Launch ( client );
                CorrelationInfluenceZone.Launch ( client );
            }
            catch ( Exception e )
            {
                Program.log.Error ( "Exception => " + e.GetType () + " = " + e.Message );
                ret = 1;
            }
            Program.log.Info ( "Terminated => " + ret.ToString () );
            return ( ret );
        }
    }
}
