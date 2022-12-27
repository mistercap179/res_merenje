using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Konekcije
{
    public class Konekcija
    {
        public static short ServerPort { get; set; } = 1234;
        public static short ProxyServerPort { get; set; } = 1235;

        public static string UriServer { get; set; } = $"net.tcp://localhost:{ServerPort}/MerenjeService";
        public static string UriProxyServer { get; set; } = $"net.tcp://localhost:{ProxyServerPort}/MerenjeService";
    }
}
