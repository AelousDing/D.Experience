using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace D.Experience
{
    public class NotifyIconHelper
    {
        private NotifyIcon notifyIcon;
        private Window host;
        public NotifyIconHelper(Window window, string tipText, string text)
        {
            host = window;
            
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = tipText;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.Text = text;
            notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);

            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += Exit_Click;
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            host.Close();
        }
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //双击托盘图标
            host.Show();
            host.WindowState = WindowState.Normal;
        }

        public void Show()
        {
            if (!notifyIcon.Visible)
            {
                notifyIcon.Visible = true;
            }
        }
        public void Hide()
        {
            if (notifyIcon.Visible)
            {
                notifyIcon.Visible = false;
            }
        }
        public void Dispose()
        {
            notifyIcon.Dispose();
        }
    }
}
