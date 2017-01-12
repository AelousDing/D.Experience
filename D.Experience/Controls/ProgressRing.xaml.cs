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

namespace D.Experience.Controls
{
    /// <summary>
    /// ProgressRing.xaml 的交互逻辑
    /// 模块编号：公用控件
    /// 作用：加载等待控件
    /// 作者:代码来自网络
    /// 编写日期：2017-01-09
    /// </summary>
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class ProgressRing : Control
    {
        #region 字段
        private List<Action> _deferredActions = new List<Action>();
        #endregion

        #region 构造函数
        static ProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
            VisibilityProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(new PropertyChangedCallback(
                (ringObject, e) =>
                {
                    if (e.NewValue != e.OldValue)
                    {
                        var ring = (ProgressRing)ringObject;
                        if ((Visibility)e.NewValue != Visibility.Visible)
                        {
                            ring.SetCurrentValue(ProgressRing.IsActiveProperty, false);
                        }
                        else
                        {
                            ring.IsActive = true;
                        }
                    }
                })));
        }
        public ProgressRing()
        {
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            BindableWidth = ActualWidth;
        }
        #endregion

        #region 依赖属性
        public double BindableWidth
        {
            get { return (double)GetValue(BindableWidthProperty); }
            set { SetValue(BindableWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindableWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindableWidthProperty =
            DependencyProperty.Register("BindableWidth", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double), BindableWidthCallback));

        private static void BindableWidthCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing ring = d as ProgressRing;
            if (ring == null)
            {
                return;
            }
            var action = new Action(() =>
            {
                ring.SetEllipseDiameter((double)e.NewValue);
                ring.SetEllipseOffset((double)e.NewValue);
                ring.SetMaxSideLength((double)e.NewValue);
            });
            if (ring._deferredActions != null)
            {
                ring._deferredActions.Add(action);
            }
            else
            {
                action();
            }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressRing), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActiveChanged));

        private static void IsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing ring = d as ProgressRing;
            if (ring == null)
            {
                return;
            }
            ring.UpdateActiveState();
        }


        public bool IsLarge
        {
            get { return (bool)GetValue(IsLargeProperty); }
            set { SetValue(IsLargeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLarge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLargeProperty =
            DependencyProperty.Register("IsLarge", typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, IsLargeChangedCallback));

        private static void IsLargeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing ring = d as ProgressRing;
            if (ring == null)
            {
                return;
            }
            ring.UpdateLargeState();
        }


        public double MaxSideLength
        {
            get { return (double)GetValue(MaxSideLengthProperty); }
            set { SetValue(MaxSideLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxSideLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxSideLengthProperty =
            DependencyProperty.Register("MaxSideLength", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double)));


        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EllipseDiameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipseDiameterProperty =
            DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double)));



        public Thickness EllipseOffset
        {
            get { return (Thickness)GetValue(EllipseOffsetProperty); }
            set { SetValue(EllipseOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EllipseOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipseOffsetProperty =
            DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(ProgressRing), new PropertyMetadata(default(Thickness)));

        #endregion

        #region 方法
        private void SetMaxSideLength(double width)
        {
            MaxSideLength = width <= 20 ? 20 : width;
        }

        private void SetEllipseDiameter(double width)
        {
            EllipseDiameter = width / 8;
        }

        private void SetEllipseOffset(double width)
        {
            EllipseOffset = new Thickness(0, width / 2, 0, 0);
        }
        private void UpdateActiveState()
        {
            Action action;
            if (IsActive)
            {
                action = () => VisualStateManager.GoToState(this, "Active", true);
            }
            else
            {
                action = () => VisualStateManager.GoToState(this, "Inactive", true);
            }
            if (_deferredActions != null)
            {
                _deferredActions.Add(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 更新加载控件中圆多少
        /// </summary>
        private void UpdateLargeState()
        {
            Action action;

            if (IsLarge)
            {
                action = () => VisualStateManager.GoToState(this, "Large", true);
            }
            else
            {
                action = () => VisualStateManager.GoToState(this, "Small", true);
            }
            if (_deferredActions != null)
            {
                _deferredActions.Add(action);
            }
            else
            {
                action();
            }
        }
        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            UpdateLargeState();
            UpdateActiveState();
            base.OnApplyTemplate();
            if (_deferredActions != null)
            {
                foreach (var action in _deferredActions)
                {
                    action();
                }
            }
            _deferredActions = null;
        }
        #endregion
    }
}
