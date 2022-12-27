using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Konekcije.ServerKonekcija konekcija = new Models.Konekcije.ServerKonekcija(
                new string[] { Models.Konekcije.Konekcija.UriServer }, new MerenjeService()
            );
            konekcija.Open();
            Console.ReadLine();
        }
    }

}
