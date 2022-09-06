using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULLETTE_DAC
{
    public class PaymentDAC : ConnectionAccess, IDisposable
    {
        string strConn;
        SqlConnection conn;

        public PaymentDAC()
        {
            try
            {
                strConn = this.ConnectionString;
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool insertPayment(string userNm, int payMoney)
        {
            int iRowAffect;
            string sql = $@"INSERT INTO RL_PAYMENT (USER_NM, PAY_MONEY)
                                            VALUES ({userNm}, {payMoney});";

            using (SqlCommand unusableCmd = new SqlCommand(sql, conn))
            {
                iRowAffect = unusableCmd.ExecuteNonQuery();
                conn.Close();
            }

            return iRowAffect > 0;
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}
