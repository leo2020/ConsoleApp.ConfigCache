using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.ConfigCache.Configuration;

namespace ConsoleApp.ConfigCache
{
    class Program
    {
        static void Main(string[] args)
        {
            string strPageSize = ConfigFactory.Instance.GetConfig<WebConfig>().PageSize;
            Console.WriteLine("PageSize:" + strPageSize);
            string strCategoryID = ConfigFactory.Instance.GetConfig<WebConfig>().CategoryID;
            Console.WriteLine("CategoryID:" + strCategoryID);
            Console.ReadLine();
        }
    }
}