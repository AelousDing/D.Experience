using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace D.Experience.CustomVisul
{
    /// <summary>
    /// 模块编号：自定义可视化容器
    /// 作用：自定义可视化容器
    /// 作者：丁纪名（源代码来自WPF编程宝典）
    /// 编写日期：2017-01-18
    /// </summary>
    public class DrawingCanvas : Panel
    {
        /// <summary>
        /// 可视化对象列表
        /// </summary>
        private List<Visual> _visuals = new List<Visual>();

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }
        /// <summary>
        /// 添加可视化对象
        /// </summary>
        /// <param name="visual"></param>
        public void AddVisual(Visual visual)
        {
            _visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }
        /// <summary>
        /// 移除可视化对象
        /// </summary>
        /// <param name="visual"></param>
        public void DeleteVisual(Visual visual)
        {
            _visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
        /// <summary>
        /// 获取选中的可视化对象
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DrawingVisual GetVisual(System.Windows.Point point)
        {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(this, point);
            return hitTestResult.VisualHit as DrawingVisual;
        }
        List<DrawingVisual> hits = new List<DrawingVisual>();
        /// <summary>
        /// 获取选中区域内的可视化对象
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public List<DrawingVisual> GetVisuals(Geometry region)
        {
            hits.Clear();
            GeometryHitTestParameters parameters = new GeometryHitTestParameters(region);
            HitTestResultCallback callback = new HitTestResultCallback(TestResultCallback);
            VisualTreeHelper.HitTest(this, null, callback, parameters);
            return hits;
        }

        private HitTestResultBehavior TestResultCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if (visual != null && geometryResult.IntersectionDetail == IntersectionDetail.FullyContains)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }
    }
}
