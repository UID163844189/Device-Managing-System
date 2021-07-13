using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.IO;

namespace Asset_Managing_System
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1(string type)
		{
			InitializeComponent();
			MessageBox.Show(type);
		}

		// 插入sql语句：
		//INSERT INTO `device` (`ID`, `Code`, `Name`, `Model`, `Vendor`, `Price`, `SN`)
		// VALUES ('107', 'GM0007', '固态硬盘盒', 'NVMe - USB3G2', 'UGREEN', '189', '');
		private void Add_Click(object sender, RoutedEventArgs e)
		{
			MySqlConnection connection = new MySqlConnection(File.ReadAllText("connectionInfo.txt"));
			bool succeed = false;
			try
			{
				connection.Open();
				string commandSentense = "insert into `device` (`ID`, `Code`, `Name`, `Model`, `Vendor`, `Price`, `SN`)"
					+ " values ('" + Int32.Parse(ID.Text) + "', '" + Code.Text + "', '" + Name.Text + "', '" +Model.Text + "', '" + Vendor.Text + "', '" + Price.Text + "', '" + SN.Text + "');";
				MySqlCommand command = new MySqlCommand(commandSentense, connection);
				//MessageBox.Show(commandSentense);
				MySqlDataReader reader = command.ExecuteReader();
				succeed = true;

				ID.Text = (Int32.Parse(ID.Text) + 1).ToString();

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				succeed = false;
			}
			finally
			{
				connection.Close();
				if (succeed)
				{
					//ID.Text = string.Empty;
					Code.Text = string.Empty;
					Name.Text = string.Empty;
					Model.Text = string.Empty;
					Vendor.Text = string.Empty;
					Price.Text = string.Empty;
					SN.Text = string.Empty;
					ID.Focus();
				}
			}

		}
	}
}
