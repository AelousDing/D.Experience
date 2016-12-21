using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace D.Experience.ExcelHelp
{
    public class ExportOperation<T>
    {
        /// <summary>
        /// 每页的最大行数(excel 2003 每页的最大行数为65536)
        /// </summary>
        private const double _pageSize = 65536.00;
        /// <summary>
        /// 表名
        /// </summary>
        private const string _sheetName = "Sheet";

        private IEnumerable<T> fillData;
        /// <summary>
        /// 导出是填充表格的数据对象
        /// </summary>
        public IEnumerable<T> FillData
        {
            get { return fillData; }
            set { fillData = value; }
        }
        private int headCount;
        /// <summary>
        /// 获取或设置表头占据的行数，准确的说是数据开始的上一行
        /// </summary>
        public int HeadCount
        {
            get { return headCount; }
            set { headCount = value; }
        }
        private Dictionary<string, ICellStyle> cellStyle;
        /// <summary>
        /// 获取或设置样式表
        /// </summary>
        public Dictionary<string, ICellStyle> CellStyle
        {
            get { return cellStyle; }
            set { cellStyle = value; }
        }

        /// <summary>
        /// 有模板的导出方式
        /// </summary>
        /// <param name="importPath">模板路径</param>
        /// <param name="exportPath">导出是保存路径</param>
        public void Export(string importPath, int totalCount)
        {
            if (totalCount > _pageSize - headCount)
            {
                throw new OverflowException("导出的数据行数，大于Excel表格允许的最大行数（65336），请分批次导出！");
            }
            if (string.IsNullOrEmpty(importPath))
            {
                throw new ArgumentNullException("模板路径为空，请选择模板路径！");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel 文件|*.xls";
            saveFileDialog.AddExtension = true;

            System.Console.WriteLine(saveFileDialog.FileName);
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(importPath),
                 outStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(stream);
                    SetCellStyles(workbook);
                    //填充表格
                    FillForm(workbook);
                    workbook.Write(outStream);
                }
            }
        }
        /// <summary>
        /// 无模板的导出方式 
        /// </summary>
        /// <param name="totalCount">导出数据条数，没有数据的时候默认0条数据</param>
        public void Export(int totalCount = 0)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel 文件|*.xls";
            saveFileDialog.AddExtension = true;

            System.Console.WriteLine(saveFileDialog.FileName);
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream outStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    double pageCout = 0;
                    if (totalCount <= 0)
                    {
                        pageCout = 1;//没有数据的时候导出表头
                    }
                    else
                    {
                        pageCout = Math.Ceiling(totalCount / _pageSize);
                        if (pageCout * (_pageSize - headCount) < totalCount)
                        {
                            pageCout++;//防止数据所占行数加上各个表头所在行数大于pageCout*_pageSize
                        }
                    }

                    //无模板的导出方式
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    SetCellStyles(workbook);

                    for (int i = 1; i <= pageCout; i++)
                    {
                        FillHearder(workbook, _sheetName + i.ToString());
                        //填充表格
                        if (fillData != null)
                        {
                            FillNoTemplateForm(workbook, fillData.Skip<T>((int)((i - 1) * (_pageSize - headCount))).Take<T>((int)(_pageSize - headCount)), _sheetName + i.ToString());
                        }
                    }
                    workbook.Write(outStream);
                }
            }
        }
        /// <summary>
        /// 填充表头
        /// </summary>
        /// <param name="workBook">表格对象</param>
        public virtual void FillHearder(HSSFWorkbook workBook, string sheetName)
        {
        }
        /// <summary>
        /// 填充无模板表格
        /// </summary>
        /// <param name="workBook">表格对象</param>
        /// <param name="fillData">填充表格的数据对象</param>
        public virtual void FillNoTemplateForm(HSSFWorkbook workBook, IEnumerable<T> data, string sheetName)
        {
            if (data == null)
            {
                //throw new ArgumentNullException("没有需要导出的数据！");
                return;//无数据也允许导出，导出空表格
            }
        }
        /// <summary>
        /// 填充有模板表格
        /// </summary>
        /// <param name="workBook">表格对象</param>
        /// <param name="fillData">填充表格的数据对象</param>
        public virtual void FillForm(HSSFWorkbook workBook)
        {
            if (fillData == null)
            {
                //throw new ArgumentNullException("没有需要导出的数据！");
                return;//无数据也允许导出，导出空表格
            }
        }
        /// <summary>
        /// 设置单元格样式表
        /// </summary>
        /// <param name="workBook">表格对象</param>
        public virtual void SetCellStyles(HSSFWorkbook workBook)
        {
            cellStyle = new Dictionary<string, ICellStyle>();
            cellStyle.Add("HeadStyle", SetCellStyle(workBook, SetFont(workBook)));
            cellStyle.Add("FormStyle", SetCellStyle(workBook, SetFont(workBook, FontBoldWeight.NORMAL)));
        }
        /// <summary>
        /// 设置字体格式
        /// </summary>
        /// <param name="workBook">表格对象</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontHeightInPoints">字体大小</param>
        /// <returns></returns>
        public IFont SetFont(HSSFWorkbook workBook, FontBoldWeight weight = FontBoldWeight.BOLD, string fontName = "宋体", short fontHeightInPoints = 11)
        {
            IFont font = workBook.CreateFont();
            font.Boldweight = (short)weight;
            font.FontName = fontName;
            font.FontHeightInPoints = fontHeightInPoints;
            return font;
        }
        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="workBook">表对象</param>
        /// <param name="font">字体</param>
        /// <param name="ha">水平对齐方式</param>
        /// <param name="va">垂直对齐方式</param>
        /// <returns>返回单元格样式</returns>
        public ICellStyle SetCellStyle(HSSFWorkbook workBook, IFont font, bool wrapText = true, HorizontalAlignment ha = HorizontalAlignment.CENTER, VerticalAlignment va = VerticalAlignment.CENTER)
        {
            ICellStyle style = workBook.CreateCellStyle();
            if (font != null)
            {
                style.SetFont(font);
            }
            style.WrapText = wrapText;
            style.Alignment = ha;
            style.VerticalAlignment = va;
            return style;
        }
        /// <summary>
        /// 获取样式文件名
        /// </summary>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public ICellStyle GetCellStyle(string styleName)
        {
            ICellStyle cellStyle;
            if (!CellStyle.ContainsKey(styleName))
            {
                throw new System.Collections.Generic.KeyNotFoundException("不存在该名称的单元格样式！");
            }
            cellStyle = CellStyle[styleName];
            return cellStyle;
        }
        /// <summary>
        /// 冻结某行或者某列
        /// </summary>
        /// <param name="sheet">表对象</param>
        /// <param name="colSplit">冻结的列</param>
        /// <param name="rowSplit">冻结的行</param>
        public void CreateFreezePane(ISheet sheet, int colSplit, int rowSplit)
        {
            sheet.CreateFreezePane(colSplit, rowSplit);
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">表对象</param>
        /// <param name="firstRow">合并第一行</param>
        /// <param name="lastRow">合并最后一行</param>
        /// <param name="firstCol">合并第一列</param>
        /// <param name="lastCol">合并最后一列</param>
        public void MergedRegion(ISheet sheet, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            sheet.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
        }
    }
}
