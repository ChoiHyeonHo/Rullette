using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Util
{
    class CommonUtil
    {
        #region 데이터그리드뷰
        public static void SetInitGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        #endregion

        public static void AddGridTextColumn(
                DataGridView dgv,
                string headerText,
                string dataPropertyName,
                int colWidth = 100,
                bool visibility = true,
                DataGridViewContentAlignment textAlign = DataGridViewContentAlignment.MiddleLeft)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = dataPropertyName;
            col.HeaderText = headerText;
            col.DataPropertyName = dataPropertyName;
            col.Width = colWidth;
            col.DefaultCellStyle.Alignment = textAlign;
            col.Visible = visibility;
            col.ReadOnly = true;

            dgv.Columns.Add(col);
        }

        public static void AddGridCheckBoxColumn(
                DataGridView dgv,
                bool visibility = true)
        {
            DataGridViewCheckBoxColumn cbCol = new DataGridViewCheckBoxColumn();
            cbCol.Name = "";
            cbCol.HeaderText = "";
            cbCol.CellTemplate = new DataGridViewCheckBoxCell();
        }
    }
}