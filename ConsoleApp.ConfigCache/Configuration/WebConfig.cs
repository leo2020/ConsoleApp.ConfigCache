using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// WebConfig Class implement Interface IConfiger
    /// </summary>
    [Serializable]
    [XmlRoot("WebConfig")]
    public class WebConfig : IConfiger
    {
        [XmlArray("ParamConfigList")]
        [XmlArrayItem(ElementName = "ParamConfig")]
        public ParamConfigCollection ParamConfigList
        {
            get;
            set;
        }

        [XmlArray("settings")]
        [XmlArrayItem(ElementName = "setting")]
        public KeyedCollection<OtherParamSetting> Settings
        {
            get;
            set;
        }

        [XmlElement("PageSize")]
        public string PageSize { get; set; }
        [XmlElement("CategoryID")]
        public string CategoryID { get; set; }
    }
}
