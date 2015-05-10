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
		MySqlConnection sconn;
        SqlConnection mconn;
        MySqlCommand scom;
        SqlCommand mcom;
        MySqlDataAdapter sad;
        SqlDataAdapter mad;
        DataSet ds = new DataSet();
		public DatabaseSelector(MySqlConnection con)
		{
			sconn = con;
            sconn.Open();
            scom = new MySqlCommand("show databases;", sconn);
            scom.ExecuteNonQuery();
            sad = new MySqlDataAdapter(scom);
            sad.Fill(ds);
			InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            sconn.Close();
		}
        public DatabaseSelector(SqlConnection con)
        {
            mconn = con;
            mconn.Open();
            mcom = new SqlCommand("exec sp_databases;", mconn);
            mcom.ExecuteNonQuery();
            mad = new SqlDataAdapter(mcom);
            mad.Fill(ds);
            InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            mconn.Close();
        }

        private void next(object sender, EventArgs e)
        {
            if (sconn != null)
            {
                TableSelector TS = new TableSelector(sconn, listBox1.SelectedItem.ToString());
                TS.Show();
            }
            else if (mconn != null)
            {
                TableSelector TS = new TableSelector(mconn, listBox1.SelectedItem.ToString());
                TS.Show();
            }
                Close();
        }
    }
}
