using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// Param List Info
    /// </summary>
    [Serializable]
    public class ParamInfoList
    {
        [XmlAttribute("Id")]
        public string Id
        {
            get;
            set;
        }

        [XmlAttribute("CommInfo")]
        public string CommInfo
        {
            get;
            set;
        }

        [XmlElement("ParamInfo")]
        public List<ParamInfo> ParamInfos
        {

            get;
            set;
        }
    }


    /// <summary>
    /// Param Info
    /// </summary>
    [Serializable]
    public class ParamInfo
    {
        [XmlAttribute("Num")]
        public string Num { get; set; }

        [XmlElement("Info")]
        public List<Info> Infos
        {
            get;
            set;
        }
    }

     /// <summary>
    /// Infomation
    /// </summary>
    [Serializable]
    public class Info
    {
        [XmlAttribute("Index")]
        public string Index { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
