using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace D.Experience.Controls
{
    /// <summary>
    /// 模块编号：
    /// 作用：用于处理可以多选的树控件的项
    /// 作者：
    /// 编写日期：2016-11-30
    /// </summary>
    public class MultiSelectionTreeViewItem : TreeViewItem
    {
        #region 属性
        /// <summary>
        /// 获取此节点的父级容器控件
        /// </summary>
        public ItemsControl ParentItemsControl
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this);
            }
        }
        /// <summary>
        /// 获取项目所在的MultiSelectionTreeView控件
        /// 如果项目没在MultiSelectionTreeView控件中，则返回空
        /// </summary>
        public MultiSelectionTreeView ParentTreeView
        {
            get
            {
                for (ItemsControl container = this.ParentItemsControl; container != null; container = ItemsControl.ItemsControlFromItemContainer(container))
                {
                    MultiSelectionTreeView view = container as MultiSelectionTreeView;
                    if (view != null)
                    {
                        return view;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 获取该项所在的父级MultiSelectionTreeViewItem，如果该项未在MultiSelectionTreeViewItem类型的控件上，则返回空值。
        /// </summary>
        public MultiSelectionTreeViewItem ParentTreeViewItem
        {
            get
            {
                return ParentItemsControl as MultiSelectionTreeViewItem;
            }
        }

        public new bool? IsSelected
        {
            get { return (bool?)GetValue(IsSelectedExProperty); }
            set { SetValue(IsSelectedExProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelectedEx.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedExProperty =
            DependencyProperty.Register("IsSelected", typeof(bool?), typeof(MultiSelectionTreeViewItem), new PropertyMetadata(false, new PropertyChangedCallback(OnSelectedExChanged)));

        private static void OnSelectedExChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectionTreeViewItem tvi = d as MultiSelectionTreeViewItem;
            if ((bool?)e.NewValue != (bool?)e.OldValue)
            {
                tvi.IsSelected = (bool?)e.NewValue;
                if ((bool?)e.NewValue == true)
                {
                    tvi.OnSelectedEx();
                }
                else if ((bool?)e.NewValue == false)
                {
                    tvi.OnUnselectedEx();
                }
                else
                {
                    //tvi.OnUnselectedEx();
                }
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 静态的构造函数，OverridesDefaultStyleProperty属性元数据重载，以便于加载样式
        /// </summary>
        static MultiSelectionTreeViewItem()
        {
            //OverridesDefaultStyleProperty.OverrideMetadata(typeof(MultiSelectionTreeViewItem), new FrameworkPropertyMetadata(typeof(MultiSelectionTreeViewItem)));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiSelectionTreeViewItem), new FrameworkPropertyMetadata(typeof(MultiSelectionTreeViewItem)));
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
        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ParentTreeView == null)
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Released && e.RightButton == MouseButtonState.Pressed)
            {
                return;
            }
            if (IsSelected == true)
            {
                IsSelected = false;
            }
            else if (IsSelected == false)
            {
                IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }
            //TODO:逻辑父级需要做的逻辑
            ParentTreeView.OnViewItemMouseDown(this);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                MultiSelectionTreeViewItem itemToSelect = null;
                if (e.Key == Key.Left)
                {
                    this.IsExpanded = false;
                }
                else if (e.Key == Key.Right)
                {
                    this.IsExpanded = true;
                }
                else if (e.Key == Key.Up)
                {
                    int curentNodeIndex = this.ParentItemsControl.ItemContainerGenerator.IndexFromContainer(this);
                    if (curentNodeIndex == 0)
                    {
                        itemToSelect = this.ParentTreeViewItem;
                    }
                    else
                    {
                        MultiSelectionTreeViewItem temp = null;
                        temp = GetPreviousNodeAtSameLevel(this);
                        itemToSelect = GetLastVisibleChildNodeOf(temp);
                    }
                }
                else if (e.Key == Key.Down)
                {
                    if (this.IsExpanded && this.Items.Count > 0)
                    {
                        itemToSelect = this.ItemContainerGenerator.ContainerFromIndex(0) as MultiSelectionTreeViewItem;
                    }
                    else
                    {
                        itemToSelect = GetNexNodeAtSameLevel(this);
                        if (itemToSelect == null)
                        {
                            MultiSelectionTreeViewItem temp = this.ParentTreeViewItem;
                            while (itemToSelect == null && temp != null)
                            {
                                itemToSelect = GetNexNodeAtSameLevel(temp);
                                temp = temp.ParentTreeViewItem;
                            }
                        }
                    }
                }
                if (itemToSelect != null)
                {
                    itemToSelect.Focus();
                    itemToSelect.IsSelected = true;
                    //TODO:Treeview逻辑
                    ParentTreeView.OnViewItemMouseDown(itemToSelect);
                }
            }
            catch (Exception)
            {
                e.Handled = true;
            }
        }

        private static bool _isSelectParent;
        private static bool _isUnSelectParent;
        protected void OnSelectedEx()
        {
            if (!_isSelectParent)
            {
                SelectAllExpandChildren();
            }
            SelectAllParent();
        }
        protected void OnUnselectedEx()
        {
            if (!_isUnSelectParent)
            {
                UnSelectAllChildren();
            }
            UnSelectAllParent();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取上个可见节点，如果节点有子节点并且展开则取最后的子节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private MultiSelectionTreeViewItem GetLastVisibleChildNodeOf(MultiSelectionTreeViewItem item)
        {
            MultiSelectionTreeViewItem lastVisibleNode = item;
            while (lastVisibleNode != null && lastVisibleNode.Items.Count > 0 && lastVisibleNode.IsExpanded)
            {
                lastVisibleNode = lastVisibleNode.ItemContainerGenerator.ContainerFromIndex(lastVisibleNode.Items.Count - 1) as MultiSelectionTreeViewItem;
            }
            return lastVisibleNode;
        }
        /// <summary>
        /// 获取与某节点处于同意等级，并且此节点的同等级的上个节点
        /// </summary>
        /// <param name="item">传入的节点</param>
        /// <returns>节点同意等级的上个节点</returns>
        private MultiSelectionTreeViewItem GetPreviousNodeAtSameLevel(MultiSelectionTreeViewItem item)
        {
            if (item == null)
            {
                return null;
            }

            MultiSelectionTreeViewItem previousNodeAtSameLevel = null;

            if (item.ParentItemsControl != null)
            {
                int index = item.ParentItemsControl.ItemContainerGenerator.IndexFromContainer(item);
                if (index > 0)
                {
                    previousNodeAtSameLevel = item.ParentItemsControl.ItemContainerGenerator.ContainerFromIndex(index - 1) as MultiSelectionTreeViewItem;
                }
            }
            return previousNodeAtSameLevel;
        }
        /// <summary>
        /// 获取同级别的下一个节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private MultiSelectionTreeViewItem GetNexNodeAtSameLevel(MultiSelectionTreeViewItem item)
        {
            if (item == null)
            {
                return null;
            }
            MultiSelectionTreeViewItem nextNodeAtSame = null;
            if (item.ParentItemsControl != null)
            {
                int index = item.ParentItemsControl.ItemContainerGenerator.IndexFromContainer(item);
                if (index <= item.Items.Count - 1)
                {
                    nextNodeAtSame = item.ParentItemsControl.ItemContainerGenerator.ContainerFromIndex(index + 1) as MultiSelectionTreeViewItem;
                }
            }
            return nextNodeAtSame;
        }
        /// <summary>
        /// 取消所有子节点的选中
        /// </summary>
        public void UnSelectAllChildren()
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
            if (IsSelected != false)
            {
                IsSelected = false;
            }
            //TODO:
            ParentTreeView.OnSelectionChnanged(this);
        }
        /// <summary>
        /// 选中所有展开的节点
        /// </summary>
        public void SelectAllExpandChildren()
        {
            if (Items != null && Items.Count > 0)
            {
                if (!IsExpanded)
                {
                    IsExpanded = true;
                }
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
                            if (tvi.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                            {
                                tvi.UpdateLayout();
                            }
                            tvi.SelectAllExpandChildren();
                        }
                    }
                }
            }
            if (IsSelected != true)
            {
                IsSelected = true;
            }
            //TODO:
            ParentTreeView.OnSelectionChnanged(this);
        }
        /// <summary>
        /// 取消所有父节点的选中
        /// </summary>
        public void UnSelectAllParent()
        {
            _isUnSelectParent = true;
            try
            {
                MultiSelectionTreeViewItem parent = ParentTreeViewItem;
                while (parent != null && parent.IsSelected != false)
                {
                    bool isSelected = false;
                    foreach (var item in parent.Items)
                    {
                        MultiSelectionTreeViewItem viewItem = null;
                        if (item is MultiSelectionTreeViewItem)
                        {
                            viewItem = item as MultiSelectionTreeViewItem;
                        }
                        else
                        {
                            viewItem = parent.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
                        }
                        if (viewItem == null || viewItem.IsSelected != false)
                        {
                            isSelected = true;
                            break;
                        }
                    }
                    if (isSelected)
                    {
                        parent.IsSelected = null;
                    }
                    else
                    {
                        parent.IsSelected = false;
                    }
                    parent = parent.ParentTreeViewItem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _isUnSelectParent = false;
            }
        }
        /// <summary>
        /// 判断父节点是否需要选中
        /// </summary>
        public void SelectAllParent()
        {
            _isSelectParent = true;
            try
            {
                MultiSelectionTreeViewItem parent = ParentTreeViewItem;

                while (parent != null && parent.IsSelected != true)
                {
                    foreach (var item in parent.Items)
                    {
                        MultiSelectionTreeViewItem viewItem = null;
                        if (item is MultiSelectionTreeViewItem)
                        {
                            viewItem = item as MultiSelectionTreeViewItem;
                        }
                        else
                        {
                            viewItem = parent.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
                        }
                        if (viewItem == null || viewItem.IsSelected == false)
                        {
                            parent.IsSelected = null;
                            return;
                        }
                    }
                    parent.IsSelected = true;
                    parent = parent.ParentTreeViewItem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _isSelectParent = false;
            }
        }
        /// <summary>
        /// 展开所有子节点
        /// </summary>
        public void ExpandAllChildren()
        {
            if (Items != null && Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    if (item is MultiSelectionTreeViewItem)
                    {
                        MultiSelectionTreeViewItem tvi = item as MultiSelectionTreeViewItem;
                        if (!tvi.IsExpanded)
                        {
                            tvi.IsExpanded = true;
                        }
                        tvi.ExpandAllChildren();
                    }
                    else
                    {
                        MultiSelectionTreeViewItem tvi = this.ItemContainerGenerator.ContainerFromItem(item) as MultiSelectionTreeViewItem;
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
        }
        /// <summary>
        /// 获取节点在树控件上的深度
        /// </summary>
        /// <returns></returns>
        public int GetDepth()
        {
            MultiSelectionTreeViewItem parent = this;
            while (parent.ParentTreeViewItem != null)
            {
                return parent.ParentTreeViewItem.GetDepth() + 1;
            }
            return 0;
        }
        #endregion
    }
}
