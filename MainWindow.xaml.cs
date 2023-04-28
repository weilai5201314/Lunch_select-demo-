// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

using System;
using System.Windows;
using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;
//引用数据库
using MySql.Data.MySqlClient;

namespace Lunch_Select
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 设置了全屏的组件适配
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取窗口的宽度和高度
            Button btn = MyCanvas.Children[0] as Button; //似乎有小小的问题，但是无法解决
            if (btn != null)
            {
                double width = MyCanvas.ActualWidth;
                double height = MyCanvas.ActualHeight;
                Canvas.SetLeft(btn, (width - btn.ActualWidth) / 2);
                Canvas.SetTop(btn, (height - btn.ActualHeight) / 2);
            }
            else
            {
                // 处理Children[0]为空的情况
            }
        }

        //  设置点击选菜按钮之后的跳转函数
        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello world.恭喜进入选菜按钮事件");
            //  开始连接数据库
            string connStr =
                "server=localhost;port=3306;user=root;database=test;port=3306;password=12345678;";
            MessageBox.Show("Connecting to MySQL..."); // 添加这行代码
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MessageBox.Show("Success connecting Mysql !");

                //  开始数据库的操作
                //  读取信息
                // string query = "SELECT 菜名,口味 FROM test.菜单表";
                // string result1 = "";
                // string result2 = "";
                // MySqlCommand cmd = new MySqlCommand(query, conn);
                // MySqlDataReader reader = cmd.ExecuteReader();
                // while (reader.Read())
                // {
                //     result1 += reader.GetString(0) + Environment.NewLine;
                //     result2 += reader.GetString(1) + Environment.NewLine;
                // }
                // reader.Close();
                // MyTextBox1.Text = result1;
                // MyTextBox2.Text = result2;

                //  写入操作
                string menuName = MyTextBox1.Text;
                string flavor = MyTextBox2.Text;
                string insertQuery = "INSERT INTO test.菜单表 (菜名, 口味) VALUES ('" + menuName + "', '" + flavor + "')";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Console.WriteLine(exception);
                throw;
            }
        }
    }
}