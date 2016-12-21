using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace D.Experience.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MultipleSelection"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MultipleSelection;assembly=MultipleSelection"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CheckBoxTreeView/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_TreeView", Type = typeof(MultiSelectionTreeView))]
    public class CheckBoxTreeView : Control
    {
        Popup _poup;
        TextBox _textBox;
        //Button _button;
        MultiSelectionTreeView _treeView;
        static CheckBoxTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxTreeView), new FrameworkPropertyMetadata(typeof(CheckBoxTreeView)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _poup = (Popup)this.Template.FindName("PART_Popup", this);
            _textBox = (TextBox)this.Template.FindName("PART_TextBox", this);
            _treeView = (MultiSelectionTreeView)this.Template.FindName("PART_TreeView", this);
            if (_treeView != null)
            {
                _treeView.TextChangedEvent -= _treeView_TextChangedEvent;
                _treeView.TextChangedEvent += _treeView_TextChangedEvent;
            }
            _treeView.ItemsSource = ItemsSource;
            _treeView.ItemTemplate = ItemTemplate;
        }
        void _treeView_TextChangedEvent(object sender, EventArgs e)
        {
            UpdateText();
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CheckBoxTreeView), new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxTreeView dp = (CheckBoxTreeView)d;

            if (dp._treeView != null)
            {
                dp._treeView.ItemsSource = e.NewValue as IEnumerable;
            }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CheckBoxTreeView), new PropertyMetadata(null, OnItemTemplateChanged));

        private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxTreeView dp = (CheckBoxTreeView)d;

            if (dp._treeView != null)
            {
                dp._treeView.ItemTemplate = e.NewValue as DataTemplate;
            }
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CheckBoxTreeView), new PropertyMetadata(string.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxTreeView ctv = d as CheckBoxTreeView;
            if (e.NewValue != null)
            {
                if (e.NewValue.ToString() != ctv._textBox.Text)
                {
                    ctv._textBox.Text = e.NewValue.ToString();
                }
            }
            else
            {
                ctv._textBox.Text = null;
            }
        }
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CheckBoxTreeView), new PropertyMetadata(null));


        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CheckBoxTreeView), new PropertyMetadata(false, new PropertyChangedCallback(OnIsDropDownOpenChanged), new CoerceValueCallback(CoerceIsDropDownOpen)));

        private static object CoerceIsDropDownOpen(DependencyObject d, object baseValue)
        {
            if ((bool)baseValue)
            {
                CheckBoxTreeView cb = (CheckBoxTreeView)d;
                if (!cb.IsLoaded)
                {
                    //cb.RegisterToOpenOnLoad();
                    return false;
                }
            }

            return baseValue;
        }

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckBoxTreeView cb = (CheckBoxTreeView)d;

            if (cb._poup != null)
            {
                if ((bool)e.NewValue)
                {
                    cb.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg)
                    {
                        //展开所有子节点
                        if (cb._treeView != null && cb.ItemsSource != null)
                        {
                            foreach (var item in cb._treeView.Items)
                            {
                                if (item is MultiSelectionTreeViewItem)
                                {
                                    (item as MultiSelectionTreeViewItem).IsExpanded = true;
                                }
                                else
                                {
                                    MultiSelectionTreeViewItem tvi = cb._treeView.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
                                    if (tvi != null)
                                    {
                                        if (!tvi.IsExpanded)
                                        {
                                            tvi.IsExpanded = true;
                                        }
                                        //如果子项还没有初始化容器，则刷新布局获取容器，保证取遍历的时候取到
                                        if (tvi.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                                        {
                                            tvi.UpdateLayout();
                                        }
                                        tvi.ExpandAllChildren();
                                    }
                                }
                            }
                        }
                        return null;
                    }, cb);

                }
            }
        }

        /// <summary>
        /// 更新显示文本
        /// </summary>
        private void UpdateText()
        {
            if (this._treeView != null && this._treeView.SelectedItems != null)
            {
                List<string> datas = new List<string>();
                foreach (var item in this._treeView.SelectedItems)
                {
                    if (item.HasItems)
                    {
                        continue;
                    }
                    object data = item.DataContext;
                    if (data == null)
                    {
                        continue;
                    }
                    Type type = data.GetType();
                    if (string.IsNullOrEmpty(DisplayMemberPath))
                    {
                        //TODO:暂时先将异常抛出
                        throw new ArgumentNullException("DisplayMemberPath不能为空。");
                    }
                    PropertyInfo propertyInfo = type.GetProperty(DisplayMemberPath);
                    if (propertyInfo == null)
                    {
                        Console.WriteLine("未找到DisplayMemberPath对应的属性。");
                    }
                    else
                    {
                        string value = propertyInfo.GetValue(data, null).ToString();
                        datas.Add(value);
                    }
                }
                if (datas.Count > 0)
                {
                    string text = string.Join(",", datas.ToArray());
                    Text = text;
                }
                else
                {
                    Text = string.Empty;
                }
            }
        }
    }
}
