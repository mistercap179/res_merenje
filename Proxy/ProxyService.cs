using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    interface IProxyService : Models.IMerenjeService 
    {
        void CheckRemovals();
    }

    class ProxyService : IProxyService
    {
        Models.IMerenjeService ServerService = null;
        private List<ProxyMerenje> LocalStorage = new List<ProxyMerenje>();

        public ProxyService(Models.IMerenjeService serverService)
        {
            ServerService = serverService;
        }

        public ICollection<Merenje> GetMerenjes()
        {
            return null;
        }

        public void CheckRemovals()
        {
            LocalStorage.RemoveAll(x => x.PoslednjiPutProcitano.CompareTo(DateTime.Now.AddDays(-1)) < 0);
        }
    }
}
