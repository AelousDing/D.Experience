using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace D.Experience.Controls
{
    /// <summary>
    /// ImagePreview.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePreview : Window
    {
        Point dragStart;
        int imageAngle;

        public ImagePreview(string path)
        {
            InitializeComponent();
            img.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }
        /// <summary>
        /// 图片鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStart = e.GetPosition(root);
        }
        /// <summary>
        /// 图片鼠标滚轮滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var mosePos = e.GetPosition(img);

            var scale = transform.ScaleX * (e.Delta > 0 ? 1.2 : 1 / 1.2);
            scale = Math.Max(scale, 1);

            transform.ScaleX = scale;
            transform.ScaleY = scale;

            if (scale == 1)        //缩放率为1的时候，复位
            {
                translate.X = 0;
                translate.Y = 0;
            }
            else                //保持鼠标相对图片位置不变
            {
                var newPos = e.GetPosition(img);

                translate.X += (newPos.X - mosePos.X);
                translate.Y += (newPos.Y - mosePos.Y);
            }
        }
        /// <summary>
        /// 图片鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }
            //如果事件是有非image控件触发的这认为是在移动窗体
            if (e.OriginalSource is Image)
            {
                //防止双击打开的时候，将之后一下点击事件判断成鼠标按下，并触发移动事件，导致图片位置出现偏差
                if (dragStart.X == 0 && dragStart.Y == 0)
                {
                    return;
                }
                var current = e.GetPosition(root);
                //将坐标都转换成image坐标系下的坐标
                Point point0 = root.TranslatePoint(current, (UIElement)img);
                Point point1 = root.TranslatePoint(dragStart, (UIElement)img);

                bool isAboveHeight = img.ActualHeight * transform.ScaleY >= this.ActualHeight;//图片的实际高度是否大于窗体的高度，大于为true，小于false
                bool isAboveWidth = img.ActualWidth * transform.ScaleX >= this.ActualWidth;//图片的实际宽度是否大于窗体的宽度，大于为true，小于false

                bool isRotate = imageAngle == 90 || imageAngle == -90 || imageAngle == 270 || imageAngle == -270;//当左右旋转90°或者270°的时候，这个时候的图片宽高是反的

                if (isRotate)
                {
                    bool isAboveHeightRotate = img.ActualHeight * transform.ScaleY >= this.ActualWidth;//图片的实际高度是否大于窗体的高度，大于为true，小于false
                    bool isAboveWidthRotate = img.ActualWidth * transform.ScaleX >= this.ActualHeight;//图片的实际宽度是否大于窗体的宽度，大于为true，小于false

                    MoveImage(point0, point1, isAboveHeightRotate, isAboveWidthRotate);
                }
                else
                {
                    MoveImage(point0, point1, isAboveHeight, isAboveWidth);
                }

                dragStart = current;
            }
            else if (e.OriginalSource is Grid)
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// 移动图片
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <param name="isAboveHeight"></param>
        /// <param name="isAboveWidth"></param>
        private void MoveImage(Point point0, Point point1, bool isAboveHeight, bool isAboveWidth)
        {
            if (isAboveHeight && isAboveWidth)
            {
                translate.X += (point0.X - point1.X);
                translate.Y += (point0.Y - point1.Y);
            }
            else if (!isAboveHeight && isAboveWidth)
            {
                translate.X += (point0.X - point1.X);
            }
            else if (isAboveHeight && !isAboveWidth)
            {
                translate.Y += (point0.Y - point1.Y);
            }
            else
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// 图片左转事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imageAngle = (imageAngle - 90) % 360;
            rotate.Angle = imageAngle;
        }
        /// <summary>
        /// 图片右转事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            imageAngle = (imageAngle + 90) % 360;
            rotate.Angle = imageAngle;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private const int WM_NCHITTEST = 0x0084;
        private readonly int agWidth = 12; //拐角宽度  
        private readonly int bThickness = 4; // 边框宽度  
        private Point mousePoint = new Point(); //鼠标坐标

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.WndProc));
            }
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    this.mousePoint.X = (lParam.ToInt32() & 0xFFFF);
                    this.mousePoint.Y = (lParam.ToInt32() >> 16);

                    #region 测试鼠标位置

                    // 窗口左上角  
                    if (this.mousePoint.Y - this.Top <= this.agWidth
                       && this.mousePoint.X - this.Left <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPLEFT);
                    }
                    // 窗口左下角      
                    else if (this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth
                       && this.mousePoint.X - this.Left <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                    }
                    // 窗口右上角  
                    else if (this.mousePoint.Y - this.Top <= this.agWidth
                       && this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPRIGHT);
                    }
                    // 窗口右下角  
                    else if (this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth
                       && this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                    }
                    // 窗口左侧  
                    else if (this.mousePoint.X - this.Left <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTLEFT);
                    }
                    // 窗口右侧  
                    else if (this.ActualWidth + this.Left - this.mousePoint.X <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTRIGHT);
                    }
                    // 窗口上方  
                    else if (this.mousePoint.Y - this.Top <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOP);
                    }
                    // 窗口下方  
                    else if (this.ActualHeight + this.Top - this.mousePoint.Y <= this.bThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOM);
                    }
                    else // 窗口移动   
                    {
                        handled = false;//结束与win32的交互，否则无边框窗体其他动作都做不了
                        return IntPtr.Zero;
                    }
                    #endregion
            }
            return IntPtr.Zero;
        }
    }
    public enum HitTest : int
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTSIZE = HTGROWBOX,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTREDUCE = HTMINBUTTON,
        HTZOOM = HTMAXBUTTON,
        HTSIZEFIRST = HTLEFT,
        HTSIZELAST = HTBOTTOMRIGHT,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21,
    }
}
