using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RULLETTE_VO;
using RULLETTE_DAC;

namespace WindowsFormsApp1
{
    class UserService
    {
        public List<UserVO> getUserList()
        {
            UserDAC dac = new UserDAC();
            return dac.getUserList();
        }
        public string upsertUser(string userNm)
        {
            UserDAC dac = new UserDAC();
            return dac.upsertUserList(userNm);
        }
        public string unusableUser(string usernm)
        {
            UserDAC dac = new UserDAC();
            return dac.unusableUserList(usernm);
        }
    }
}
