using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MerenjeService : Models.IServerMerenjeService, Models.IWrite
    {
        private Conversion conversion = new Conversion();
        private DBCRUD.IDBCRUD crud { get; set; } = null;
        public MerenjeService(DBCRUD.IDBCRUD crud) : base()
        {
            this.crud = crud;
        }

        [OperationBehavior]
        public IDictionary<int, long> GetAllTimestampsById(int id)
        {
            return crud.GetAllTimestampsById(id);
        }

        [OperationBehavior]
        public long GetLastTimestampById(int id)
        {
            long lastTimestamp = crud.GetLastTimestampById(id);
            if (lastTimestamp == -1)
            {
                throw new Exception("Last timestamp not found.");
            }

            return lastTimestamp;
        }

        [OperationBehavior]
        public IDictionary<long, long> GetTimestampPerDevice()
        {
            return crud.GetTimestampPerDevice();
        }

        [OperationBehavior]
        public IDictionary<long, long> GetTimestampsAnalog()
        {
            return crud.GetTimestampsAnalog();
        }

        [OperationBehavior]
        public IDictionary<long, long> GetTimestampsDigital()
        {
            return crud.GetTimestampsDigital();
        }

        [OperationBehavior]
        public void WriteDevice(Models.Merenje merenje)
        {
            crud.WriteDevice(
                new Merenje()
                {
                    idDevice = merenje.IdDevice,
                    idMerenja = merenje.IdMerenja,
                    timestamp = merenje.Timestamp,
                    tip = (int?)merenje.Tip,
                    vrednost = merenje.Vrednost
                }
            );
        }
        
        [OperationBehavior]
        public Models.Merenje GetMerenjeByDbId(int dbId)
        {
            try
            {
                return conversion.ConversionMerenje(crud.GetMerenjeByDbId(dbId));
            }
            catch (Exception)
            {
                throw new Exception("Merenje not found");
            }
        }

        public Models.Merenje GetLastMerenjeFromIdMerenje(int id)
        {
            try
            {
                return conversion.ConversionMerenje(crud.GetLastMerenjeFromIdMerenje(id));
            }
            catch (Exception)
            {
                throw new Exception("Merenje not found");
            }
        }

        [OperationBehavior]
        public Models.Merenje GetLastForDeviceId(int id)
        {
            try
            {
                var merenje = crud.GetLastForDeviceId(id);
                return conversion.ConversionMerenje(merenje);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Nije nadjeno merenje za uredjaj.");
            }
        }

    }
}
