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
using Oracle.DataAccess.Client;
using Npgsql;
using IBM.Data.DB2;

namespace DatabaseManager
{
    public partial class TableSelector : Form
    {
        MySqlConnection sconn;
        SqlConnection mconn;
        OracleConnection oconn;
        NpgsqlConnection nconn;
        DB2Connection dconn;
        MySqlCommand scom;
        SqlCommand mcom;
        OracleCommand ocom;
        NpgsqlCommand ncom;
        DB2Command dcom;
        MySqlDataAdapter sad;
        SqlDataAdapter mad;
        OracleDataAdapter oad;
        NpgsqlDataAdapter nad;
        DB2DataAdapter dad;
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
        public TableSelector(OracleConnection con)
        {
            oconn = con;
            oconn.Open();
            ocom = new OracleCommand("select table_name from user_tables", oconn);
            ocom.ExecuteNonQuery();
            oad = new OracleDataAdapter(ocom);
            oad.Fill(ds);
            InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            oconn.Close();
        }
        public TableSelector(NpgsqlConnection con)
        {
            nconn = con;
            nconn.Open();
            ncom = new NpgsqlCommand("select table_name from information_schema.tables where table_schema = 'public';", nconn);
            ncom.ExecuteNonQuery();
            nad = new NpgsqlDataAdapter(ncom);
            nad.Fill(ds);
            InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            nconn.Close();
        }
        public TableSelector(DB2Connection con)
        {
            dconn = con;
            dconn.Open();
            dcom = new DB2Command("select tabname from syscat.tables;", dconn);
            dcom.ExecuteNonQuery();
            dad = new DB2DataAdapter(dcom);
            dad.Fill(ds);
            InitializeComponent();
            foreach (DataRow row in ds.Tables[0].Rows) listBox1.Items.Add(row[0]);
            dconn.Close();
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
            else if (oconn != null)
            {
                Database DB = new Database(oconn, listBox1.SelectedItem.ToString());
                DB.Show();
            }
            else if (nconn != null)
            {
                Database DB = new Database(nconn, listBox1.SelectedItem.ToString());
                DB.Show();
            }
            else if (dconn != null)
            {
                Database DB = new Database(dconn, listBox1.SelectedItem.ToString());
                DB.Show();
            }
            Close();
        }
    }
}
