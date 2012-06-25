using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// Param Config Collection
    /// </summary>
    public class ParamConfigCollection : KeyedCollection<string, ParamInfoList>
    {
        protected override string GetKeyForItem(ParamInfoList item)
        {
            return item.Id;
        }
    }
}
