using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigFile
{
    public class ConfigHandle
    {
        public enum HandleFun
        {
            Xml,
            AppConfig1,
            AppConfig2,
        }

        static HandleFun handleFun = HandleFun.Xml;

        /*
         * XPath 使用路径表达式在 XML 文档中选取节点。节点是通过沿着路径或者 step 来选取的
         * 下面列出了最有用的路径表达式：
         * 表达式        描述
         * nodename      选取此节点的所有子节点
         * /             从根节点选取
         * //            从匹配选择的当前节点选择文档中的节点，而不考虑它们的位置
         * .             选取当前节点
         * ..            选取当前节点的父节点
         * @             选取属性
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            try
            {
                if (handleFun == HandleFun.Xml)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "ConfigFile.config");
                    XmlNode node = doc.SelectSingleNode(@"//add[@key='" + key + "']");
                    XmlElement ele = (XmlElement)node;
                    string value = ele.GetAttribute("value");
                    return value;
                }
                else if (handleFun == HandleFun.AppConfig1)
                {
                    string value = ConfigurationManager.AppSettings[key];
                    Console.WriteLine("ConfigurationManager:" + value);
                    return value;
                }
                else
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    string value = cfa.AppSettings.Settings[key].Value;
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public static void SaveConfig(string key, string value)
        {
            try
            {
                if (handleFun == HandleFun.Xml)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "ConfigFile.config");
                    XmlNode node = doc.SelectSingleNode(@"//add[@key='" + key + "']");
                    XmlElement ele = (XmlElement)node;
                    ele.SetAttribute("value", value);
                    doc.Save(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "ConfigFile.config");
                }
                else
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings[key].Value = value;
                    cfa.Save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
