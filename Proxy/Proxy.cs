using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    class Proxy
    {
        List<Task> ProxyTasks = new List<Task>();
        public bool ProxyWorking = false;
        Models.Konekcije.ServerKonekcija KonekcijaProxyAsServer = null;
        Models.Konekcije.KlijentKonekcija KonekcijaProxyAsClient = null;
        IProxyService ProxyService = null;
        public Proxy()
        {}

        public void StartProxy()
        {
            ProxyWorking = true;
            StartProxyServices();
        }

        /// <summary>
        /// Otvara servise proksija
        /// </summary>
        private void StartProxyServices()
        {
            KonekcijaProxyAsClient = new Models.Konekcije.KlijentKonekcija(
                Models.Konekcije.Konekcija.UriServer
            );

            ProxyService = new ProxyService(KonekcijaProxyAsClient.Service);

            KonekcijaProxyAsServer = new Models.Konekcije.ServerKonekcija(
                new string[] { Models.Konekcije.Konekcija.UriProxyServer }, ProxyService);

            ProxyTasks.Add(Task.Factory.StartNew(() => CheckRemovals()));
            KonekcijaProxyAsServer.Open();
            Console.ReadLine();
            ProxyWorking = false;
            Task.WaitAll(ProxyTasks.ToArray());
        }

        /// <summary>
        /// Pokrece task koji ce proveravati da li se treba brisati u lokalnoj kopiji
        /// </summary>
        private void CheckRemovals()
        {
            int secondsToSleep = 10;

            while (ProxyWorking)
            {
                ProxyService.CheckRemovals();
                Task.Delay(secondsToSleep * 1000).Wait();
            }
        }
    }
}
