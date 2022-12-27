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

            Models.Konekcije.KlijentKonekcija konekcija = new Models.Konekcije.KlijentKonekcija(Models.Konekcije.Konekcija.UriServer);
            var res = konekcija.Service.GetMerenjes();
            Console.WriteLine("Received:");
            res.ToList().ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
        }
    }
}
