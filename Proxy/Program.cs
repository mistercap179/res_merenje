using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Proxy se digao");
            Proxy proxy = new Proxy();
            proxy.StartProxy();
            
        }
    }
}
