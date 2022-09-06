using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Util;
using RULLETTE_DAC;
using RULLETTE_VO;

namespace WindowsFormsApp1
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        List<UserVO> userList = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
