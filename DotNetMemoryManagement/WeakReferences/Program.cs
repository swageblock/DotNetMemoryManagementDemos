using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateLookupData();
            Console.WriteLine("Lookup Cache created");
            Console.WriteLine("Creating some heap activity...");
            var a = new Task(() =>
            {
                DoCrazyStuffInParallel();
                Console.WriteLine("Parallel memory pressure completed");
            });
            a.Start();
            Console.WriteLine("Starting Lookups.");
            UseLookups(_theCache);
            Console.WriteLine("Lookups Complete.");
            a.Wait();
            Console.WriteLine("Parallel process wait completed.");
            Console.ReadLine();
        }

        private static TheCache _theCache;
        private static void CreateLookupData()
        {
            var basedata = CacheData.CreateData();
            _theCache = new TheCache(basedata);

        }

        private static void UseLookups(TheCache theCache)
        {
            int count = 20000;
            var tasks = new List<Task>(count);
            Action<object> weakAction = (object state) =>
            {
                var a = _theCache.GetWeakItem((int)state);
                Person p;
                Console.Write(!a.TryGetTarget(out p) ? "-" : ".");
            };
            Action<object> strongAction = (object state) =>
            {
                var a = _theCache.GetItem((int)state);
                //Console.WriteLine("Cache Hit");
            };
            for (int i = 0; i < count; i++)
            {
                if ((i % 5) == 0)
                {
                    tasks.Add(new Task(weakAction, (object)i));
                }
                else
                {
                    tasks.Add(new Task(strongAction, (object)i));

                }
            }


            Console.Write("Starting tasks a '.' indicates a hit and misses are indicated by '-'");
            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static void DoCrazyStuffInParallel()
        {
            var tasks = new List<Task>(20000);
            for (int i = 0; i < 20000; i++)
            {
                tasks.Add(new Task(() =>
                {
                    var h = new SampleObject();
                    h.c.BigArray[0] = h.c.BigArray[0] + 1.0;
                }));
            }
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());
        }
    }



    public class SampleObject
    {
        public double[] BigArray = new double[500];
        private decimal[] something = new decimal[500];

        public SubClass c;
        public SampleObject()
        {
            c = new SubClass();
        }

    }

    public class SubClass
    {
        public double[] BigArray = new double[86000];

        public SubClass()
        {
            for (int j = 0; j < 86000; j++)
            {
                BigArray[j] = 9.0;
            }
        }

    }
}
