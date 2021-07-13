using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.IO;

namespace Asset_Managing_System
{
	/// <summary>
	/// Connect.xaml 的交互逻辑
	/// </summary>
	public partial class Connect : Window
	{
		public Connect()
		{
			InitializeComponent();
		}

		private void ConnectDB_Click(object sender, RoutedEventArgs e)
		{
			//String connetStr = "server=127.0.0.1;port=3306;user=root;password=123; database=vs;";
			string Str = "server=" + Hostname.Text + "; port=" + DBPort.Text + "; user=" + DBUsername.Text + "; password=" + DBPasswd.Password + "; database=" + DBName.Text + ";";
			//usr:用户名，password：数据库密码，database：数据库名
			MySqlConnection conn = new MySqlConnection(Str);
			try
			{
				conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
				File.WriteAllText("connectionInfo.txt", Str);
				MessageBox.Show("已经建立连接，连接永久有效，可以删除connectionInfo.txt取消连接");
				//Hide();

			}
			catch (MySqlException ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}

