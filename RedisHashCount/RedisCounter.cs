using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace RedisHashCount
{
    public class RedisCounter
    {
        private StackExchangeRedisCacheClient Client { get; }
        
        public RedisCounter(string connStr)
        {            
            Client = new StackExchangeRedisCacheClient(ConnectionMultiplexer.Connect(connStr), new NewtonsoftSerializer());
        }

        public Dictionary<string, string> GetHashCounts(string prefix)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            //1) Get Keys
            var keys = Client.SearchKeys(prefix);

            //2) For each key, get the raw firm ID
            foreach (var key in keys)
            {
                var label = key.Replace(prefix, string.Empty);
                var lengths = Client.HashLength(key);
                results[label] = lengths.ToString();
            }
            return results;
        }




    }


}
