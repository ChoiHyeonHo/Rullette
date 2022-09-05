using System;
using System.Data.SqlClient;
using RULLETTE_DAC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RULLETTE_DAC
{
    class PaymentDAC : ConnectionAccess, IDisposable
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

        public void Dispose()
        {
            conn.Close();
        }
    }
}
