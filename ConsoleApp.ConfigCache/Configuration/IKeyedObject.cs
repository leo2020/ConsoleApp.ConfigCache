using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConfigCache.Configuration
{
    /// <summary>
    /// IKeyed Object
    /// </summary>
    public interface IKeyedObject
    {
        string Key { get; }
    }
}
