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
    public partial class TableSelector : Form
    {
        MySqlConnection sconn;
        SqlConnection mconn;
        MySqlCommand scom;
        SqlCommand mcom;
        MySqlDataAdapter sad;
        SqlDataAdapter mad;
        DataSet ds = new DataSet();
        string da;
        public TableSelector(MySqlConnection con, string db)
        {
            da = db;
            sconn = con;
            sconn.Open();
            scom = new MySqlCommand("use "+db+";show tables;", sconn);
            scom.ExecuteNonQuery();
            sad = new MySqlDataAdapter(scom);
            sad.Fill(ds);
            InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            sconn.Close();
        }
        public TableSelector(SqlConnection con, string db)
        {
            da = db;
            mconn = con;
            mconn.Open();
            mcom = new SqlCommand("use " + db + ";select table_name from information_schema.tables;", mconn);
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
                Database DB = new Database(sconn, da, listBox1.SelectedItem.ToString());
                DB.Show();
            }
            else if (mconn != null)
            {
                Database DB = new Database(mconn, da, listBox1.SelectedItem.ToString());
                DB.Show();
            }
            Close();
        }
    }
}
