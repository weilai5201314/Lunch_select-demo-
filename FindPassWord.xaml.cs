using System.Windows;
using System;
//引用数据库
using MySql.Data.MySqlClient;

namespace Lunch_Select;

public partial class FindPassWord : Window
{
    public FindPassWord()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 用户验证
    /// 验证提示的正确性
    private void Button_CheckUser(object sender, RoutedEventArgs e)
    {
        string connStr = "server=localhost;port=3306;user=root;database=test;port=3306;password=12345678;";
        MySqlConnection conn = new MySqlConnection(connStr);
        MessageBox.Show("Connecting Mysql......", "找回密码");
        try
        {
            conn.Open();
            // MessageBox.Show("Success connecting Mysql!", "找回密码");
            //  开始赋值
            string account = Account.Text;
            string tip = Tip.Text;
            string checkQuery = "SELECT * FROM users WHERE UserName=@Account AND tip = @Tip";
            string insertQuery = "INSERT INTO users (Password) VALUES (@Password); SELECT LAST_INSERT_ID();";

            //  判断是否为空
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(tip))
            {
                MessageBox.Show("请填写全部内容", "找回密码");
                return;
            }

            //  判断是否存在
            using (MySqlCommand command = new MySqlCommand(checkQuery, conn))
            {
                command.Parameters.AddWithValue("@Account", account);
                command.Parameters.AddWithValue("@Tip", tip);
                MySqlDataReader reader = command.ExecuteReader();
                // 验证失败
                if (!reader.HasRows)
                {
                    MessageBox.Show("账号或提示错误，请重新输入", "找回密码");
                    //  清空
                    Account.Text = "";
                    Tip.Text = "";
                    return;
                }

                reader.Close();
            }

            //  开始跳转修改密码
            AlterPassWord alterPassWord = new AlterPassWord();
            alterPassWord.FindPassWordInstance = this;
            Jump_AlterPass(alterPassWord);
            //  修改完之后清空输入
            // Account.Text = "";
            // Tip.Text = "";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    /// <summary>
    /// 返回登录页面
    /// </summary>
    private void Jump_UserAdmin(object sender, RoutedEventArgs e)
    {
        UserAdmin useradmin = new UserAdmin();
        useradmin.Show();
        FindPassWord.GetWindow(this).Close();
    }

    /// <summary>
    /// 跳转修改密码页面
    /// </summary>
    private void Jump_AlterPass(AlterPassWord alterPassWord)
    {
        this.IsEnabled = false; //禁用原来的窗口
        // 订阅新窗口的 Closed 事件，在窗口关闭时恢复原始窗口的可用状态
        alterPassWord.Closed += (sender, e) => { this.IsEnabled = true; };
        alterPassWord.Show();
    }
}