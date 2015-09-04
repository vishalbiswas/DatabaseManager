using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DatabaseManager
{
    public partial class DatabaseSelector : Form
    {
        internal Form MainForm;
        object conn, da;
        int dbtype;

        public DatabaseSelector(object con, int type)
        {
            DataTable tb = new DataTable();
            switch (type)
            {
                case 0:
                    ((MySqlConnection)con).Open();
                    da = new MySqlDataAdapter("show databases;", (MySqlConnection)con);
                    ((MySqlDataAdapter)da).Fill(tb);
                    ((MySqlConnection)con).Close();
                    break;
                case 1:
                    ((SqlConnection)con).Open();
                    da = new SqlDataAdapter("exec sp_databases;", (SqlConnection)con);
                    ((SqlDataAdapter)da).Fill(tb);
                    ((SqlConnection)con).Close();
                    break;
                default:
                    break;
            }
            InitializeComponent();
            foreach (DataRow row in tb.Rows) listBox1.Items.Add(row[0]);
            conn = con;
            dbtype = type;
        }

        private void showParent(object sender, EventArgs e)
        {
            if (MainForm.Visible) return;
            MainForm.Show();
            Close();
        }

        private void next(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            TableSelector TS = new TableSelector(conn, dbtype, listBox1.SelectedItem.ToString());
            TS.MainForm = MainForm;
            TS.Show();
            Hide();
        }
    }
}
