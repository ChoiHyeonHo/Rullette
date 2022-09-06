using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RULLETTE_DAC;
using RULLETTE_VO;

namespace WindowsFormsApp1
{
    class PaymentService
    {
        public bool insertPayment(string userNm, int payMoney)
        {
            PaymentDAC dac = new PaymentDAC();
            return dac.insertPayment(userNm, payMoney);
        }
    }
}
