using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp.ConfigCache
{
    /// <summary>
    /// 配置信息生产工厂
    /// </summary>
    public class ConfigFactory : Singleton<ConfigFactory>
    {
        #region 私有

        private ConfigFactory() { }

        /// <summary>
        /// 配置文件管理类
        /// </summary>
        static ConfigFilesManager cfm;
 
        #endregion
 
        #region 公开的属性
         public T GetConfig<T>() where T : IConfiger
         {
             string configFilePath = string.Empty;
             string filename = typeof(T).Name;
 
             HttpContext context = HttpContext.Current;
             string siteVirtrualPath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SiteVirtrualPath"]) ?
                 "/" : ConfigurationManager.AppSettings["SiteVirtrualPath"];
             if (context != null)
             {
                 configFilePath = context.Server.MapPath(string.Format("{0}/Configs/{1}.Config", siteVirtrualPath, filename));
             }
             else
             {
                 configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format( @"Configs\{0}.config", filename));
             }
 
             if (!File.Exists(configFilePath))
             {
                 throw new Exception("发生错误: 网站" +
                     new FileInfo("fileName").DirectoryName
                     + "目录下没有正确的.Config文件");
             }
 
             cfm = new ConfigFilesManager(configFilePath, typeof(T));
             return (T)cfm.LoadConfig();
         }
         #endregion
    }
}
