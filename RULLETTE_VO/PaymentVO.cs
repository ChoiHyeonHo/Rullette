using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULLETTE_VO
{
    class PaymentVO
    {
        public int? id { get; set; }
        public string user_id { get; set; }
        public string user_nm { get; set; }
        public DateTime pay_date { get; set; }
        public int pay_money { get; set; }
    }
}
