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
		object conn;
		ushort type;
		public DatabaseSelector(object con, ushort db)
		{
			conn = con;
			type = db;
			InitializeComponent();
		}
	}
}
