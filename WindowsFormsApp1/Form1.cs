using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;
using System.Data.SqlClient;
using RULLETTE_VO;
using WindowsFormsApp1.Util;

namespace WindowsFormsApp1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        List<UserVO> userList = null;
        public Form1()
        {
            InitializeComponent();
            btnRsltChk.Visible = false;
            button1.Enabled = false;
            UserListBinding();
        }

        #region 추가
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == null || txtName.Text == "" || txtName.Text == string.Empty)
            {
                lblResultName.Text = Properties.Resources.msg003;
            } else
            {
                UserService service = new UserService();
                string rsltMsg = service.upsertUser(txtName.Text.ToString());
                MessageBox.Show(rsltMsg);
                UserListBinding();
                clearText();
            }
        }
        #endregion

        #region 제거
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> chkUserList = new List<string>();
            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                bool bCheck = (bool)row.Cells["chk"].EditedFormattedValue;
                if (bCheck)
                {
                    chkUserList.Add("'" + row.Cells["user_nm"].Value.ToString() + "'");
                }
            }

            if (chkUserList.Count == 0)
            {
                lblResultName.Text = Properties.Resources.msg001;
            } else
            {
                string temp = string.Join(",", chkUserList);

                UserService service = new UserService();
                string rsltMsg = service.unusableUser(temp);
                MessageBox.Show(rsltMsg);
                UserListBinding();
                clearText();
            }
        }
        #endregion

        #region GoRullette!
        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd"));
            button1.Enabled = false;
            Random r = new Random();
            int a = r.Next(0, dgvUser.RowCount);

            int interval = 1000;

            for (int t = 5; t > -2; t--)
            {
                lblResultName.Text = t.ToString();
                Thread.Sleep(interval);
                Application.DoEvents();
                if (t == -1)
                {
                    lblResultName.Visible = false;
                    btnRsltChk.Visible = true;

                    for (int i = 0; i < dgvUser.RowCount; i++)
                    {
                        if (a == i)
                        {
                            lblResultName.Text = dgvUser.Rows[i].Cells[2].Value.ToString();
                            PaymentService service = new PaymentService();

                            bool rslt = service.insertPayment("'"+lblResultName.Text.ToString()+"'", int.Parse(textBox1.Text));
                            if (rslt)
                            {
                                break;
                            }
                            else
                            {
                                MessageBox.Show(Properties.Resources.msg002);
                                return;
                            }
                        }
                    }

                }
            }
        }
        #endregion

        private void clearText()
        {
            txtName.Text = "";
            txtName.Focus();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick();
            }
        }

        private void btnRsltChk_Click(object sender, EventArgs e)
        {
            btnRsltChk.Visible = false;
            lblResultName.Visible = true;
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void UserListBinding()
        {

            if (dgvUser.DataSource == null)
            {
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "";
                chk.Name = "chk";
                chk.Width = 30;
                dgvUser.Columns.Add(chk);
                CommonUtil.SetInitGridView(dgvUser);
                CommonUtil.AddGridTextColumn(dgvUser, Properties.Resources.column_user_id, "user_id", 100);
                CommonUtil.AddGridTextColumn(dgvUser, Properties.Resources.column_user_nm, "user_nm", 100);
            }

            UserService service = new UserService();
            userList = service.getUserList();
            dgvUser.DataSource = userList;
            dgvUser.ClearSelection();
        }
    }
}
