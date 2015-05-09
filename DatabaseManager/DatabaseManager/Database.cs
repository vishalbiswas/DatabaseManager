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
    public partial class Database : Form
    {
        MySqlConnection sconn;
        SqlConnection mconn;
        MySqlCommand scom;
        SqlCommand mcom;
        MySqlDataAdapter sad;
        SqlDataAdapter mad;
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
            InitializeComponent();
        }
    }
}
