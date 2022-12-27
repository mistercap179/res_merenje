using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    /// <summary>
    /// 
    /// </summary>
    class ProxyMerenje : Models.Merenje
    {
        public DateTime PoslednjiPutDobavljeno { get; set; }
        public DateTime PoslednjiPutProcitano { get; set; }
        public ProxyMerenje() : base() {}
    }
}
