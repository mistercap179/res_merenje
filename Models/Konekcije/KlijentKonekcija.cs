using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models.Konekcije
{
    public class KlijentKonekcija<T> : IKonekcija<T>
    {
        NetTcpBinding Binding { get; set; } = null;
        ChannelFactory<T> Channel { get; set; } = null;
        EndpointAddress EndpointAddress { get; set; } = null;
        public override T Service { get ; set ; }

        public KlijentKonekcija(string uri) : base()
        {
            try
            {
                Binding = new NetTcpBinding(SecurityMode.None);
                Channel = new ChannelFactory<T>(Binding);
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
