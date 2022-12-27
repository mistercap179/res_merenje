using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Conversion
    {
        public Models.Merenje ConversionMerenje(Merenje merenje)
        {
            return new Models.Merenje()
            {
                Id = (int)merenje.id,
                Timestamp = merenje.timestamp,
                Tip = (MerenjeTip)merenje.tip,
                Vrednost = (double)merenje.vrednost
            };
        }
    }
}
