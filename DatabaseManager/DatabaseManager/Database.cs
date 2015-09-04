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
using Oracle.ManagedDataAccess.Client;
using Npgsql;
using IBM.Data.DB2;

namespace DatabaseManager
{
    public partial class Database : Form
    {
        internal Form MainForm;
        object conn, da;
        int dbtype;
        string dbname, tbname;
        DataTable tb = new DataTable();

        private void showParent(object sender, EventArgs e)
        {
            if (MainForm.Visible) return;
            MainForm.Show();
            Close();
        }

        public Database(object con, int type, string db, string table)
        {
            InitializeComponent();
            conn = con;
            dbtype = type;
            dbname = db;
            tbname = table;
            getData(this, new EventArgs());
        }
        private void getData(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            switch (dbtype)
            {
                case 0:
                    ((MySqlConnection)conn).Open();
                    da = new MySqlDataAdapter("use " + dbname + ";select * from " + tbname + ";", (MySqlConnection)conn);
                    new MySqlCommandBuilder((MySqlDataAdapter)da);
                    ((MySqlDataAdapter)da).Fill(tb);
                    ((MySqlConnection)conn).Close();
                    break;
                case 1:
                    ((SqlConnection)conn).Open();
                    da = new SqlDataAdapter("use " + dbname + ";select * from " + tbname + ";", (SqlConnection)conn);
                    new SqlCommandBuilder((SqlDataAdapter)da);
                    ((SqlDataAdapter)da).Fill(tb);
                    ((SqlConnection)conn).Close();
                    break;
                case 2:
                    ((OracleConnection)conn).Open();
                    da = new OracleDataAdapter("select * from " + tbname + ";", (OracleConnection)conn);
                    new OracleCommandBuilder((OracleDataAdapter)da);
                    ((OracleDataAdapter)da).Fill(tb);
                    ((OracleConnection)conn).Close();
                    break;
                case 3:
                    ((NpgsqlConnection)conn).Open();
                    da = new NpgsqlDataAdapter("select * from " + tbname + ";", (NpgsqlConnection)conn);
                    new NpgsqlCommandBuilder((NpgsqlDataAdapter)da);
                    ((NpgsqlDataAdapter)da).Fill(tb);
                    ((NpgsqlConnection)conn).Close();
                    break;
                case 4:
                    ((DB2Connection)conn).Open();
                    da = new DB2DataAdapter("select * from " + tbname + ";", (DB2Connection)conn);
                    new DB2CommandBuilder((DB2DataAdapter)da);
                    ((DB2DataAdapter)da).Fill(tb);
                    ((DB2Connection)conn).Close();
                    break;
                default:
                    break;
            }
            dataGridView1.DataSource = tb;
        }
        private void updateDB(object sender, EventArgs e)
        {
            switch (dbtype)
            {
                case 0:
                    ((MySqlConnection)conn).Open();
                    ((MySqlDataAdapter)da).Update(tb);
                    ((MySqlConnection)conn).Close();
                    break;
                case 1:
                    ((SqlConnection)conn).Open();
                    ((SqlDataAdapter)da).Update(tb);
                    ((SqlConnection)conn).Close();
                    break;
                case 2:
                    ((OracleConnection)conn).Open();
                    ((OracleDataAdapter)da).Update(tb);
                    ((OracleConnection)conn).Close();
                    break;
                case 3:
                    ((NpgsqlConnection)conn).Open();
                    ((NpgsqlDataAdapter)da).Update(tb);
                    ((NpgsqlConnection)conn).Close();
                    break;
                case 4:
                    ((DB2Connection)conn).Open();
                    ((DB2DataAdapter)da).Update(tb);
                    ((DB2Connection)conn).Close();
                    break;
                default:
                    break;
            }
        }
    }
}
