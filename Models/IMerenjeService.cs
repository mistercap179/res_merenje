using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [ServiceContract]
    public interface IMerenjeService
    {
        [OperationContract]
        Models.Merenje getById(int id);

        [OperationContract]
        double getAzuriranuVrednost(int id);

        [OperationContract]
        List<double?> getVrednosti();
        
        [OperationContract]
        List<Merenje> getAnalogni();

        [OperationContract]
        List<Merenje> getDigitalni();


    }

}
