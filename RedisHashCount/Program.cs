using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace RedisHashCount
{
    class Program
    {
        private static string FileName = $"_{ConfigurationManager.AppSettings["FileName"]}{DateTime.Now:yyyyMMddHHmmss}.txt";

        static void Main(string[] args)
        {
            var ctr = new RedisCounter(ConfigurationManager.AppSettings["RedisConnection"]);
            var results = ctr.GetHashCounts(ConfigurationManager.AppSettings["ClientKeyPrefix"]);


            var items = results.OrderBy(a => a.Key).Select(b => $"{b.Key}\t{b.Value.ToString()}").ToList();


            items.Insert(0, "FirmID\tClientCount");
            
            File.WriteAllLines(FileName, items);




        }
    }
}
