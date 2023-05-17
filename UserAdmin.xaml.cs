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
    private void Jump_Mainwindos()
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        UserAdmin.GetWindow(this).Close();
    }

    ///
    /// 登录按钮
    private void Jump_LogIn(object sender, RoutedEventArgs e)
    {
        Jump_Mainwindos();
    }
}