using D.Experience.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D.Experience.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ImagePreview preview = new ImagePreview(@"C:\Users\dingjm\Desktop\111.jpg");
            //preview.ShowInTaskbar = false;
            //preview.ShowDialog();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn != null && btn.Content != null)
            {
                frame.Source = new Uri("/Views/" + btn.Content.ToString() + ".xaml", UriKind.RelativeOrAbsolute);
            }
        }
    }
}
