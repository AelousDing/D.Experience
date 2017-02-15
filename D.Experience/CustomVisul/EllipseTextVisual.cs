using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace D.Experience.CustomVisul
{
    public class EllipseTextVisual : DrawingVisual, IVisualRender
    {
        public double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadiusX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register("RadiusX", typeof(double), typeof(EllipseTextVisual), new PropertyMetadata(default(double)));



        public double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadiusY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register("RadiusY", typeof(double), typeof(EllipseTextVisual), new PropertyMetadata(default(double)));



        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(EllipseTextVisual), new PropertyMetadata(new Point()));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EllipseTextVisual), new PropertyMetadata(string.Empty));


        public void OnRender(Brush brush, Pen pen)
        {
            DrawingContext dc = this.RenderOpen();
            //绘制图形
            dc.DrawEllipse(brush, pen, Center, RadiusX, RadiusY);
            FormattedText text = new FormattedText(Text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Microsoft Yahei"), 15, Brushes.Red);
            dc.DrawText(text, new Point(Center.X - RadiusX * 0.8, Center.Y - RadiusY * 0.8));
            dc.Close();
        }
    }
}
