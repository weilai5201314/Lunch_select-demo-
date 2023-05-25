using System;
using System.Windows;
using System.Windows.Controls;

//引用数据库
using MySql.Data.MySqlClient;

namespace Lunch_Select;

public partial class UserAdmin : Window
{
    public UserAdmin()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 全局组件
    /// </summary>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // UserAdmin UserAdmin = (UserAdmin)sender;
        double width = Useradmin.ActualWidth;
        double height = Useradmin.ActualHeight;

        // 设置按钮的宽度和高度为窗口的1/3
        Button btn = Useradmin.Children[0] as Button;
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

    /// 
    /// 跳转到主页面函数
    /// <param name="window"></param>
    private void Jump_Mainwindos()
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.UserId = Account.Text;
        mainWindow.Show();
        UserAdmin.GetWindow(this).Close();
    }

    /// <summary>
    /// 验证用户的数据库操作
    /// </summary>
    private void CheckUserAccount()
    {
        string connStr = "server=localhost;port=3306;user=root;database=test;password=12345678;";
        MySqlConnection conn = new MySqlConnection(connStr);
        // MessageBox.Show("Connecting Mysql......", "提示");
        try
        {
            conn.Open();
            // MessageBox.Show("Success connecting Mysql!", "提示");

            //  开始数据库操作
            string account = Account.Text;
            string password = Password.Text;
            string query = $"SELECT * FROM users WHERE UserName = '{account}' AND Password = '{password}'";
            //  判断是否为空
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("输入不能为空.");
                return;
            }

            //  开始判断是否有用户
            // query=$"SELECT UserName FROM test.菜单表 WHERE 口味 = '{selectedFlavor}'"
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();

            // 检查是否有匹配的记录
            if (reader.HasRows)
            {
                // 存在匹配的记录，登录成功
                MessageBox.Show("登录成功！", "提示");
                // MainWindow mainWindow = new MainWindow();
                // // mainWindow.UserAdminInstance = this;
                // mainWindow.userid = account;
                //  开始跳转主页面
                Jump_Mainwindos();
            }
            else
            {
                // 没有匹配的记录，登录失败
                MessageBox.Show("账号或密码错误，请重新输入。", "提示");
                //  清空输入框
                // Account.Text = "";
                Password.Text = "";
            }

            // 关闭读取器
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    ///
    /// 登录按钮
    private void Jump_LogIn(object sender, RoutedEventArgs e)
    {
        CheckUserAccount();
        //  开始跳转
        //Jump_Mainwindos();
    }

    /// <summary>
    /// 前往注册页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Jump_SignUp(object sender, RoutedEventArgs e)
    {
        this.IsEnabled = false;
        SignUp signup = new SignUp();
        signup.Closed += (sender, e) => { this.IsEnabled = true; };
        signup.Show();
        //UserAdmin.GetWindow(this).Close();
    }

    /// <summary>
    /// 前往找回密码页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Jump_FindPass(object sender, RoutedEventArgs e)
    {
        FindPassWord findpass = new FindPassWord();
        findpass.Show();
        UserAdmin.GetWindow(this).Close();
    }
}