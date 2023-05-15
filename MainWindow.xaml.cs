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

        // public static void Navigate(string pageName)
        // {
        //     Uri uri = new Uri(pageName, UriKind.Relative);
        //  
        //     object newPage = Application.LoadComponent(uri);
        //
        //     ((MainWindow)Application.Current.MainWindow).Content = newPage;
        // }
        //
        // private void ButtonAddMenu_Click(object sender, RoutedEventArgs e)
        // {
        //     Navigate("addMenu.xaml");
        // }
        private void ButtonAddMenu_Click(object sender, RoutedEventArgs e)
        {
            AddMenu testmenu = new AddMenu();
            testmenu.Show();
            Window.GetWindow(this).Close();
        }


       

       
        
       
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 设置了全屏的组件适配
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取窗口的宽度和高度

            // 获取窗口的宽度和高度
            double width = MyCanvas.ActualWidth;
            double height = MyCanvas.ActualHeight;

            // 设置按钮的宽度和高度为窗口的1/3
            Button btn = MyCanvas.Children[0] as Button;
            if (btn != null)
            {
                btn.Width = width / 3;
                btn.Height = height / 3;

                // 设置按钮的位置为窗口中心
                Canvas.SetLeft(btn, (width - btn.Width) / 2);
                Canvas.SetTop(btn, (height - btn.Height) / 2);
            }
            else
            {
                // 处理Children[0]为空的情况
            }
        }

        
      
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //  设置点击选菜按钮之后的跳转函数
        //  包括了数据库的操作
        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            //  MessageBox.Show("Hello world.恭喜进入选菜按钮事件");
            //  进来先清空聊天框
            MyTextBox1.Text = "";
            MyTextBox2.Text = "";
            
            //  开始连接数据库
            string connStr =
                "server=localhost;port=3306;user=root;database=test;port=3306;password=12345678;";

            MessageBox.Show("Connecting Mysql......","提示");


            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MessageBox.Show("Success connecting Mysql.","提示");
                
                //  先获取随机数范围
                string getLastIDQuery = "SELECT MAX(编号) FROM test.菜单表";
                MySqlCommand getLastIDCmd = new MySqlCommand(getLastIDQuery, conn);
                int lastID = Convert.ToInt32(getLastIDCmd.ExecuteScalar());
                //  生成随机数
                Random random = new Random();
                int randomID = random.Next(1, lastID + 1); 
                //  进入数据库开始查询
                string query = $"SELECT 菜名, 口味 FROM test.菜单表 WHERE 编号 = {randomID}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                //  开始赋值
                if (reader.Read())
                {
                    MyTextBox1.Text += reader.GetString(0) + Environment.NewLine;
                    MyTextBox2.Text += reader.GetString(1) + Environment.NewLine;
                    reader.Close();
                   
                }
                
            }
            catch (Exception ex)
            {
                // Console.WriteLine(exception);
                MessageBox.Show(ex.Message, "错误");
            }
        }
        
        



    }
}