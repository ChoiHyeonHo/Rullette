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

namespace WindowsFormsApp1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string strConn;
        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            getAppSettings();
            ChangeListBox();
            btnRsltChk.Visible = false;

            //try
            //{
            //    strConn = ConfigurationManager.ConnectionStrings["NX_RULLETTE"].ConnectionString;
            //    conn = new SqlConnection(strConn);
            //    conn.Open();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != null && txtName.Text != "")
            {
                for (int i = 0; i < clbName.Items.Count; i++)
                {
                    if (clbName.Items[i].ToString() == txtName.Text)
                    {
                        lblResultName.Text = "중복된 이름입니다.";
                        return;
                    }
                }
                string key = txtName.Text;
                string value = txtName.Text;
                saveAppConfig(key, value);

                clbName.Items.Add(txtName.Text.ToString());
                clearText();
            }
            else
            {
                lblResultName.Text = "추가할 이름을 입력하여 주십시오.";
            }
            ChangeListBox();
        }

        private static void saveAppConfig(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clbName.CheckedItems.Count == 0)
            {
                lblResultName.Text = "제거할 이름이 선택되지 않았습니다.";
                return;
            }
            for (int i = clbName.Items.Count - 1; i >= 0; i--)
            {
                if (clbName.GetItemChecked(i))
                {
                    string key = clbName.Items[i].ToString();
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var settings = configFile.AppSettings.Settings;
                    settings.Remove(key);
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                    clbName.Items.RemoveAt(i);
                }
            }
            ChangeListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int a = r.Next(0, clbName.Items.Count);

            int interval = 1000;
            //progressBar1.Visible = true;
            //timer1.Start();

            for (int t = 5; t > -2; t--)
            {
                lblResultName.Text = t.ToString();
                Thread.Sleep(interval);
                Application.DoEvents();
                if (t == -1)
                {
                    lblResultName.Visible = false;
                    btnRsltChk.Visible = true;

                    for (int i = 0; i < clbName.Items.Count; i++)
                    {
                        if (a == i)
                        {
                            lblResultName.Text = clbName.Items[i].ToString();
                        }
                    }

                }
            }
        }
        private void clearText()
        {
            txtName.Text = "";
            txtName.Focus();
        }
        private void ChangeListBox()
        {
            if (clbName.Items.Count > 0)
            {
                button1.Enabled = true;
                button1.BackColor = Color.DeepSkyBlue;
            }
            else
            {
                button1.Enabled = false;
                button1.BackColor = Color.LightGray;
            }
        }
        private void getAppSettings()
        {
            for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
            {
                clbName.Items.Add(ConfigurationManager.AppSettings.Get(i));
            }
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string temp = "9f1+6EDpE6hCXhwu5ECg5kxHLf4pqTzdbZin976D++6s4XFsF70+Z9u3mQ9wxdDVObkHti1RSdcDjT1N3h2A+Q==";

            EncrytLibrary.AES aes = new EncrytLibrary.AES();
            string strConn = aes.AESDecrypt256(temp);

            MessageBox.Show(strConn);

            //try
            //{
            //    string sql = @"SELECT * FROM RL_PAYMENT";

            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        SqlDataReader reader = cmd.ExecuteReader();

            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
