using StackExchange.Redis;
using System;

namespace POC_AWSRedisCache
{
    class Program
    {
        readonly int devicesCount = 5;
        IDatabase cache = RedisConnectionHelper.Connection.GetDatabase();

        static void Main(string[] args)
        {
            Console.WriteLine("Testing Redis Cache.");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("AWS- AmbujRedisDB");
            Console.WriteLine("Endpoint: redis - 13990.c212.ap - south - 1 - 1.ec2.cloud.redislabs.com:13990");
            Console.WriteLine("------------------------------------");
            var program = new Program();
            Console.WriteLine();
            Console.WriteLine("Saving random data in cache");
            Console.WriteLine("------------------------------------");
            program.SaveBigData();
            Console.WriteLine();
            Console.WriteLine("Reading data from cache");
            Console.WriteLine("------------------------------------");
            program.ReadData();
            Console.WriteLine();
            Console.WriteLine("Clearing data from cache");
            Console.WriteLine("------------------------------------");
            program.ClearCache();

            Console.ReadLine();
        }


        public void ReadData()
        {
            for (int i = 1; i < devicesCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                Console.WriteLine($"Device_Status:{i} ,Value={value}");
            }
            Console.WriteLine("------------------------------------");
        }

        public void SaveBigData()
        {
            
            var rnd = new Random();

            for (int i = 1; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 5);
                Console.WriteLine($"Device_Status:{i} : {value}");
                cache.StringSet($"Device_Status:{i}", value);
            }
            Console.WriteLine("------------------------------------");
        }
        public void ClearCache()
        {
            
            int j = 0;
            for (int i = 1; i < devicesCount; i++)
            {
                if (cache.KeyDelete($"Device_Status:{i}"))
                {
                    j++;
                    Console.WriteLine($"Deleted Device_Status:{i}.");
                }
            }
            Console.WriteLine($"Deleted {j} keys.");
            Console.WriteLine("------------------------------------");
        }
    }
}
