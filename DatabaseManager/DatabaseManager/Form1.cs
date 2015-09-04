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
    public partial class Form1 : Form
    {
        object conn;
        bool IntegrateSecurity = false;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "MySQL/MariaDB";
        }

        private void useDefault(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Oracle" || comboBox1.SelectedItem.ToString() == "PostgreSQL" || comboBox1.SelectedItem.ToString() == "DB2") textBox3.Enabled = label4.Enabled = true;
            else textBox3.Enabled = label4.Enabled = false;
            if (checkBox2.Checked == true)
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "MySQL/MariaDB":
                        textBox1.Text = "root";
                        textBox2.Text = "";
                        break;
                    case "MSSQL":
                        IntegrateSecurity = true;
                        break;
                    case "Oracle":
                        //to be implemented
                        break;
                    case "PostgreSQL":
                        //to be implemented
                        break;
                    case "DB2":
                        textBox1.Text = textBox2.Text = "";
                        break;
                }
                textBox1.Enabled = textBox2.Enabled = false;
            }
            else
            {
                IntegrateSecurity = false;
                textBox1.Enabled = textBox2.Enabled = true;
            }
        }

        private void connect(object sender, EventArgs e)
        {
            foreach (Control control in Controls) control.Enabled = false;
            Cursor = Cursors.WaitCursor;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "MySQL/MariaDB":
                    conn = new MySqlConnection("user=" + textBox1.Text + ";password=" + textBox2.Text + ";server=localhost;port=3306");
                    try
                    {
                        ((MySqlConnection)conn).Open();
                        ((MySqlConnection)conn).Close();
                        DatabaseSelector DS = new DatabaseSelector(conn, comboBox1.SelectedIndex);
                        DS.MainForm = this;
                        DS.Show();
                        Hide();
                    }
                    catch (MySqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                case "MSSQL":
                    string constr;
                    if (IntegrateSecurity) constr = "Data Source=localhost;Integrated Security=true";
                    else constr = "Data Source=localhost;Integrated Security=false;User ID = " + textBox1.Text + "; Password = " + textBox2.Text;
                    conn = new SqlConnection(constr);
                    try
                    {
                        ((SqlConnection)conn).Open();
                        ((SqlConnection)conn).Close();
                        DatabaseSelector DS = new DatabaseSelector(conn, comboBox1.SelectedIndex);
                        DS.MainForm = this;
                        DS.Show();
                        Hide();
                    }
                    catch (SqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                case "Oracle":
                    conn = new OracleConnection("User Id=" + textBox1.Text + ";Password=" + textBox2.Text + ";Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=" + textBox3.Text + ")));");
                    try
                    {
                        ((OracleConnection)conn).Open();
                        ((OracleConnection)conn).Close();
                        TableSelector TS = new TableSelector(conn, comboBox1.SelectedIndex, textBox3.Text);
                        TS.MainForm = this;
                        TS.Show();
                        Hide();
                    }
                    catch (OracleException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                case "PostgreSQL":
                    conn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=" + textBox1.Text + ";Password=" + textBox2.Text + ";Database=" + textBox3.Text + ";");
                    try
                    {
                        ((NpgsqlConnection)conn).Open();
                        ((NpgsqlConnection)conn).Close();
                        TableSelector TS = new TableSelector(conn, comboBox1.SelectedIndex, textBox3.Text);
                        TS.MainForm = this;
                        TS.Show();
                        Hide();
                    }
                    catch (NpgsqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                case "DB2":
                    try {
                        conn = new DB2Connection("Server=localhost:50000;UserId=" + textBox1.Text + ";Password=" + textBox2.Text + ";Database=" + textBox3.Text);
                        ((DB2Connection)conn).Open();
                        ((DB2Connection)conn).Close();
                        TableSelector TS = new TableSelector(conn, comboBox1.SelectedIndex, textBox3.Text);
                        TS.MainForm = this;
                        TS.Show();
                        Hide();
                    }
                    catch (DllNotFoundException err) { MessageBox.Show("Make sure you have DB2 client installed.\r\n" + err.Message, "Error Occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    catch (DB2Exception err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                default:
                    break;
                    }
            foreach (Control control in Controls) control.Enabled = true;
            Cursor = Cursors.Default;
        }
    }
}
