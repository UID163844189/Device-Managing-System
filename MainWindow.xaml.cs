using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Asset_Managing_System
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			RefreshStatusBar();
		}

		void RefreshStatusBar()
		{
			try
			{
				ConnectedDB.Text = File.ReadAllText("connectionInfo.txt");
			}
			finally
			{
				
			}

		}

		private void ConnectDB_Click(object sender, RoutedEventArgs e)
		{
			Connect form = new Connect();
			form.Show();
		}

		private void FormatDB_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Refresh_Click(object sender, RoutedEventArgs e)
		{
			MySqlConnection connection = new MySqlConnection(File.ReadAllText("connectionInfo.txt"));
			Status.Text = "正在连接数据库……";
			try
			{
				connection.Open();
				Status.Text = "正在读取数据库……";
				MySqlCommand command = new MySqlCommand("select * from device", connection);
				MySqlDataReader reader = command.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象

				Status.Text = "正在解析数据……";
				DeviceList.Items.Clear();
				while (reader.Read())
				{
					/*
					int index = this.dataGridView1.Rows.Add();

					this.dataGridView1.Rows[index].Cells[0].Value = reader.GetString("name");
					this.dataGridView1.Rows[index].Cells[1].Value = reader.GetString("describe");
					this.dataGridView1.Rows[index].Cells[2].Value = reader.GetString("price");
					this.dataGridView1.Rows[index].Cells[3].Value = reader.GetInt32("salenumber");
					*/

					//ListViewItem item = new ListViewItem();

					Device device = new Device
					{
						ID = reader.GetInt32("ID"),
						Code = reader.GetString("Code"),
						Name = reader.GetString("Name"),
						Model = reader.GetString("Model"),
						Vendor = reader.GetString("Vendor"),
						Price = reader.GetFloat("Price"),
						SN=reader.GetString("SN")
					};
					DeviceList.Items.Add(device);

				}
				Status.Text = "就绪";

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				Status.Text = "就绪";

			}
			finally
			{
				connection.Close();
			}

		}

		private void AddDevice_Click(object sender, RoutedEventArgs e)
		{
			string type = "add";
			Window1 window = new Window1(type);
			window.Show();
		}
	}



	//如果好用，请收藏地址，帮忙分享。
	public class Device
	{
		public int ID { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 型号
		/// </summary>
		public string Model { get; set; }
		/// <summary>
		/// 厂商
		/// </summary>
		public string Vendor { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public float Price { get; set; }
		/// <summary>
		/// 序列号
		/// </summary>
		public string SN { get; set; }
	}
}
