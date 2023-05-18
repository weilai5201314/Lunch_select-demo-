using System.Windows;

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
        throw new System.NotImplementedException();
    }
}