using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace D.Experience.Print
{
    /// <summary>
    /// 模块编号：公用
    /// 作用：打印使用的接口
    /// 作者：丁纪名
    /// 编写日期：2016-12-26
    /// </summary>
    public interface IDocumentRenderer<T>
    {
        /// <summary>
        /// 渲染打印界面 可设置doc 的属性来决定打印的大小，A4横向：PageHeight="761.5" PageWidth="1084.7"；A4纵向：PageHeight="1084.7" PageWidth="761.5"
        /// </summary>
        /// <param name="doc">显示打印界面的FlowDocument文档</param>
        /// <param name="data">显示需要的数据</param>
        /// <param name="pageOrientation"></param>
        void Render(FlowDocument doc, T data);
    }
    /// <summary>
    /// 模块编号：公用
    /// 作用：渲染打印时的界面
    /// 作者：丁纪名
    /// 编写日期：2016-12-26
    /// </summary>
    public class DocumentHelper
    {
        /// <summary>
        /// 加载流文档的方法
        /// </summary>
        /// <typeparam name="T">数据上下文数据类型</typeparam>
        /// <param name="strTmplName">流文档路径</param>
        /// <param name="margin">流文档padding</param>
        /// <param name="data">数据上下文</param>
        /// <param name="renderer">渲染方法接口</param>
        /// <returns></returns>
        public static FlowDocument LoadDocumentAndRender<T>(string strTmplName, Thickness padding, T data, IDocumentRenderer<T> renderer = null)
        {
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri(strTmplName, UriKind.RelativeOrAbsolute));
            doc.PagePadding = padding;
            doc.DataContext = data;
            if (renderer != null)
            {
                renderer.Render(doc, data);
            }
            return doc;
        }
    }
    public class FlowDocumentPrinter : FlowDocumentScrollViewer
    {
        public event EventHandler PrintCompleted;

        protected override void OnPrintCompleted()
        {
            base.OnPrintCompleted();
            if (PrintCompleted != null)
            {
                PrintCompleted(this, null);
            }
        }
    }
}
