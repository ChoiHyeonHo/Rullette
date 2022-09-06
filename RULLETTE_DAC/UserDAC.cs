using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RULLETTE_VO;

namespace RULLETTE_DAC
{
    public class UserDAC : ConnectionAccess, IDisposable
    {
        string strConn;
        SqlConnection conn;

        public UserDAC()
        {
            strConn = this.ConnectionString;
            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public List<UserVO> getUserList()
        {
            string sql = @"SELECT USER_ID,
                                  USER_NM
                             FROM RL_USER
                            WHERE USE_STAT_CD = 'Y'";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                List<UserVO> list = Helper.DataReaderMapToList<UserVO>(reader);
                Dispose();
                return list;
            }
        }
        public string upsertUserList(string userNm)
        {
            string rsltMsg = null;
            int iRowAffect;
            string sql = @"SELECT USER_NM
                             FROM RL_USER
                            WHERE 1=1
                              AND USER_NM = @userNm";

            using (SqlCommand selectCmd = new SqlCommand(sql, conn))
            {
                selectCmd.Parameters.AddWithValue("@userNm", userNm);
                SqlDataReader reader = selectCmd.ExecuteReader();

                List<UserVO> list = Helper.DataReaderMapToList<UserVO>(reader);
                reader.Close();
                if (list.Count > 0)
                {
                    sql = @"UPDATE RL_USER SET USE_STAT_CD = 'Y' WHERE USER_NM = @userNm";
                    using (SqlCommand updateCmd = new SqlCommand(sql, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@userNm", userNm);
                        iRowAffect = updateCmd.ExecuteNonQuery();
                        conn.Close();
                        if (iRowAffect > 0)
                        {
                            rsltMsg = Properties.Resources.msgOK;
                        }
                        else
                        {
                            rsltMsg = Properties.Resources.msgNG;
                        }
                    }
                } else if (list.Count == 0)
                {
                    sql = @"INSERT INTO RL_USER (USER_NM)
                                 VALUES (@userNm)";
                    using (SqlCommand insertCmd = new SqlCommand(sql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@userNm", userNm);
                        iRowAffect = insertCmd.ExecuteNonQuery();
                        conn.Close();
                        if (iRowAffect > 0)
                        {
                            rsltMsg = Properties.Resources.msgOK;
                        } else
                        {
                            rsltMsg = Properties.Resources.msgNG;
                        }
                    }
                }
            }

            return rsltMsg;
        }

        public string unusableUserList(string userNm)
        {
            string rsltMsg = null;
            int iRowAffect;
            string sql = $@"UPDATE RL_USER SET USE_STAT_CD = 'N' WHERE USER_NM IN ({userNm})";

            using (SqlCommand unusableCmd = new SqlCommand(sql, conn))
            {
                //unusableCmd.Parameters.AddWithValue("@userNm", userNm);
                iRowAffect = unusableCmd.ExecuteNonQuery();
                conn.Close();
                if (iRowAffect > 0)
                {
                    rsltMsg = Properties.Resources.msgOK;
                }
                else
                {
                    rsltMsg = Properties.Resources.msgNG;
                }
            }

            return rsltMsg;
        }

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}
