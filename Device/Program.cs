using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Device
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Device se digao");

            const short serverPort = 1234;
            var uri = $"net.tcp://localhost:{serverPort}/MerenjeService";

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            var channel = new ChannelFactory<IWrite>(binding);
            var endpoint = new EndpointAddress(uri);
            var proxy = channel.CreateChannel(endpoint);

            while (true)
            {
                Random rand = new Random();
                Merenje merenje = new Merenje()
                {
                    IdMerenja = rand.Next(),
                    IdDevice = rand.Next(),
                    Timestamp = DateTime.Now.Ticks,
                    Vrednost = rand.Next(20),
                    Tip = (MerenjeTip)rand.Next(1)
                };

                proxy.writeDevice(merenje) ;
                Task.Delay(10000).Wait();
            }

        }
    }
}
