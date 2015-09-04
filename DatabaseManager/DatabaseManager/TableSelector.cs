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
    public partial class TableSelector : Form
    {
        internal Form MainForm;
        object conn, da;
        int dbtype;
        string dbname;
        public TableSelector(object con, int type, string db)
        {
            DataTable tb = new DataTable();
            switch (type)
            {
                case 0:
                    ((MySqlConnection)con).Open();
                    da = new MySqlDataAdapter("use " + db + ";show tables;", (MySqlConnection)con);
                    ((MySqlDataAdapter)da).Fill(tb);
                    ((MySqlConnection)con).Close();
                    break;
                case 1:
                    ((SqlConnection)con).Open();
                    SqlDataAdapter mad = new SqlDataAdapter("use " + db + ";select table_name from information_schema.tables;", (SqlConnection)con);
                    ((SqlDataAdapter)da).Fill(tb);
                    ((SqlConnection)con).Close();
                    break;
                case 2:
                    ((OracleConnection)con).Open();
                    OracleDataAdapter oad = new OracleDataAdapter("select table_name from user_tables", (OracleConnection)con);
                    ((OracleDataAdapter)da).Fill(tb);
                    ((OracleConnection)con).Close();
                    break;
                case 3:
                    ((NpgsqlConnection)con).Open();
                    NpgsqlDataAdapter nad = new NpgsqlDataAdapter("select table_name from information_schema.tables where table_schema = 'public';", (NpgsqlConnection)conn);
                    ((NpgsqlDataAdapter)da).Fill(tb);
                    ((NpgsqlConnection)con).Close();
                    break;
                case 4:
                    ((DB2Connection)con).Open();
                    DB2DataAdapter dad = new DB2DataAdapter("use " + db + ";show tables;", (DB2Connection)con);
                    ((DB2DataAdapter)da).Fill(tb);
                    ((DB2Connection)con).Close();
                    break;
                default:
                    break;
            }
            InitializeComponent();
            foreach (DataRow row in tb.Rows) listBox1.Items.Add(row[0]);
            conn = con;
            dbtype = type;
            dbname = db;
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
            Database DB = new Database(conn, dbtype, dbname, listBox1.SelectedItem.ToString());
            DB.MainForm = MainForm;
            DB.Show();
            Hide();
        }
    }
}
