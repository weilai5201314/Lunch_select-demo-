using System.Windows;

namespace Lunch_Select;

public partial class UserCenter : Window
{
    /// <summary>
    /// 声明变量列
    public string UserId;         // 存储登录ID
    
    public UserCenter(string userId)
    {
        InitializeComponent();
        ID.Text = userId;
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_SignOut(object sender, RoutedEventArgs e)
    {
        UserAdmin useradmin = new UserAdmin();
        useradmin.Show();
        UserCenter.GetWindow(this).Close();
    }

    /// <summary>
    /// 跳转选菜页面
    /// </summary>
    private void Jump_MainWindow(object sender, RoutedEventArgs e)
    {
        MainWindow mainwindow = new MainWindow(UserId);
        mainwindow.UserId = UserId;
        mainwindow.Show();
        UserCenter.GetWindow(this).Close();
    }
    
    /// <summary>
    /// 跳转到修改密码页面
    /// </summary>
    private void Jump_FindPass(object sender, RoutedEventArgs e)
    {
        
    }
    
    ///
}