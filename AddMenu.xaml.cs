using System.Windows;

namespace Lunch_Select;

public partial class AddMenu : Window
{
    public AddMenu()
    {
        InitializeComponent();
    }
    
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Window.GetWindow(this).Close();
    }
}