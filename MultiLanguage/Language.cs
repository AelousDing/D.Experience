using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiLanguage
{
    /// <summary>
    /// 调用方式
    /// XAML：
    /// xmlns:lan="clr-namespace:MultiLanguage.Properties;assembly=MultiLanguage"
    /// <Button Grid.Row="5" Content="{x:Static lan:Resources.UserName}" Command="{Binding LogOnCommand}" FontSize="16" Height="40" VerticalAlignment="Top" IsDefault="True"/>
    /// .CS：MultiLanguage.Properties.Resources.UserName
    /// </summary>
    public class Language
    {
        public static void SetLanguage(string region)
        {
            CultureInfo ci = new CultureInfo(region);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
