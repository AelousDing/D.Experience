using D.Experience.ExcelHelp;
using D.Experience.Test.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D.Experience.Test.ExcelHelp
{
    public class ReportFormExportOpration<T> : ExportOperation<T>
    {
        public override void FillHearder(HSSFWorkbook workBook, string sheetName)
        {
            ISheet sheet = workBook.CreateSheet(sheetName);

            IRow row = sheet.CreateRow(0);
            IRow row1 = sheet.CreateRow(1);
            row.HeightInPoints = 20;
            row1.HeightInPoints = 20;

            ICell cell = row.CreateCell(0);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(0, 10 * 256);// 第二个参数单位是1/256个字符宽度 
            //在Excel中，每一行的高度也是要求一致的，所以设置单元格的高度，其实就是设置行的高度，所以相关的属性也应该在HSSFRow上，它就是HSSFRow.Height和HeightInPoints，
            //这两个属性的区别在于HeightInPoints的单位是点，而Height的单位是1/20个点，所以Height的值永远是HeightInPoints的1/20倍。
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(0);
            cell.CellStyle = GetCellStyle("BorderStyle");
            MergedRegion(sheet, 0, 1, 0, 0);

            cell = row.CreateCell(1);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(1, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(1);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 1, 1);

            cell = row.CreateCell(2);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(2, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(2);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 2, 2);

            cell = row.CreateCell(3);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(3, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(3);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 3, 3);

            cell = row.CreateCell(4);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(4, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(4);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 4, 4);

            cell = row.CreateCell(5);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(5, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(5);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 5, 5);

            cell = row.CreateCell(6);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(6, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(6);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 6, 6);

            cell = row.CreateCell(7);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(7, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(7);
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 1, 7, 7);

            cell = row.CreateCell(8);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(8, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(8);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(9);
            sheet.SetColumnWidth(9, 20 * 256);

            cell = row1.CreateCell(9);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(10);
            sheet.SetColumnWidth(10, 20 * 256);

            cell = row1.CreateCell(10);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(11);
            sheet.SetColumnWidth(11, 20 * 256);

            cell = row1.CreateCell(11);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 8, 11);//契税合并单元格

            cell = row.CreateCell(12);
            sheet.SetColumnWidth(12, 20 * 256);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(12);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(13);
            sheet.SetColumnWidth(13, 20 * 256);

            cell = row1.CreateCell(13);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(14);
            sheet.SetColumnWidth(14, 20 * 256);

            cell = row1.CreateCell(14);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(15);
            sheet.SetColumnWidth(15, 20 * 256);

            cell = row1.CreateCell(15);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(16);
            sheet.SetColumnWidth(16, 20 * 256);

            cell = row1.CreateCell(16);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 12, 16);//营业税及附加合并单元格

            cell = row.CreateCell(17);
            sheet.SetColumnWidth(17, 20 * 256);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(17);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(18);
            sheet.SetColumnWidth(18, 20 * 256);

            cell = row1.CreateCell(18);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(19);
            sheet.SetColumnWidth(19, 20 * 256);

            cell = row1.CreateCell(19);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(20);
            sheet.SetColumnWidth(20, 20 * 256);

            cell = row1.CreateCell(20);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(21);
            sheet.SetColumnWidth(21, 20 * 256);

            cell = row1.CreateCell(21);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 17, 21);//个人所得税合并单元格

            cell = row.CreateCell(22);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(22, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(22);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(23);
            sheet.SetColumnWidth(23, 20 * 256);

            cell = row1.CreateCell(23);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 22, 23);//土地增值税合并单元格

            cell = row.CreateCell(24);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(24, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(24);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(25);
            sheet.SetColumnWidth(25, 20 * 256);

            cell = row1.CreateCell(25);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 24, 25);//印花税合并单元格

            cell = row.CreateCell(26);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(26, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(26);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(27);
            sheet.SetColumnWidth(27, 20 * 256);

            cell = row1.CreateCell(27);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 26, 27);//拆迁减免合并单元格

            cell = row.CreateCell(28);
            sheet.SetColumnWidth(28, 20 * 256);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(28);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row.CreateCell(29);
            sheet.SetColumnWidth(29, 20 * 256);
            cell.CellStyle = GetCellStyle("BorderStyle");

            cell = row1.CreateCell(29);
            cell.SetCellValue("*");
            cell.CellStyle = GetCellStyle("BorderStyle");

            MergedRegion(sheet, 0, 0, 28, 29);//拆迁减免合并单元格
        }
        public override void FillForm(NPOI.HSSF.UserModel.HSSFWorkbook workBook)
        {
            base.FillForm(workBook);

            HSSFSheet sheet = (HSSFSheet)workBook.GetSheet("台账字段");

            int rowCount = HeadCount;
            IRow row;

            foreach (var item in FillData)
            {
                row = sheet.GetRow(rowCount);
                SetDataRowValue(workBook, row, item);
                rowCount++;
            }
        }
        public override void FillNoTemplateForm(HSSFWorkbook workBook, IEnumerable<T> data, string sheetName)
        {
            base.FillNoTemplateForm(workBook, data, sheetName);
            HSSFSheet sheet = (HSSFSheet)workBook.GetSheet(sheetName);

            int rowCount = HeadCount;
            IRow row;

            foreach (var item in data)
            {
                row = sheet.CreateRow(rowCount);
                SetNoTemplateDataRowValue(workBook, row, item);
                rowCount++;
            }
        }
        /// <summary>
        /// 对行赋值
        /// </summary>
        /// <param name="row">行对象</param>
        /// <param name="item">数据对象</param>
        private void SetDataRowValue(HSSFWorkbook workBook, IRow row, T item)
        {
            ICell cell;

            ReportFormModel ledgerTableModel = item as ReportFormModel;
            cell = row.GetCell(0);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Dw);

            cell = row.GetCell(1);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsrq);

            cell = row.GetCell(2);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jzrq);

            cell = row.GetCell(3);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Blfs);

            cell = row.GetCell(4);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Esffs);

            cell = row.GetCell(5);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Ysffs);

            cell = row.GetCell(6);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Zyfe);

            cell = row.GetCell(7);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Fpfs);

            cell = row.GetCell(8);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsje);

            cell = row.GetCell(9);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_1);

            cell = row.GetCell(10);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_1_5);

            cell = row.GetCell(11);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_3);

            cell = row.GetCell(12);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yysfs);

            cell = row.GetCell(13);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cjje);

            cell = row.GetCell(14);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jyfje);

            cell = row.GetCell(15);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jyfje);

            cell = row.GetCell(16);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jyfjje);

            cell = row.GetCell(17);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsje);

            cell = row.GetCell(18);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs);

            cell = row.GetCell(19);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs_1);

            cell = row.GetCell(20);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs_20);

            cell = row.GetCell(21);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Mgsfs);

            cell = row.GetCell(22);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsje);

            cell = row.GetCell(23);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsfs);

            cell = row.GetCell(24);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yhsje);

            cell = row.GetCell(25);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yhsfs);

            cell = row.GetCell(26);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cqjmse);

            cell = row.GetCell(27);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cqjmfs);

            cell = row.GetCell(28);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gfjmse);

            cell = row.GetCell(29);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gfjmfs);
        }

        /// <summary>
        /// 对行赋值
        /// </summary>
        /// <param name="row">行对象</param>
        /// <param name="item">数据对象</param>
        private void SetNoTemplateDataRowValue(HSSFWorkbook workBook, IRow row, T item)
        {
            ICell cell;
            ReportFormModel ledgerTableModel = item as ReportFormModel;

            cell = row.CreateCell(0);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Dw);

            cell = row.CreateCell(1);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsrq);

            cell = row.CreateCell(2);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jzrq);

            cell = row.CreateCell(3);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Blfs);

            cell = row.CreateCell(4);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Esffs);

            cell = row.CreateCell(5);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Ysffs);

            cell = row.CreateCell(6);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Zyfe);

            cell = row.CreateCell(7);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Fpfs);

            cell = row.CreateCell(8);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsje);

            cell = row.CreateCell(9);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_1);

            cell = row.CreateCell(10);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_1_5);

            cell = row.CreateCell(11);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Qsfs_3);

            cell = row.CreateCell(12);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yysje);

            cell = row.CreateCell(13);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yysfs);

            cell = row.CreateCell(14);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cjje);

            cell = row.CreateCell(15);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jyfje);

            cell = row.CreateCell(16);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Jyfjje);

            cell = row.CreateCell(17);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsje);

            cell = row.CreateCell(18);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs);

            cell = row.CreateCell(19);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs_1);

            cell = row.CreateCell(20);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gsfs_20);

            cell = row.CreateCell(21);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Mgsfs);

            cell = row.CreateCell(22);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsje);

            cell = row.CreateCell(23);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsfs);

            cell = row.CreateCell(24);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yhsje);

            cell = row.CreateCell(25);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Yhsfs);

            cell = row.CreateCell(26);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cqjmse);

            cell = row.CreateCell(27);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Cqjmfs);

            cell = row.CreateCell(28);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gfjmse);

            cell = row.CreateCell(29);
            cell.CellStyle = GetCellStyle("BorderStyle");
            cell.SetCellValue(ledgerTableModel.Gfjmfs);
        }

        public override void SetCellStyles(HSSFWorkbook workBook)
        {
            base.SetCellStyles(workBook);
            ICellStyle cellStyle = SetCellStyle(workBook, SetFont(workBook, FontBoldWeight.NORMAL));
            //单元格边框
            cellStyle.BorderBottom = BorderStyle.THIN;
            cellStyle.BorderLeft = BorderStyle.THIN;
            cellStyle.BorderRight = BorderStyle.THIN;
            cellStyle.BorderTop = BorderStyle.THIN;

            CellStyle.Add("BorderStyle", cellStyle);
        }
    }
}
