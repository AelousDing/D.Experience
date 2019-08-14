using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Core
{
    public class Startup
    {
        public Startup()
        {
            AppEnvironment.Config = LoadConfig();
        }

        private Config LoadConfig()
        {
            Config config = new Config();
            PropertyInfo[] properties = config.GetType().GetProperties();
            foreach (var property in properties)
            {
                string propertyValue = ConfigurationManager.AppSettings[property.Name];
                if (!property.PropertyType.IsGenericType)
                {
                    property.SetValue(config, string.IsNullOrEmpty(propertyValue) ? null : Convert.ChangeType(propertyValue, property.PropertyType), null);
                }
                else
                {
                    Type type = property.PropertyType.GetGenericTypeDefinition();
                    if (type == typeof(Nullable<>))
                    {
                        property.SetValue(config, string.IsNullOrEmpty(propertyValue) ? null : Convert.ChangeType(propertyValue, Nullable.GetUnderlyingType(property.PropertyType)), null);
                    }
                }
            }
            return config;
        }
    }
}
