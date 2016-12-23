using System;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Linq;
using System.Windows.Markup;
using System.Collections.Generic;

namespace D.Experience.Print
{

    public class PrintHelper
    {
        static double a4Width = 761.5; //后续修改 自动获取
        static double a4Height = 1084.7;  //后续修改 自动获取
        //1.从bitmap转换成ImageSource 
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true)]
        internal static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="ViewContainer">需要打印的控件</param>
        /// <param name="pageOrientation">打印方向，通过枚举类型 System.Printing.PageOrientation选择需要打印的方式</param>
        public static void Print(FrameworkElement ViewContainer, PageOrientation pageOrientation = PageOrientation.Portrait, PageMediaSizeName pageMediaSizeName = PageMediaSizeName.ISOA4)
        {
            if (ViewContainer is Canvas)
            {
                throw new Exception("该方法不支持 Canvas 类型控件打印");
            }

            FrameworkElement objectToPrint = ViewContainer as FrameworkElement;
            double obj_Width = objectToPrint.ActualWidth;
            double obj_Heigh = objectToPrint.ActualHeight;

            if (pageOrientation == PageOrientation.Portrait)
            {
                a4Width = 761.5;
                a4Height = 1084.7;

            }
            else if (pageOrientation == PageOrientation.Landscape)
            {
                a4Width = 1084.7;  //后续修改 自动获取
                a4Height = 761.5;  //后续修改 自动获取
            }

            RenderTargetBitmap bmp = null;
            System.Drawing.Bitmap bmp2 = null;
            PageContent lastPageContent = null;
            FixedDocument document = new FixedDocument();
            PrintDialog printDialog = new PrintDialog();
            bool isDoPrint = false;
            try
            {
                printDialog.PrintTicket.PageOrientation = pageOrientation;
                printDialog.PrintTicket.PageMediaSize = new PageMediaSize(pageMediaSizeName);

                if ((bool)printDialog.ShowDialog().GetValueOrDefault())
                {
                    isDoPrint = true;
                    Mouse.OverrideCursor = Cursors.Wait;

                    double dpiScale = 300.0 / 96.0;


                    try
                    {
                        Size size = new Size(a4Width > objectToPrint.ActualWidth ? objectToPrint.ActualWidth : a4Width,
                                          a4Height > objectToPrint.ActualHeight ? a4Height : objectToPrint.ActualHeight);
                        objectToPrint.Measure(size);
                        objectToPrint.Arrange(new Rect(size));

                        double left = a4Width > objectToPrint.ActualWidth ?
                            (a4Width - objectToPrint.ActualWidth) * 0.5 : 20;

                        // Convert the UI control into a bitmap at 300 dpi
                        double dpiX = 300;

                        double dpiY = 300;

                        bmp = new RenderTargetBitmap(Convert.ToInt32(

                            a4Width * dpiScale),

                            Convert.ToInt32(objectToPrint.ActualHeight * dpiScale),

                            dpiX, dpiY, PixelFormats.Pbgra32);

                        bmp.Render(objectToPrint);
                        bmp.Freeze();
                        // Convert the RenderTargetBitmap into a bitmap we can more readily use

                        PngBitmapEncoder png = new PngBitmapEncoder();

                        png.Frames.Add(BitmapFrame.Create(bmp));


                        using (MemoryStream memoryStream = new MemoryStream())
                        {

                            png.Save(memoryStream);

                            bmp2 = new System.Drawing.Bitmap(memoryStream);

                        }

                        document.DocumentPaginator.PageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                        // break the bitmap down into pages

                        int pageBreak = 0;
                        int previousPageBreak = 0;
                        int temp = 0;
                        int pageHeight =

                             Convert.ToInt32(a4Height * dpiScale);

                        while (pageBreak < bmp2.Height - pageHeight)
                        {

                            pageBreak += pageHeight - 250;  // Where we thing the end of the page should be
                            temp = pageBreak;
                            // Keep moving up a row until we find a good place to break the page

                            while (!IsRowGoodBreakingPoint(bmp2, pageBreak))
                            {
                                pageBreak--;
                                if (pageBreak == previousPageBreak)
                                {
                                    pageBreak = temp;
                                    break;
                                }
                            }

                            var pageContent = GeneratePageContent(
                                bmp2,
                                previousPageBreak == 0 ? 0 : previousPageBreak - 3,
                                pageBreak + 3,
                                document.DocumentPaginator.PageSize.Width,
                                document.DocumentPaginator.PageSize.Height,
                                new Size(a4Width, a4Height),
                                left);
                            document.Pages.Add(pageContent);
                            previousPageBreak = pageBreak;
                        }

                        // Last Page

                        lastPageContent = GeneratePageContent(bmp2, previousPageBreak == 0 ? 0 : previousPageBreak - 3,

                          bmp2.Height, document.DocumentPaginator.PageSize.Width,

                          document.DocumentPaginator.PageSize.Height, new Size(a4Width, a4Height), left);

                        document.Pages.Add(lastPageContent);
                        printDialog.PrintDocument(document.DocumentPaginator, "Print Document Name");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        //Scale UI control back to the original so we don't effect what is on the screen
                        Size size = new Size(obj_Width, obj_Heigh);
                        objectToPrint.Measure(size);
                        objectToPrint.Arrange(new Rect(size));
                        Mouse.OverrideCursor = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("打印过程中出错，错误描述为：" + ex.Message, "打印", MessageBoxButton.OK, MessageBoxImage.Information);
                Console.WriteLine(ex.ToString());

                //try
                //{
                //    if (ex is OutOfMemoryException && isDoPrint)
                //    {
                //        printDialog.PrintVisual(ViewContainer, "打印");
                //        return;
                //    }
                //}
                //catch (Exception e)
                //{

                //}

                string msg = "系统检测到您的打印机状态不正常（可能您在使用其他软件时能正常打印），建议重新安装打印机驱动！\n点击“确定”按钮，将导出XPS文档，您可自行打印。\n异常信息为：" + ex.Message;
                if (MessageBox.Show(msg, "打印", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    SaveAsXps(objectToPrint);
                }
            }
            finally
            {
                try
                {
                    printDialog = null;

                    if (bmp != null)
                    {
                        bmp.Clear();
                        bmp = null;
                    }
                    if (document != null)
                    {
                        foreach (var fixedPage in document.Pages.Select(pageContent => pageContent.Child))
                        {
                            fixedPage.Children.Clear();
                        }
                    }
                    lastPageContent = null;

                    if (bmp2 != null)
                    {
                        bmp2.Dispose();
                    }
                    document = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    //GC.Collect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="ViewContainer">需要打印的控件</param>
        /// <param name="pageOrientation">打印方向，通过枚举类型 System.Printing.PageOrientation选择需要打印的方式</param>
        public static bool Print(PrintDialog printDialog, List<FrameworkElement> ViewContainers, PageOrientation pageOrientation = PageOrientation.Portrait, PageMediaSizeName pageMediaSizeName = PageMediaSizeName.ISOA4)
        {
            if (pageOrientation == PageOrientation.Portrait)
            {
                a4Width = 761.5;
                a4Height = 1084.7;

            }
            else if (pageOrientation == PageOrientation.Landscape)
            {
                a4Width = 1084.7;  //后续修改 自动获取
                a4Height = 761.5;  //后续修改 自动获取
            }

            RenderTargetBitmap bmp = null;
            System.Drawing.Bitmap bmp2 = null;
            PageContent lastPageContent = null;
            FixedDocument document = new FixedDocument();
            try
            {
                printDialog.PrintTicket.PageOrientation = pageOrientation;
                printDialog.PrintTicket.PageMediaSize = new PageMediaSize(pageMediaSizeName);

                Mouse.OverrideCursor = Cursors.Wait;

                double dpiScale = 300.0 / 96.0;

                try
                {
                    foreach (FrameworkElement objectToPrint in ViewContainers)
                    {
                        Size size = new Size(a4Width > objectToPrint.ActualWidth ? objectToPrint.ActualWidth : a4Width,
                                          a4Height > objectToPrint.ActualHeight ? a4Height : objectToPrint.ActualHeight);
                        objectToPrint.Measure(size);
                        objectToPrint.Arrange(new Rect(size));

                        double left = a4Width > objectToPrint.ActualWidth ?
                            (a4Width - objectToPrint.ActualWidth) * 0.5 : 20;

                        // Convert the UI control into a bitmap at 300 dpi
                        double dpiX = 300;

                        double dpiY = 300;

                        bmp = new RenderTargetBitmap(Convert.ToInt32(
                            a4Width * dpiScale),
                            Convert.ToInt32(objectToPrint.ActualHeight * dpiScale),
                            dpiX, dpiY, PixelFormats.Pbgra32);

                        bmp.Render(objectToPrint);
                        bmp.Freeze();
                        // Convert the RenderTargetBitmap into a bitmap we can more readily use
                        PngBitmapEncoder png = new PngBitmapEncoder();
                        png.Frames.Add(BitmapFrame.Create(bmp));

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            png.Save(memoryStream);
                            bmp2 = new System.Drawing.Bitmap(memoryStream);
                        }

                        document.DocumentPaginator.PageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                        // break the bitmap down into pages
                        int pageBreak = 0;
                        int previousPageBreak = 0;
                        int temp = 0;
                        int pageHeight =
                             Convert.ToInt32(a4Height * dpiScale);

                        while (pageBreak < bmp2.Height - pageHeight)
                        {
                            pageBreak += pageHeight - 250;  // Where we thing the end of the page should be
                            temp = pageBreak;
                            // Keep moving up a row until we find a good place to break the page
                            while (!IsRowGoodBreakingPoint(bmp2, pageBreak))
                            {
                                pageBreak--;
                                if (pageBreak == previousPageBreak)
                                {
                                    pageBreak = temp;
                                    break;
                                }
                            }

                            var pageContent = GeneratePageContent(
                                bmp2,
                                previousPageBreak == 0 ? 0 : previousPageBreak - 3,
                                pageBreak + 3,
                                document.DocumentPaginator.PageSize.Width,
                                document.DocumentPaginator.PageSize.Height,
                                new Size(a4Width, a4Height),
                                left);
                            document.Pages.Add(pageContent);
                            previousPageBreak = pageBreak;
                        }

                        // Last Page
                        lastPageContent = GeneratePageContent(bmp2, previousPageBreak == 0 ? 0 : previousPageBreak - 3,
                          bmp2.Height, document.DocumentPaginator.PageSize.Width,
                          document.DocumentPaginator.PageSize.Height, new Size(a4Width, a4Height), left);
                        document.Pages.Add(lastPageContent);
                    }
                    printDialog.PrintDocument(document.DocumentPaginator, "Print Document Name");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //Scale UI control back to the original so we don't effect what is on the screen
                    //Size size = new Size(obj_Width, obj_Heigh);
                    //objectToPrint.Measure(size);
                    //objectToPrint.Arrange(new Rect(size));
                    Mouse.OverrideCursor = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("打印时出现异常，异常信息为：" + ex.Message, "打印");

            }
            finally
            {
                try
                {
                    if (bmp != null)
                    {
                        bmp.Clear();
                        bmp = null;
                    }
                    if (document != null)
                    {
                        foreach (var fixedPage in document.Pages.Select(pageContent => pageContent.Child))
                        {
                            fixedPage.Children.Clear();
                        }
                    }
                    lastPageContent = null;

                    if (bmp2 != null)
                    {
                        bmp2.Dispose();
                    }
                    document = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    //GC.Collect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return true;
        }





        private static bool IsRowGoodBreakingPoint(System.Drawing.Bitmap bmp, int row)
        {
            bool goodBreakingPoint = false;
            double cval = rowPixelDeviation(bmp, row);

            if (cval < 50000 || cval > 5000000)

                goodBreakingPoint = true;
            return goodBreakingPoint;

        }
        private static double rowPixelDeviation(System.Drawing.Bitmap bmp, int row)
        {

            int count = 0;

            double total = 0;

            double totalVariance = 0;

            double standardDeviation = 0;

            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0,

                   bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            int stride = bmpData.Stride;

            IntPtr firstPixelInImage = bmpData.Scan0;

            unsafe
            {

                byte* p = (byte*)(void*)firstPixelInImage;

                p += stride * row;  // find starting pixel of the specified row

                for (int column = 0; column < bmp.Width; column++)
                {

                    count++;  //count the pixels

                    byte blue = p[0];

                    byte green = p[1];

                    byte red = p[3];

                    int pixelValue = System.Drawing.Color.FromArgb(0, red, green, blue).ToArgb();

                    total += pixelValue;

                    double average = total / count;

                    totalVariance += Math.Pow(pixelValue - average, 2);

                    standardDeviation = Math.Sqrt(totalVariance / count);

                    //go to next pixel

                    p += 3;

                }
            }

            bmp.UnlockBits(bmpData);

            return standardDeviation;

        }

        #region XPS A4
        /// <summary>
        /// 导出XPS文档
        /// </summary>
        /// <param name="objectToPrint"></param>
        private static void SaveAsXps(FrameworkElement objectToPrint)
        {
            double dpiScale = 300.0 / 96.0;
            FixedDocument document = new FixedDocument();
            double obj_Width = objectToPrint.ActualWidth;
            double obj_Height = objectToPrint.ActualHeight;
            try
            {
                Size size = new Size(a4Width > objectToPrint.ActualWidth ? objectToPrint.ActualWidth : a4Width,
                                  a4Height > objectToPrint.ActualHeight ? a4Height : objectToPrint.ActualHeight);
                objectToPrint.Measure(size);
                objectToPrint.Arrange(new Rect(size));

                double left = a4Width > objectToPrint.ActualWidth ? (a4Width - objectToPrint.ActualWidth) * 0.5 : 20;

                // Convert the UI control into a bitmap at 300 dpi
                double dpiX = 300;

                double dpiY = 300;

                RenderTargetBitmap bmp = new RenderTargetBitmap(Convert.ToInt32(
                    a4Width * dpiScale),
                    Convert.ToInt32(objectToPrint.ActualHeight * dpiScale),
                    dpiX, dpiY, PixelFormats.Pbgra32);

                bmp.Render(objectToPrint);

                // Convert the RenderTargetBitmap into a bitmap we can more readily use

                PngBitmapEncoder png = new PngBitmapEncoder();

                png.Frames.Add(BitmapFrame.Create(bmp));

                System.Drawing.Bitmap bmp2;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    png.Save(memoryStream);
                    bmp2 = new System.Drawing.Bitmap(memoryStream);
                }

                document.DocumentPaginator.PageSize = new Size(1122, 793);

                // break the bitmap down into pages
                int pageBreak = 0;
                int previousPageBreak = 0;
                int temp = 0;
                int pageHeight = Convert.ToInt32(a4Height * dpiScale);

                while (pageBreak < bmp2.Height - pageHeight)
                {
                    pageBreak += pageHeight - 250;  // Where we thing the end of the page should be
                    temp = pageBreak;
                    // Keep moving up a row until we find a good place to break the page
                    while (!IsRowGoodBreakingPoint(bmp2, pageBreak))
                    {
                        pageBreak--;
                        if (pageBreak == previousPageBreak)
                        {
                            pageBreak = temp;
                            break;
                        }
                    }

                    PageContent pageContent = GeneratePageContent(
                        bmp2,
                        previousPageBreak == 0 ? 0 : previousPageBreak - 3,
                        pageBreak + 3,
                        document.DocumentPaginator.PageSize.Width,
                        document.DocumentPaginator.PageSize.Height,
                        new Size(a4Width, a4Height),
                        left);
                    document.Pages.Add(pageContent);
                    previousPageBreak = pageBreak;
                }
                // Last Page
                PageContent lastPageContent = GeneratePageContent(bmp2, previousPageBreak == 0 ? 0 : previousPageBreak - 3,
                  bmp2.Height, document.DocumentPaginator.PageSize.Width,
                  document.DocumentPaginator.PageSize.Height, new Size(a4Width, a4Height), left);
                document.Pages.Add(lastPageContent);

                SaveSingleFixedContentDocument(document, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("导出XPS文件时出错，错误描述为：" + ex.Message, "导出XPS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                //Scale UI control back to the original so we don't effect what is on the screen
                Size size = new Size(obj_Width, obj_Height);
                objectToPrint.Measure(size);
                objectToPrint.Arrange(new Rect(size));
                Mouse.OverrideCursor = null;
            }
        }

        private static PageContent GeneratePageContent(System.Drawing.Bitmap bmp, int top, int bottom, double pageWidth, double PageHeight, Size pageSize, double left)
        {

            FixedPage printDocumentPage = new FixedPage();
            printDocumentPage.Width = pageWidth;

            printDocumentPage.Height = PageHeight;

            int newImageHeight = bottom - top;

            System.Drawing.Bitmap bmpPage = bmp.Clone(new System.Drawing.Rectangle(0, top, bmp.Width, newImageHeight), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // Create a new bitmap for the contents of this page  

            Image pageImage = new Image();
            var ptr = bmpPage.GetHbitmap();
            BitmapSource bmpSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                     ptr,
                    IntPtr.Zero,
                   System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, newImageHeight));
            bmpSource.Freeze();
            bool res = PrintHelper.DeleteObject(ptr);
            pageImage.Source = bmpSource;

            pageImage.Width = pageSize.Width;

            pageImage.Height = pageSize.Height;
            pageImage.VerticalAlignment = VerticalAlignment.Top;
            pageImage.HorizontalAlignment = HorizontalAlignment.Center;

            // Place the bitmap on the page

            FixedPage.SetLeft(pageImage, left);

            FixedPage.SetTop(pageImage, 35);

            printDocumentPage.Children.Add(pageImage);
            PageContent pageContent = new PageContent();

            ((System.Windows.Markup.IAddChild)pageContent).AddChild(printDocumentPage);




            return pageContent;
        }

        private static void SaveSingleFixedContentDocument(FixedDocument fd, PrintTicket printTicket)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xps"; // Default file extension
            dlg.Filter = "XPS 文档|*.xps"; // Filter files by extension
            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result != true)
                return;
            string pack = dlg.FileName;
            Uri uri = new Uri(pack);
            using (Package container = Package.Open(pack, FileMode.Create))
            {
                PackageStore.AddPackage(uri, container);
                using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Fast, pack))
                {
                    XpsDocumentWriter xpsdw1 = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                    xpsdw1.Write(fd.DocumentPaginator);        // Write the FixedDocument as a document.                                        
                }
                PackageStore.RemovePackage(uri);
            }
            string msg = "导出XPS文件成功，文件位置“" + pack + "”是否立即打开该文档？";
            if (MessageBoxResult.Yes == MessageBox.Show(msg, "导出XPS", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                try
                {
                    System.Diagnostics.Process.Start(pack);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("采用默认的打开方式打开文件失败，请找到该文件，手动打开。", "打开XPS", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        #endregion


        public static void PrintNewMethod(FrameworkElement toPrint)
        {
            PrintDialog printDialog = new PrintDialog();
            FixedDocument doc = GetFixedDocument(toPrint, printDialog);
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                printDialog.PrintDocument(doc.DocumentPaginator, "打印");
            }
        }
        public static FixedDocument GetFixedDocument(FrameworkElement toPrint, PrintDialog printDialog)
        {
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            FixedDocument fixedDoc = new FixedDocument();
            //If the toPrint visual is not displayed on screen we neeed to measure and arrange it  
            toPrint.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));
            //  
            Size size = toPrint.DesiredSize;
            //Will assume for simplicity the control fits horizontally on the page  
            double yOffset = 0;
            while (yOffset < size.Height)
            {
                VisualBrush vb = new VisualBrush(toPrint);
                vb.Stretch = Stretch.None;
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.None;
                vb.Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height);
                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;
                Canvas canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += visibleSize.Height;
            }
            return fixedDoc;
        }
    }

}
