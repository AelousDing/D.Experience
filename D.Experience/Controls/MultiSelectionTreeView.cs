using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    ///     <MyNamespace:MultiSelectionTreeView/>
    ///
    /// </summary>
    public class MultiSelectionTreeView : ItemsControl
    {
        #region 属性
        private MultiSelectionTreeViewItem _lastClickedItem = null;
        /// <summary>
        /// 获取或设置选择模式
        /// </summary>
        public SelectionModalities SelectionMode
        {
            get { return (SelectionModalities)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionModalities), typeof(MultiSelectionTreeView), new PropertyMetadata(SelectionModalities.Single));

        private SelectedItemsCollection _selectedItems = new SelectedItemsCollection();
        /// <summary>
        /// 获取选择的项
        /// </summary>
        public SelectedItemsCollection SelectedItems
        {
            get { return _selectedItems; }
        }

        #endregion

        #region 构造函数
        static MultiSelectionTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiSelectionTreeView), new FrameworkPropertyMetadata(typeof(MultiSelectionTreeView)));
        }
        #endregion

        #region 重载
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MultiSelectionTreeViewItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MultiSelectionTreeViewItem;
        }
        #endregion

        #region 方法
        private void UnSelectAll()
        {
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item is MultiSelectionTreeViewItem)
                    {
                        (item as MultiSelectionTreeViewItem).UnSelectAllChildren();
                    }
                    else
                    {
                        MultiSelectionTreeViewItem tvi = this.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
                        if (tvi != null)
                        {
                            tvi.UnSelectAllChildren();
                        }
                    }
                }
            }
        }
        private void SelectionAllExpand()
        {
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item is MultiSelectionTreeViewItem)
                    {
                        (item as MultiSelectionTreeViewItem).SelectAllExpandChildren();
                    }
                    else
                    {
                        MultiSelectionTreeViewItem tvi = this.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
                        if (tvi != null)
                        {
                            tvi.SelectAllExpandChildren();
                        }
                    }
                }
            }
        }
        private void AddItemToSelection(MultiSelectionTreeViewItem newItem)
        {
            if (!_selectedItems.Contains(newItem))
            {
                _selectedItems.Add(newItem);
            }
            OnTextChanged();
        }
        private void RemoveItemFromSelection(MultiSelectionTreeViewItem newItem)
        {
            if (_selectedItems.Contains(newItem))
            {
                _selectedItems.Remove(newItem);
            }
            OnTextChanged();
        }
        private void ManageCtrlSelection(MultiSelectionTreeViewItem viewItem)
        {
            bool? isMultiSelected = viewItem.IsSelected;
            if (isMultiSelected==true)
            {
                AddItemToSelection(viewItem);
            }
            else
            {
                RemoveItemFromSelection(viewItem);
            }
        }
        private void ManageSingleSelection(MultiSelectionTreeViewItem viewItem)
        {
            bool? isMultiSelected = viewItem.IsSelected;
            UnSelectAll();
            if (isMultiSelected==true)
            {
                viewItem.IsSelected = isMultiSelected;
                AddItemToSelection(viewItem);
            }
        }
        internal void OnSelectionChnanged(MultiSelectionTreeViewItem viewItem)
        {
            if (viewItem == null)
            {
                return;
            }
            if (viewItem.IsSelected==true)
            {
                AddItemToSelection(viewItem);
            }
            else
            {
                RemoveItemFromSelection(viewItem);
            }
        }
        internal void OnViewItemMouseDown(MultiSelectionTreeViewItem viewItem)
        {
            if (viewItem == null)
            {
                return;
            }
            switch (SelectionMode)
            {
                case SelectionModalities.Single:
                    ManageSingleSelection(viewItem);
                    break;
                case SelectionModalities.Multiple:
                    ManageCtrlSelection(viewItem);
                    break;
                case SelectionModalities.Keyboard:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) ||
                        Keyboard.IsKeyDown(Key.RightCtrl) ||
                        Keyboard.IsKeyDown(Key.LeftShift) ||
                        Keyboard.IsKeyDown(Key.RightShift))
                    {
                        ManageCtrlSelection(viewItem);
                    }
                    else
                    {
                        ManageSingleSelection(viewItem);
                    }
                    break;
                default:
                    break;
            }
            _lastClickedItem = viewItem.IsSelected==true ? viewItem : null;
        }
        public void OnTextChanged()
        {
            if (TextChangedEvent != null)
            {
                TextChangedEvent(this, null);
            }
        }
        #endregion

        #region 事件
        public event EventHandler TextChangedEvent;
        #endregion
    }
    public class SelectedItemsCollection : ObservableCollection<MultiSelectionTreeViewItem>
    {

    }
    /// <summary>
    /// 选中模式
    /// </summary>
    public enum SelectionModalities
    {
        /// <summary>
        /// 单选
        /// </summary>
        Single,
        /// <summary>
        /// 多选
        /// </summary>
        Multiple,
        /// <summary>
        /// ctrl和shift键选中
        /// </summary>
        Keyboard
    }
}
