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
        Models.Konekcije.ServerKonekcija<IMerenjeService> KonekcijaProxyAsServer = null;
        Models.Konekcije.KlijentKonekcija<IServerMerenjeService> KonekcijaProxyAsClient = null;
        IProxyService ProxyService = null;
        public Proxy()
        {}

        public void StartProxy()
        {
            ProxyWorking = true;
            ProxyLogger.log.Info("Proxy starting");
            StartProxyServices();
        }

        /// <summary>
        /// Otvara servise proksija
        /// </summary>
        private void StartProxyServices()
        {
            KonekcijaProxyAsClient = new Models.Konekcije.KlijentKonekcija<IServerMerenjeService>(
                Models.Konekcije.Konekcija.UriServer
            );
            ProxyLogger.log.Info("Proxy connection with server established starting");

            ProxyService = new ProxyService(KonekcijaProxyAsClient.Service);

            KonekcijaProxyAsServer = new Models.Konekcije.ServerKonekcija<IMerenjeService>(
                new string[] { Models.Konekcije.Konekcija.UriProxyServer }, ProxyService);


            ProxyTasks.Add(Task.Factory.StartNew(() => CheckRemovals()));
            ProxyLogger.log.Info("Proxy started automatic clearing of local storage.");
            KonekcijaProxyAsServer.Open();
            ProxyLogger.log.Info("Proxy opened connection for clients.");

            Console.ReadLine();
            ProxyWorking = false;
            Task.WaitAll(ProxyTasks.ToArray());
            ProxyLogger.log.Info("Proxy finished.");
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
                ProxyLogger.log.Info("Proxy removed instaces that are older than 1 day");
                Task.Delay(secondsToSleep * 1000).Wait();
            }
        }
    }
}
