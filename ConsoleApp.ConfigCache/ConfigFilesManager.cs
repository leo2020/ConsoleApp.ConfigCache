using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConfigCache
{
    /// <summary>
    /// 基本文件配置信息管理者
    /// </summary>
    internal class ConfigFilesManager
    {
         #region 私有
         /// <summary>
         /// 配置接口
         /// </summary>
         IConfiger configer;
         /// <summary>
         /// 锁对象
         /// </summary>
         object lockHelper = new object();
 
         /// <summary>
         /// 配置文件修改时间
         /// </summary>
         Dictionary<string,DateTime>  fileChangeTime = new Dictionary<string,DateTime>();
 
         /// <summary>
         /// 配置文件所在路径
         /// </summary>
         string fileName = null;
 
         /// <summary>
         /// 配置文件对应类型
         /// </summary>
         Type configType = null;
 
         #endregion
 
         #region 属性
 
         /// <summary>
         /// 当前配置类的实例 接口
         /// </summary>
         internal IConfiger IconfigInfo
         {
             get { return configer; }
             set { configer = value; }
         }
 
         /// <summary>
         /// 配置文件所在路径
         /// </summary>
         internal string ConfigFilePath
         {
             get { return fileName; }
 
         }
 
         #endregion
 
         #region 构造
 
         /// <summary>
         /// 初始化文件修改时间和对象实例
         /// </summary>
         internal ConfigFilesManager(string fileName, Type type)
         {
             this.fileName = fileName;
             //得到配置文件的  改时间    
             this.configType = type;
             if (this.fileChangeTime.ContainsKey(this.fileName))
                 fileChangeTime.Remove(this.fileName);
             fileChangeTime.Add(this.fileName,File.GetLastWriteTime(this.fileName));
             this.configer = ConfigSerialize.DeserializeInfo(this.fileName, this.configType);
         }
 
         #endregion
 
         #region 配置操作
 
         #region 加载配置类
         /// <summary>
         /// 加载配置类
         /// </summary>
         /// <returns></returns>
         internal IConfiger LoadConfig()
         {
             return LoadConfig(this.fileName, this.configType);
         }

        /// <summary>
         /// 从文件中反序列化到实体
         /// </summary>
         /// <param name="fileName"></param>
         /// <param name="type"></param>
         private IConfiger LoadConfigFile(string fileName, Type type)
         {
             this.fileName = fileName;
             this.configType = type;
             //fileChangeTime[fileName] = File.GetLastWriteTime(this.fileName);
             //return ConfigSerialize.DeserializeInfo(fileName, this.configType);
             return LoadRealConfig();
         }
         /// <summary>
         /// 加载配置文
         /// </summary>
         /// <param name="fileName">文件名</param>
         /// <param name="type">ʵ实体类型</param>
         /// <returns></returns>
         internal IConfiger LoadConfig(string fileName, Type type)
         {
             return LoadConfig(fileName, type, true);
         }
 
         /// <summary>
         /// 加载配置文件
         /// </summary>
         /// <param name="fileName">文件名</param>
         /// <param name="type">实体类型</param>
         /// <param name="isCache">是否要从缓存读取</param>
         /// <returns></returns>
         internal IConfiger LoadConfig(string fileName, Type type, bool isCache)
         {
             if (!isCache)
                 return LoadConfigFile(fileName, type);
             lock (lockHelper)
             {
                 if (DataCache.GetCache(fileName) == null)
                     DataCache.SetCache(fileName, LoadConfigFile(fileName, type));
                 DateTime newfileChangeTime = File.GetLastWriteTime(fileName);
                 if (!newfileChangeTime.Equals(fileChangeTime[fileName]))
                 {
                     DataCache.SetCache(fileName, LoadConfigFile(fileName, type));
                     return LoadConfigFile(fileName, type);
                 }
                 else
                 {
                     return DataCache.GetCache(fileName) as IConfiger;
                 }
             }
         }
         #endregion
 
         #region 重设配置类实例

         /// <summary>
         /// 重设配置类实例
         /// </summary>
         /// <returns></returns>
         internal IConfiger LoadRealConfig()
         {
             lock (lockHelper)
             {
                 DateTime newfileChangeTime = File.GetLastWriteTime(this.fileName);
                 if (!newfileChangeTime.Equals(this.fileChangeTime[this.fileName]))
                 {
                     IconfigInfo = ConfigSerialize.DeserializeInfo(ConfigFilePath, this.configType);
                     if (this.fileChangeTime.ContainsKey(this.fileName))
                         fileChangeTime.Remove(this.fileName);
                     this.fileChangeTime.Add(this.fileName, newfileChangeTime);
                 }
             }
             return IconfigInfo as IConfiger;
         }
         #endregion
 
         #region 保存配置

         /// <summary>
         /// 保存配置
         /// </summary>
         /// <returns></returns>
         internal bool SaveConfig()
         {
             lock (lockHelper)
             {
                 return ConfigSerialize.Serializer(ConfigFilePath, IconfigInfo);
             }
         }
         #endregion
 
         #endregion
    }
}