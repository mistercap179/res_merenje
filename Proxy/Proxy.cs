using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class ProxyInput
    {
        public virtual void ReadLine()
        {
            Console.ReadLine();
        }
    }

    public class Proxy
    {
        public List<Task> ProxyTasks { get; private set; } = new List<Task>();
        public bool ProxyWorking { get; private set; } = false;
        public Models.Konekcije.ServerKonekcija<IMerenjeService> KonekcijaProxyAsServer { get; private set; } = null;
        public Models.Konekcije.IKonekcija<IServerMerenjeService> KonekcijaProxyAsClient { get; private set; } = null;
        public IProxyService ProxyService { get; private set; } = null;
        private ProxyInput Input { get; set; }
        public Proxy(
            Models.Konekcije.ServerKonekcija<IMerenjeService> konekcijaProxyAsServer,
            Models.Konekcije.IKonekcija<IServerMerenjeService> konekcijaProxyAsClient,
            ProxyInput proxyInput
            )
        {
            KonekcijaProxyAsServer = konekcijaProxyAsServer;
            KonekcijaProxyAsClient = konekcijaProxyAsClient;
            ProxyService = new ProxyService(KonekcijaProxyAsClient.Service);
            Input = proxyInput;
        }

        public void StartProxy()
        {
            ProxyWorking = true;
        }

        /// <summary>
        /// Otvara servise proksija
        /// </summary>
        public void StartProxyServices()
        {
            ProxyTasks.Add(Task.Factory.StartNew(() => CheckRemovals()));
            KonekcijaProxyAsServer.Open();
            Input.ReadLine();
            ProxyWorking = false;
            Task.WaitAll(ProxyTasks.ToArray());
        }
    
        public int secondsToSleep { get; set; } = 10;
        /// <summary>
        /// Pokrece task koji ce proveravati da li se treba brisati u lokalnoj kopiji
        /// </summary>
        private void CheckRemovals()
        {
            

            while (ProxyWorking)
            {
                ProxyService.CheckRemovals();
                Task.Delay(secondsToSleep * 1000).Wait();
            }
        }
    }
}
