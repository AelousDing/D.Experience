using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace D.Experience
{
    public class XmlHelper
    {
        /// <summary>
        /// 将实体序列化成xml字符串
        /// 序列化注意:如果实体序列化的时候,指定根节点是在类中使用XmlRootAttribute特性
        /// 如果集合实体序列化的时候,指定集合中子项根节点是在类中使用XmlType特性
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体</param>
        /// <param name="root">根节点</param>
        /// <param name="namespaces">命名空间</param>
        /// <param name="encodingStyle">编码</param>
        /// <returns></returns>
        public static string Serialize<T>(XmlSerializerNamespaces namespaces, T obj, XmlRootAttribute root)
        {
            string xml = null;
            //如果指定了根节点则使用指定的值，否则使用默认
            try
            {
                XmlSerializer serializer = null;
                if (root == null)
                {
                    serializer = new XmlSerializer(obj.GetType());
                }
                else
                {
                    serializer = new XmlSerializer(obj.GetType(), root);
                }
                StringBuilder sb = new StringBuilder();
                StringWriter textWriter = new StringWriter(sb);
                //如果没传命名空间，则生成的xml中不带命名空间
                if (namespaces == null)
                {
                    namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                }
                serializer.Serialize(textWriter, obj, namespaces);
                xml = sb.ToString();
            }
            catch (Exception ex)
            {
            }
            return xml;
        }
        /// <summary>
        /// 将实体序列化成xml字符串
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体</param>
        /// <param name="root">根节点</param>
        /// <param name="namespaces">命名空间</param>
        /// <param name="encodingStyle">编码</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, XmlRootAttribute root, Encoding encodingStyle)
        {
            string xml = null;
            //如果指定了根节点则使用指定的值，否则使用默认
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    var setting = new XmlWriterSettings()
                    {
                        Encoding = encodingStyle,
                        Indent = true,
                        NamespaceHandling = NamespaceHandling.OmitDuplicates
                    };
                    using (XmlWriter writer = XmlWriter.Create(ms, setting))
                    {
                        XmlSerializer serializer = null;
                        if (root == null)
                        {
                            serializer = new XmlSerializer(obj.GetType());
                        }
                        else
                        {
                            serializer = new XmlSerializer(obj.GetType(), root);
                        }
                        serializer.Serialize(writer, obj);
                        xml = encodingStyle.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return xml;
        }
        /// <summary>
        /// 反序列化 xml 对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (var sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <typeparam name="T">反序列化的类型</typeparam>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static T Read<T>(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;

            using (var sr = new StringReader(doc.InnerXml))
            {
                var xmldes = new XmlSerializer(typeof(T), new XmlRootAttribute(root.Name));
                return (T)xmldes.Deserialize(sr);
            }
        }
        public static void Write(string xml, string path)
        {
            XmlDocument doc = new XmlDocument();
            xml = xml.Trim();
            doc.LoadXml(xml);
            doc.Save(path);
        }
    }
}
