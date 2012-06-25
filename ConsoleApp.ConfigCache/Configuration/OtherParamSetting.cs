using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// Other Param Setting 
    /// </summary>
    [Serializable]
    public class OtherParamSetting: IKeyedObject
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }
        [XmlAttribute("value")]
        public string Value
        {
            get;
            set;
        }

        #region IKeyedObject Members

        public string Key
        {
            get { return this.Name; }
        }

        #endregion
    }
}
