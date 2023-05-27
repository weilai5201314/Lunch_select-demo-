/// 主页面
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
using System.Windows.Threading;
//引用数据库
using MySql.Data.MySqlClient;


namespace Lunch_Select
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 定义的各种类的列表
        /// 一般定义了要去MainWindow里面添加
        public List<string> FlavorOptions { get; set; } // 下拉框的数据源
        public string UserId;         // 存储登录ID

        public MainWindow( string userId)
        {
            InitializeComponent();
           
            //  初始化下拉框
            FlavorOptions = new List<string>(); // 初始化下拉框数据源
            FlavorComboBox.ItemsSource = FlavorOptions; // 将数据源绑定到ComboBox
            FlavorOptions.Add("全部");
            InitializeComboBox(userId);// 初始化下拉框函数
            
        }
        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 设置了全屏的组件适配
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // InitializeComboBox();
            // 获取窗口的宽度和高度
            
            // 获取窗口的宽度和高度
            double width = mainwindow.ActualWidth;
            double height = mainwindow.ActualHeight;

            // 设置按钮的宽度和高度为窗口的1/3
            Button btn = mainwindow.Children[0] as Button;
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


        /// <summary>
        /// 跳转到加菜菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddMenu_Click(object sender, RoutedEventArgs e)
        {
            // this.IsEnabled = false;
            AddMenu testmenu = new AddMenu();
            testmenu.UserId = UserId;
            testmenu.Show();
            // testmenu.Closed += (sender, e) => { this.IsEnabled = true; };
            MainWindow.GetWindow(this).Close();
        }

        /// <summary>
        /// 初始化下拉框
        /// 读取数据库的相关操作
        private void InitializeComboBox(string userid)
        {
            string connStr = "server=localhost;port=3306;user=root;database=test;password=12345678;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    // MessageBox.Show("Connecting Mysql......", "提示");
                    conn.Open();
                    // 查询口味选项
                    // string query = "SELECT DISTINCT 口味 FROM test.菜单表 ";
                    string query = "SELECT DISTINCT 菜单表.口味 FROM 菜单表  INNER JOIN usermenu ON 菜单表.id = usermenu.MenuID INNER JOIN users ON usermenu.UserID = users.UserName WHERE users.UserName = @UserID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    /// 循环添加口味
                    while (reader.Read())
                    {
                        string flavor = reader.GetString(0);
                        FlavorOptions.Add(flavor);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                }
            }
        }
        
        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //  设置点击选菜按钮之后的函数
        //  包括了数据库的操作
        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            
            Main_menu.Text = "";    //  清空展示框
            string connStr = "server=localhost;port=3306;user=root;database=test;password=12345678;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                // MessageBox.Show("Success connecting Mysql.", "提示");

                string query;
                if (FlavorComboBox.SelectedItem != null)
                {
                    if (FlavorComboBox.SelectedItem != "全部")    //  判断是全部还是特定口味
                    {
                        string selectedFlavor = FlavorComboBox.SelectedItem.ToString();
                        query = $"SELECT 菜名 FROM test.菜单表 WHERE 口味 = '{selectedFlavor}'AND id IN (SELECT MenuID FROM usermenu WHERE UserID = '{UserId}')";
                    }
                    else
                    {
                        query = $"SELECT 菜名 FROM test.菜单表 where  id IN (SELECT MenuID FROM usermenu WHERE UserID = '{UserId}')";
                    }
                }
                else
                {
                    query = $"SELECT 菜名 FROM test.菜单表 where  id IN (SELECT MenuID FROM usermenu WHERE UserID = '{UserId}')";
                }
                /// 开始查询
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<string> menuOptions = new List<string>();
                /// 判断是否读取到数据计数器，及获取菜名
                while (reader.Read())
                {
                    menuOptions.Add(reader.GetString(0));
                }

                reader.Close();
                /// 赋值到文本框
                if (menuOptions.Count > 0)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(0, menuOptions.Count);
                    Main_menu.Text = menuOptions[randomIndex];
                }
                else
                {
                    Main_menu.Text = "没有这道菜。";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mainwindow：button_main错误");
            }
        }

        /// <summary>
        /// 辅助下拉框的点击函数
        /// 下拉框选择口味之后，直接自动点击按钮
        private void FlavorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonMain_Click(sender, e);
        }
        
        
        ///
    }
}