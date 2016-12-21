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
    public class LedgerTableExportOpration<T> : ExportOperation<T>
    {
        public override void FillHearder(HSSFWorkbook workBook, string sheetName)
        {
            ISheet sheet = workBook.CreateSheet(sheetName);

            IRow row = sheet.CreateRow(0);
            row.HeightInPoints = 20;

            ICell cell = row.CreateCell(0);

            cell.SetCellValue("*");
            sheet.SetColumnWidth(0, 10 * 256);// 第二个参数单位是1/256个字符宽度 
            //在Excel中，每一行的高度也是要求一致的，所以设置单元格的高度，其实就是设置行的高度，所以相关的属性也应该在HSSFRow上，它就是HSSFRow.Height和HeightInPoints，
            //这两个属性的区别在于HeightInPoints的单位是点，而Height的单位是1/20个点，所以Height的值永远是HeightInPoints的1/20倍。
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(1);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(1, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(2);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(2, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(3);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(3, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(4);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(4, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(5);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(5, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(6);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(6, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(7);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(7, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(8);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(8, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(9);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(9, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(10);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(10, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(11);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(11, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(12);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(12, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(13);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(13, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(14);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(14, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(15);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(15, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(16);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(16, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(17);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(17, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(18);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(18, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(19);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(19, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(20);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(20, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(21);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(21, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(22);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(22, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(23);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(23, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(24);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(24, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(25);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(25, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(26);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(26, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(27);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(27, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(28);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(28, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(29);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(29, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(30);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(30, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            cell = row.CreateCell(31);
            cell.SetCellValue("*");
            sheet.SetColumnWidth(31, 20 * 256);
            cell.CellStyle = GetCellStyle("HeadStyle");

            CreateFreezePane(sheet, 0, 1);
        }
        public override void FillForm(NPOI.HSSF.UserModel.HSSFWorkbook workBook)
        {
            base.FillForm(workBook);

            HSSFSheet sheet = (HSSFSheet)workBook.GetSheet("Sheet1");

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

            LedgerTableModel ledgerTableModel = item as LedgerTableModel;
            cell = row.GetCell(0);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Xh);

            cell = row.GetCell(1);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Rq);

            cell = row.GetCell(2);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Sld);

            cell = row.GetCell(3);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Ywlx);

            cell = row.GetCell(4);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Hth);

            cell = row.GetCell(5);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerName);

            cell = row.GetCell(6);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerZjh);

            cell = row.GetCell(7);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerName);

            cell = row.GetCell(8);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerZjh);

            cell = row.GetCell(9);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fwdz);

            cell = row.GetCell(10);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fczh);

            cell = row.GetCell(11);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fwlx);

            cell = row.GetCell(12);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gmnx);

            cell = row.GetCell(13);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jtwyzf);

            cell = row.GetCell(14);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Htjg);

            cell = row.GetCell(15);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jsjg);

            cell = row.GetCell(16);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qssl);

            cell = row.GetCell(17);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsje);

            cell = row.GetCell(18);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gssl);

            cell = row.GetCell(19);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gsje);

            cell = row.GetCell(20);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsje);

            cell = row.GetCell(21);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Tdsl);

            cell = row.GetCell(22);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Yysje);

            cell = row.GetCell(23);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Cjsje);

            cell = row.GetCell(24);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jyfjsje);

            cell = row.GetCell(25);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Dfjysje);

            cell = row.GetCell(26);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerYhsje);

            cell = row.GetCell(27);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerYhsje);

            cell = row.GetCell(28);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fczyhsje);

            cell = row.GetCell(29);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmlx);

            cell = row.GetCell(30);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmje);

            cell = row.GetCell(31);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmwjh);
        }

        /// <summary>
        /// 对行赋值
        /// </summary>
        /// <param name="row">行对象</param>
        /// <param name="item">数据对象</param>
        private void SetNoTemplateDataRowValue(HSSFWorkbook workBook, IRow row, T item)
        {
            ICell cell;
            LedgerTableModel ledgerTableModel = item as LedgerTableModel;

            cell = row.CreateCell(0);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Xh);

            cell = row.CreateCell(1);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Rq);

            cell = row.CreateCell(2);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Sld);

            cell = row.CreateCell(3);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Ywlx);

            cell = row.CreateCell(4);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Hth);

            cell = row.CreateCell(5);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerName);

            cell = row.CreateCell(6);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerZjh);

            cell = row.CreateCell(7);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerName);

            cell = row.CreateCell(8);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerZjh);

            cell = row.CreateCell(9);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fwdz);

            cell = row.CreateCell(10);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fczh);

            cell = row.CreateCell(11);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fwlx);

            cell = row.CreateCell(12);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gmnx);

            cell = row.CreateCell(13);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jtwyzf);

            cell = row.CreateCell(14);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Htjg);

            cell = row.CreateCell(15);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jsjg);

            cell = row.CreateCell(16);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qssl);

            cell = row.CreateCell(17);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsje);

            cell = row.CreateCell(18);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gssl);

            cell = row.CreateCell(19);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Gsje);

            cell = row.CreateCell(20);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Tdzzsje);

            cell = row.CreateCell(21);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Tdsl);

            cell = row.CreateCell(22);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Yysje);

            cell = row.CreateCell(23);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Cjsje);

            cell = row.CreateCell(24);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Jyfjsje);

            cell = row.CreateCell(25);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Dfjysje);

            cell = row.CreateCell(26);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.SellerYhsje);

            cell = row.CreateCell(27);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.BuyerYhsje);

            cell = row.CreateCell(28);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Fczyhsje);

            cell = row.CreateCell(29);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmlx);

            cell = row.CreateCell(30);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmje);

            cell = row.CreateCell(31);
            cell.CellStyle = GetCellStyle("FormStyle");
            cell.SetCellValue(ledgerTableModel.Qsjmwjh);
        }
    }
}
