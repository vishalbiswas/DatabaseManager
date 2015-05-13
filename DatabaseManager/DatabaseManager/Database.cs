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
    public partial class Database : Form
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
        public Database(MySqlConnection con, string db, string table)
        {
            sconn = con;
            sconn.Open();
            scom = new MySqlCommand("use "+db+";select * from "+table+";", sconn);
            scom.ExecuteNonQuery();
            sad = new MySqlDataAdapter(scom);
            sad.Fill(ds);
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables[0];
            sconn.Close();
        }
        public Database(SqlConnection con, string db, string table)
        {
            mconn = con;
            mconn.Open();
            mcom = new SqlCommand("use " + db + ";select * from " + table + ";", mconn);
            mcom.ExecuteNonQuery();
            mad = new SqlDataAdapter(mcom);
            mad.Fill(ds);
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables[0];
            mconn.Close();
        }
        public Database(OracleConnection con, string table)
        {
            oconn = con;
            oconn.Open();
            ocom = new OracleCommand("select * from " + table, oconn);
            ocom.ExecuteNonQuery();
            oad = new OracleDataAdapter(ocom);
            oad.Fill(ds);
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables[0];
            oconn.Close();
        }
        public Database(NpgsqlConnection con, string table)
        {
            nconn = con;
            nconn.Open();
            ncom = new NpgsqlCommand("select * from " + table + ";", nconn);
            ncom.ExecuteNonQuery();
            nad = new NpgsqlDataAdapter(ncom);
            nad.Fill(ds);
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables[0];
            nconn.Close();
        }
        public Database(DB2Connection con, string table)
        {
            dconn = con;
            dconn.Open();
            dcom = new DB2Command("select * from " + table + ";", dconn);
            dcom.ExecuteNonQuery();
            dad = new DB2DataAdapter(dcom);
            dad.Fill(ds);
            InitializeComponent();
            dataGridView1.DataSource = ds.Tables[0];
            dconn.Close();
        }
    }
}
