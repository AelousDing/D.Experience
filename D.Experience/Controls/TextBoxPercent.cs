using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace D.Experience.Controls
{
    /// <summary>
    /// 模块编号：自定义控件
    /// 作用：输入0-1之间的数字，失去焦点之后转换成百分数
    /// 作者：Aelous.D
    /// 编写日期：2016-12-22
    /// </summary>
    public class TextBoxPercent : TextBox
    {
        public TextBoxPercent()
        {
            InputMethod.SetIsInputMethodEnabled(this, false);
        }
        protected override void OnGotFocus(System.Windows.RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (!string.IsNullOrEmpty(Text))
            {
                if (Text.Contains("%"))
                {
                    string percent = Text.Substring(0, Text.Length - 1);
                    double result;
                    if (double.TryParse(percent, out result))
                    {
                        Text = (result / 100).ToString();
                    }
                    this.Select(Text.Length, 0);
                }
                else
                {
                    Text = "";
                }
            }
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (!string.IsNullOrEmpty(Text))
            {
                double result;
                if (double.TryParse(Text, out result))
                {
                    Text = (result * 100).ToString() + "%";
                }
            }
        }
        protected override void OnTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            string input = e.Text;
            if (input.Equals("."))
            {
                if (this.Text.Contains("."))
                {
                    //设置光标的位置 
                    this.Select(this.Text.IndexOf(".") + 1, 0);
                    e.Handled = true;
                }
                else if (string.IsNullOrEmpty(this.Text))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if (Regex.IsMatch(input, @"^[\d]$"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            base.OnTextInput(e);
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(Text.Trim()) && !Text.Contains("%"))
            {
                if (Regex.IsMatch(Text, @"^[01]$|^0\.\d*$"))
                {
                }
                else
                {
                    Text = "";
                }
            }
            base.OnTextChanged(e);
        }
    }
}
