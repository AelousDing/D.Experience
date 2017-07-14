using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace D.Experience.Helper
{
    /// <summary>
    /// 模块编号：公共帮助类
    /// 作用：获取文件在系统中默认的图标
    /// 作者：dingjiming
    /// 编写日期：2017-07-14
    /// </summary>
    public class FileIcon
    {
        /// <summary>
        /// 获取文件的默认图标 
        /// 文件名。 
        /// 可以只是文件名，甚至只是文件的扩展名(.*)； 
        /// 如果想获得.ICO文件所表示的图标，则必须是文件的完整路径。
        /// 是否大图标 
        /// 文件的默认图标 
        /// </summary>
        /// <param name="fileName">文件路径或者后缀</param>
        /// <param name="largeIcon">是否是大图标</param>
        /// <returns></returns>
        public static Icon GetFileIcon(string fileName, bool largeIcon)
        {
            SHFILEINFO info = new SHFILEINFO(true);
            int cbFileInfo = Marshal.SizeOf(info);
            SHGFI flags;
            if (largeIcon)
                flags = SHGFI.Icon | SHGFI.LargeIcon | SHGFI.UseFileAttributes;
            else
                flags = SHGFI.Icon | SHGFI.SmallIcon | SHGFI.UseFileAttributes;
            SHGetFileInfo(fileName, 256, out info, (uint)cbFileInfo, flags);
            return Icon.FromHandle(info.hIcon);
        }
        /// <summary>
        /// 获取系统默认图标转换成的imagesource
        /// </summary>
        /// <param name="fileName">文件路径或者后缀</param>
        /// <param name="largeIcon">是否是大图标</param>
        /// <returns></returns>
        public static ImageSource IconToImageSource(string fileName, bool largeIcon)
        {
            Icon icon = GetFileIcon(fileName, largeIcon);
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(

                icon.Handle,

                new Int32Rect(0, 0, icon.Width, icon.Height),

                BitmapSizeOptions.FromEmptyOptions());
        }
        [DllImport("Shell32.dll")]
        private static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public SHFILEINFO(bool b)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
            public string szTypeName;
        };
        private enum SHGFI
        {
            SmallIcon = 0x00000001,
            LargeIcon = 0x00000000,
            Icon = 0x00000100,
            DisplayName = 0x00000200,
            Typename = 0x00000400,
            SysIconIndex = 0x00004000,
            UseFileAttributes = 0x00000010
        }
    }
}
