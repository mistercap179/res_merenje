using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models.Konekcije
{
    public class KlijentKonekcija
    {
        NetTcpBinding Binding { get; set; } = null;
        ChannelFactory<IMerenjeService> Channel { get; set; } = null;
        EndpointAddress EndpointAddress { get; set; } = null;
        public IMerenjeService Service { get; set; } = null;
        public KlijentKonekcija(string uri)
        {
            try
            {
                Binding = new NetTcpBinding(SecurityMode.None);
                Channel = new ChannelFactory<IMerenjeService>(Binding);
                EndpointAddress = new EndpointAddress(uri);
                Service = Channel.CreateChannel(EndpointAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to create client connection. Exception:{e.Message}");
            }
        }
    }
}
