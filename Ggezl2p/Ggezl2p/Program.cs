using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Security.AccessControl;
using System.Reflection.Metadata;
//////////////////////////////////
using FirefoxEX;


namespace Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OperaGXEX.OperaGX operaGX = new OperaGXEX.OperaGX();
            await operaGX.Start();
            Task.Delay(15000);

            FirefoxEX.Firefox firefox = new FirefoxEX.Firefox();
            await firefox.Start();
            Task.Delay(15000);
        }
    }
}
