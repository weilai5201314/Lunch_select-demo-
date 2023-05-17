using System;
using System.Windows;
using System.Windows.Controls;
// using System.Windows.Controls.Primitives;
// using System.Windows.Documents;
// using System.Windows.Media; 
//引用数据库
using MySql.Data.MySqlClient;


namespace Lunch_Select;

public partial class AddMenu : Window
{
    public AddMenu()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 返回主菜单按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        AddMenu.GetWindow(this).Close();
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
    private void ButtonAdd_Click(object sender, RoutedEventArgs e)
    {
        //MessageBox.Show("Hello world.恭喜进入选菜按钮事件");
        //  开始连接数据库
        string connStr =
            "server=localhost;port=3306;user=root;database=test;port=3306;password=12345678;";

        MessageBox.Show("Connecting Mysql......", "提示");


        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            // MessageBox.Show("Success connecting Mysql!", "提示");

            //  开始数据库的操作

            //  写入操作
            string menuName = MyTextBox1.Text;
            string flavor = MyTextBox2.Text;
            // 判断是否为空
            if (string.IsNullOrEmpty(menuName) || string.IsNullOrEmpty(flavor))
            {
                MessageBox.Show("输入不能为空.");
                return;
            }
            // // 开始插入
            string insertQuery = "INSERT INTO test.菜单表 (菜名, 口味) VALUES (@MenuName, @Flavor); SELECT LAST_INSERT_ID();";
            MySqlCommand writing = new MySqlCommand(insertQuery, conn);
            writing.Parameters.AddWithValue("@MenuName", menuName);
            writing.Parameters.AddWithValue("@Flavor", flavor);
            int newID = Convert.ToInt32(writing.ExecuteScalar());
            MessageBox.Show($"成功导入菜品:\n- 编号:{newID}\n- 菜名:{menuName}\n- 口味:{flavor}。", "写入操作");
            
            //  清空输入框
            MyTextBox1.Text = "";
            MyTextBox2.Text = "";
        }
        catch (Exception ex)
        {
            // Console.WriteLine(exception);
            MessageBox.Show(ex.Message, "Addmenu:ButtonAdd_Click错误");
            throw;
        }
    }
    
    
}