﻿/// 找回密码
/// 输入新密码页面
using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Lunch_Select;

public partial class AlterPassWord : Window
{
    /// <summary>
    /// 声明处
    /// </summary>
    public string UserId; /// 存储用户ID

    public AlterPassWord()
    {
        InitializeComponent();
    }


    /// <summary>
    /// 检查输入正确性
    /// 检验输入是否为空，或者是否相同
    /// <returns></returns>
    private int Check_Newpass()
    {
        string firstpass = FirstAlter.Text;
        string secondpass = SecondAlter.Text;
        //  判断是否为空
        if (string.IsNullOrEmpty(firstpass) || string.IsNullOrEmpty(secondpass))
        {
            MessageBox.Show("请全部填写.", "修改密码");
            return 0;
        }

        //  判断两次密码是否相同
        if (firstpass != secondpass)
        {
            MessageBox.Show("两次输入不相同，请重新确认密码。");
            return 0;
        }

        //  全部通过
        return 1;
    }


    

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_AlterPass(object sender, RoutedEventArgs e)
    {
        int i = Check_Newpass();
        if (i == 0)
        {
            // MessageBox.Show("")
            return;
        }

        // 输入通过，开始写入数据库
        string connStr = "server=localhost;port=3306;user=root;database=test;password=12345678;";
        string account = UserId; //读取 FindPassWord 页面的 Account text值

        string newPass = FirstAlter.Text;
        string insertQuery = "UPDATE users SET Password = @NewPassword WHERE UserName = @Account";
        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            //  开始链接数据库

            conn.Open();
            using (MySqlCommand command = new MySqlCommand(insertQuery, conn))
            {
                command.Parameters.AddWithValue("@NewPassword", newPass);
                command.Parameters.AddWithValue("@Account", account);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("密码修改成功！");
                }
            }
            
            
            AlterPassWord.GetWindow(this).Close(); //返回
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
        finally
        {
            conn.Close();
        }
    }
}