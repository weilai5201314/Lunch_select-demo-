/// 加菜页面
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
    /// <summary>
    /// 声明列表
    /// </summary>
    public string UserId; //全局用户ID

    public AddMenu()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 返回主菜单按钮
    /// </summary>
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow(UserId);
        mainWindow.UserId = UserId;
        mainWindow.Show();
        AddMenu.GetWindow(this).Close();
    }

    /// <param name="sender"></param>
    /// <param name="e"></param>
    // 设置了全屏的组件适配
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // 获取窗口的宽度和高度
        double width = Addmenu.ActualWidth;
        double height = Addmenu.ActualHeight;

        // 设置按钮的宽度和高度为窗口的1/3
        Button btn = Addmenu.Children[0] as Button;
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
            "server=localhost;port=3306;user=root;database=test;password=12345678;";

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

            // 判断当前用户是否已经添加了该菜品
            string checkUserMenuQuery =
                "SELECT COUNT(*) FROM test.usermenu WHERE UserID = @UserID AND MenuID IN (SELECT id FROM test.菜单表 WHERE 菜名 = @MenuName)";
            MySqlCommand checkUserMenuCmd = new MySqlCommand(checkUserMenuQuery, conn);
            checkUserMenuCmd.Parameters.AddWithValue("@UserID", UserId);
            checkUserMenuCmd.Parameters.AddWithValue("@MenuName", menuName);
            int userCount = Convert.ToInt32(checkUserMenuCmd.ExecuteScalar());
            if (userCount > 0)
            {
                MessageBox.Show("您已经添加了该菜品，请勿重复添加。", "提示");
                return;
            }

            // 判断是否已经有这道菜
            string checkQuery = "SELECT COUNT(*) FROM test.菜单表 WHERE 菜名 = @MenuName";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@MenuName", menuName);
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count > 0)
            {
                // 菜单表中已经存在该菜品，直接关联菜名和账户ID
                string getMenuIdQuery = "SELECT id FROM test.菜单表 WHERE 菜名 = @MenuName";
                MySqlCommand getMenuIdCmd = new MySqlCommand(getMenuIdQuery, conn);
                getMenuIdCmd.Parameters.AddWithValue("@MenuName", menuName);
                int menuId = Convert.ToInt32(getMenuIdCmd.ExecuteScalar());

                string insertUserMenuQuery = "INSERT INTO test.usermenu (UserID, MenuID) VALUES (@UserID, @MenuID)";
                MySqlCommand insertUserMenuCmd = new MySqlCommand(insertUserMenuQuery, conn);
                insertUserMenuCmd.Parameters.AddWithValue("@UserID", UserId); // 使用当前用户的ID
                insertUserMenuCmd.Parameters.AddWithValue("@MenuID", menuId); // 使用菜品的ID
                insertUserMenuCmd.ExecuteNonQuery();

                MessageBox.Show("成功关联菜名。", "提示");
            }
            else
            {
                // 菜单表中不存在该菜品，先添加到菜单表，再关联菜名和账户ID
                string insertMenuQuery = "INSERT INTO test.菜单表 (菜名, 口味) VALUES (@MenuName, @Flavor)";
                MySqlCommand insertMenuCmd = new MySqlCommand(insertMenuQuery, conn);
                insertMenuCmd.Parameters.AddWithValue("@MenuName", menuName);
                insertMenuCmd.Parameters.AddWithValue("@Flavor", flavor);
                insertMenuCmd.ExecuteNonQuery();

                // 获取新插入菜品的ID
                string getMenuIdQuery = "SELECT LAST_INSERT_ID()";
                MySqlCommand getMenuIdCmd = new MySqlCommand(getMenuIdQuery, conn);
                int menuId = Convert.ToInt32(getMenuIdCmd.ExecuteScalar());

                string insertUserMenuQuery = "INSERT INTO test.usermenu (UserID, MenuID) VALUES (@UserID, @MenuID)";
                MySqlCommand insertUserMenuCmd = new MySqlCommand(insertUserMenuQuery, conn);
                insertUserMenuCmd.Parameters.AddWithValue("@UserID", UserId); // 使用当前用户的ID
                insertUserMenuCmd.Parameters.AddWithValue("@MenuID", menuId); // 使用新插入的菜品的ID
                insertUserMenuCmd.ExecuteNonQuery();

                MessageBox.Show("成功添加菜品。", "提示");
            }
            // 清空输入框
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