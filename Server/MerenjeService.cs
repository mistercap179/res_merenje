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
    public class MerenjeService : IMerenjeService,IWrite
    {
        public Conversion conversion = new Conversion();

        [OperationBehavior]
        public Models.Merenje getById(int id)
        {
            Models.Merenje merenje = new Models.Merenje();

            try
            {
                using (var db = new MerenjeEntities())
                {
                    merenje = conversion.ConversionMerenje(db.Merenjes.Where(x => x.idMerenja == id).FirstOrDefault());
                }
            }
            catch(Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
       
            return merenje;

        }

        [OperationBehavior]
        public double getAzuriranuVrednost(int id)
        {
            try
            {
                using (var db = new MerenjeEntities())
                {
                    Models.Merenje merenje = new Models.Merenje();
                    double vrednost;
                    long maxTimestamp = db.Merenjes.Where(x => x.idMerenja == id).Max(x=>x.timestamp);
                    merenje = conversion.ConversionMerenje(db.Merenjes.Where(x => x.idMerenja == id && x.timestamp == maxTimestamp).FirstOrDefault());
                    return vrednost = merenje.Vrednost;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
           
        }

        [OperationBehavior]
        public List<double?> getVrednosti()
        {
            List<double?> listaV = new List<double?>();
            try
            {
                using (var db = new MerenjeEntities())
                {
                    db.Merenjes.Select(x => x.idDevice)
                        .ToList()
                        .ForEach(idm => 
                    {
                        var val = db.Merenjes.Where(m => m.idDevice == idm)
                                             .OrderByDescending(m => m.timestamp).FirstOrDefault();
                        listaV.Add(val.vrednost);
                    });

                    return listaV;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return listaV;
            }

        }

        [OperationBehavior]
        public List<Models.Merenje> getAnalogni()
        {
<<<<<<< HEAD
            using (var db = new MerenjeEntities())
=======
            List<Models.Merenje> listaA = new List<Models.Merenje>();
            try
>>>>>>> 0cad5371c5968881bb755d3a29184c3070d1cb17
            {
                using (var db = new MerenjeEntities())
                {
                    db.Merenjes.Where(m => m.tip == ((int)MerenjeTip.ANALOGNO)).ToList()
                           .ForEach(x =>
                           {
                               listaA.Add(conversion.ConversionMerenje(x));

                           });

                    return listaA;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return listaA;
            }
        }


        [OperationBehavior]
        public List<Models.Merenje> getDigitalni()
        {
            List<Models.Merenje> listaD = new List<Models.Merenje>();
            try
            {
                using (var db = new MerenjeEntities())
                {
                    db.Merenjes.Where(m => m.tip == ((int)MerenjeTip.DIGITALNO)).ToList()
                           .ForEach(x =>
                           {
                               listaD.Add(conversion.ConversionMerenje(x));

                           });

                    return listaD;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return listaD;
            }
        }

        public void writeDevice(Models.Merenje merenje)
        {
            try
            {
                using (var db = new MerenjeEntities())
                {
                    db.Merenjes.Add(new Merenje()
                    {
<<<<<<< HEAD
                        IdDb = (int)x.idDb,
                        IdMerenja = (int)x.idMerenja,
                        Timestamp = x.timestamp,
                        Tip = (MerenjeTip)x.tip,
                        Vrednost = (double)x.vrednost
                    };
                }).ToList();
=======
                        idMerenja = merenje.IdMerenja,
                        timestamp = merenje.Timestamp,
                        tip = (int)merenje.Tip,
                        vrednost = merenje.Vrednost
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
>>>>>>> 0cad5371c5968881bb755d3a29184c3070d1cb17
            }

        }
    }
}
