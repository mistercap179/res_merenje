using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MerenjeService : IMerenjeService
    {
        [OperationBehavior]
        public ICollection<Models.Merenje> GetMerenjes()
        {
            using (var db = new MerenjeEntities())
            {
                return db.Merenjes.ToList().Select(x =>
                {
                    return new Models.Merenje()
                    {
                        IdDb = (int)x.idDb,
                        IdMerenja = (int)x.idMerenja,
                        Timestamp = x.timestamp,
                        Tip = (MerenjeTip)x.tip,
                        Vrednost = (double)x.vrednost
                    };
                }).ToList();
            }

        }
    }
}
