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
	public partial class Form1 : Form
	{
		bool IntegrateSecurity = false;
		public Form1()
		{
			InitializeComponent();
			comboBox1.SelectedItem = "MySQL";
		}

		private void useDefault(object sender, EventArgs e)
		{
			if (checkBox2.Checked == true)
			{
				switch (comboBox1.SelectedItem.ToString())
				{
					case "MySQL":
						textBox1.Text = "root";
						textBox2.Text = "";
						break;
					case "MSSQL":
						IntegrateSecurity = true;
						break;
					case "Oracle":
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
				case "MySQL":
					MySqlConnection conn = new MySqlConnection("user=" + textBox1.Text + ";password=" + textBox2.Text + ";server=localhost;port=3306");
					try
					{
						conn.Open();
						conn.Close();
					}
					catch (MySqlException err) { MessageBox.Show(err.Message, "Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
					break;
				case "MSSQL":
					IntegrateSecurity = true;
					break;
				case "Oracle":
					//to be implemented
					break;
			}
			foreach (Control control in Controls) control.Enabled = true;
			Cursor = Cursors.Default;
		}
	}
}
