/// 注册页面
using System;
using System.Windows;
using System.Windows.Documents.Serialization;
//引用数据库
using MySql.Data.MySqlClient;

namespace Lunch_Select;

public partial class SignUp : Window
{
    public SignUp()
    {
        InitializeComponent();
    }


    /// <summary>
    /// 跳转到登录页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Jump_UserAdmin(object sender, RoutedEventArgs e)
    {
        UserAdmin useradmin = new UserAdmin();
        useradmin.Show();
        SignUp.GetWindow(this).Close();
    }

    /// <summary>
    /// 添加新用户
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_AddUser(object sender, RoutedEventArgs e)
    {
        string connStr =
            "server=localhost;port=3306;user=root;database=test;port=3306;password=12345678;";
        MessageBox.Show("Connecting Mysql......", "注册");
        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            // MessageBox.Show("Success connecting Mysql!", "注册");

            //  开始数据库操作
            string username = Account.Text;
            string password = Password.Text;
            string tip = Tip.Text;
            string checkQuery = $"SELECT * FROM users WHERE UserName = '{username}'";

            // 判断是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(tip))
            {
                MessageBox.Show("请全部填写。");
                return;
            }

            // 判断是否已注册
            MySqlCommand command = new MySqlCommand(checkQuery, conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                MessageBox.Show("当前账号已注册，请重新设置账号。", "注册");
                return;
            }

            reader.Close();

            //   开始写入人员信息
            string insertQuery =
                "INSERT INTO users (UserName, Password, tip) VALUES (@username, @password, @tip); SELECT LAST_INSERT_ID();";
            using (command = new MySqlCommand(insertQuery, conn))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@tip", tip);
                int newID = Convert.ToInt32(command.ExecuteScalar());
                MessageBox.Show($"成功插入用户:\n- 编号:{newID}\n- 用户名:{username}\n- 提示:{tip}", "插入操作");
            }

            //   清空聊天框
            Account.Text = "";
            Password.Text = "";
            Tip.Text = "";
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "SignUp:Button_AddUser错误");
            throw;
        }
    }
    
    
    ///
}