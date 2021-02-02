using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Reflection;


namespace Стамотология.Classes
{
    /// <summary>
    /// Записывает в файл конфигураци ключ
    /// </summary>
    public static class ConfigFile
    {

        public static bool WriteSetting(string key, string value)
        {
            XmlDocument doc = loadConfigDocument();
            XmlNode node = doc.SelectSingleNode("//appSettings");

            //if (node == null)
            //throw new InvalidOperationException("appSettings section not found in config file.");

            try
            {
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    elem.SetAttribute("value", value);
                }
                else
                {
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }

        private static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }

    }
}


        //public static void ChangeConfigValue(string section, string name, string value) // Работает со старой версией.
        //public static bool ChangeConfigValue(string name, string value)
        //{

        //    try
        //    {
        //        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
        //        fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + @"App.config";
        //        Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        //        config.AppSettings.Settings[name].Value = value;
        //        config.Save();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    #region Страрая версия
        //    //try
        //    //{
        //    //    string filename = AppDomain.CurrentDomain.BaseDirectory + @"App.config";
        //    //    XmlDocument xmldoc = new XmlDocument();
        //    //    xmldoc.Load(filename);
        //    //    XmlNodeList nodeList = xmldoc.DocumentElement.ChildNodes;

        //    //    foreach (XmlElement element in nodeList)
        //    //        if (element.Name.ToLower() == section.ToLower())
        //    //        {
        //    //            XmlNodeList node = element.ChildNodes;
        //    //            if (node.Count > 0)
        //    //                foreach (XmlElement el in node)
        //    //                    if (el.Attributes["key"].InnerText == name)
        //    //                    {
        //    //                        el.Attributes["value"].InnerText = value;
        //    //                        break;
        //    //                    }
        //    //            break;
        //    //        }
        //    //    xmldoc.Save(filename);
        //    //}

        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine(ex.Message);
        //    //}

        //    #endregion
        //}
