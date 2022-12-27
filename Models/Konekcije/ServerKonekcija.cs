using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models.Konekcije
{
    public class ServerKonekcija
    {
        List<Uri> Uris = new List<Uri>();
        IMerenjeService Service = null;
        ServiceHost Host = null;
        NetTcpBinding Binding = null;
        public ServerKonekcija(string[] uris, IMerenjeService service)
        {
            try
            {
                foreach (var uri in uris)
                {
                    Uris.Add(new Uri(uri));
                }

                Service = service;
                Host = new ServiceHost(Service, Uris.ToArray());
                Binding = new NetTcpBinding(SecurityMode.None);
                Host.AddServiceEndpoint(typeof(IMerenjeService), Binding, "");
                Host.Opened += Host_Opened;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating server:{e.Message}");
            }
        }

        private void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Server started.");
        }

        public void Open()
        {
            try
            {
                Host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while opening server:{e.Message}");
            }
        }
    }
}
