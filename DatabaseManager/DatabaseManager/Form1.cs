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


namespace DatabaseManager
{
	public partial class Form1 : Form
	{
		bool IntegrateSecurity = false;
		public Form1()
		{
			InitializeComponent();
			comboBox1.SelectedItem = "MySQL/MariaDB";
		}

		private void useDefault(object sender, EventArgs e)
		{
            if (comboBox1.SelectedItem.ToString() == "Oracle" || comboBox1.SelectedItem.ToString() == "PostgreSQL") textBox3.Enabled = label4.Enabled = true;
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
			switch (comboBox1.SelectedItem.ToString()) {
				case "MySQL/MariaDB":
					MySqlConnection sconn = new MySqlConnection("user=" + textBox1.Text + ";password=" + textBox2.Text + ";server=localhost;port=3306");
					try
					{
						sconn.Open();
						sconn.Close();
                        DatabaseSelector DS = new DatabaseSelector(sconn);
                        DS.Show();
					}
					catch (MySqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
					break;
				case "MSSQL":
                    string constr;
                    if (IntegrateSecurity) constr = "Data Source=localhost;Integrated Security=true";
                    else constr = "Data Source=localhost;Integrated Security=false;User ID = " + textBox1.Text + "; Password = " + textBox2.Text;
                    SqlConnection mconn = new SqlConnection(constr);
                    try
                    {
                        mconn.Open();
                        mconn.Close();
                        DatabaseSelector DS = new DatabaseSelector(mconn);
                        DS.Show();
                    }
                    catch (SqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
				case "Oracle":
                    //to be implemented
                    OracleConnection oconn = new OracleConnection("User Id=" + textBox1.Text + ";Password=" + textBox2.Text + ";Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID="+textBox3.Text+")));");
                    try
                    {
                        oconn.Open();
                        oconn.Close();
                        TableSelector TS = new TableSelector(oconn);
                        TS.Show();
                    }
                    catch (OracleException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
                case "PostgreSQL":
                    NpgsqlConnection nconn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=" + textBox1.Text + ";Password=" + textBox2.Text + ";Database=" + textBox3.Text + ";");
                    try
                    {
                        nconn.Open();
                        nconn.Close();
                        TableSelector TS = new TableSelector(nconn);
                        TS.Show();
                    }
                    catch (NpgsqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    break;
			}
			foreach (Control control in Controls) control.Enabled = true;
			Cursor = Cursors.Default;
		}
	}
}
