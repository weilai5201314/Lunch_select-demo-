using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lunch_Select
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 设置了全屏的组件适配
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取窗口的宽度和高度
            Button btn = MyCanvas.Children[0] as Button;//似乎有小小的问题，但是无法解决
            if (btn != null)
            {
                double width = MyCanvas.ActualWidth;
                double height = MyCanvas.ActualHeight;
                Canvas.SetLeft(btn, (width - btn.ActualWidth) / 2);
                Canvas.SetTop(btn, (height - btn.ActualHeight) / 2);
            }
            else
            {
                // 处理Children[0]为空的情况
            }
        }

        //  设置点击选菜按钮之后的跳转函数
        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello world.恭喜完成选菜按钮事件");
        }
    }
}