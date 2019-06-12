using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOn.Helper
{
    public class ConfigurationReader
    {
        public static string ReadString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static bool ReadBool(string key)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result.ToLower() == "true" ? true : (result == "1" ? true : false);
            }
            return false;
        }
        public static string[] ReadArray(string key, char separator)
        {
            string result = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            }
            return null;
        }

        //配置文件写入如下
        //private void WriteConfig()
        //{
        //    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    if (IsRemeberName)
        //    {
        //        cfa.AppSettings.Settings["IsRememberName"].Value = "true";
        //        if (names == null)
        //        {
        //            cfa.AppSettings.Settings["Names"].Value = Name;
        //        }
        //        else
        //        {
        //            if (names.Contains(name))
        //            {
        //                cfa.AppSettings.Settings["Names"].Value = string.Join(";", names.ToArray());
        //            }
        //            else
        //            {
        //                List<string> result = new List<string>();
        //                result.AddRange(names);
        //                result.Add(name);
        //                cfa.AppSettings.Settings["Names"].Value = string.Join(";", result.ToArray());
        //            }
        //        }
        //    }
        //    else
        //    {
        //        cfa.AppSettings.Settings["IsRememberName"].Value = "false";
        //        cfa.AppSettings.Settings["Names"].Value = "";
        //    }
        //    cfa.Save(System.Configuration.ConfigurationSaveMode.Modified);
        //    System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        //}
    }
}
