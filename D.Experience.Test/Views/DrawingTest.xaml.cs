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
using D.Experience.CustomVisul;

namespace D.Experience.Test.Views
{
    /// <summary>
    /// DrawingTest.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingTest : UserControl
    {
        public DrawingTest()
        {
            InitializeComponent();
            EllipseTextVisual ellipse = new EllipseTextVisual();
            ellipse.RadiusX = 30;
            ellipse.RadiusY = 30;
            ellipse.Center = new Point(100, 100);
            ellipse.Text = "天下科技";
            ellipse.OnRender(Brushes.Blue, new Pen(Brushes.SteelBlue, 3));
            container.AddVisual(ellipse);
        }
    }
}
