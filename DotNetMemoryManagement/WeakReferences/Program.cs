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
            Console.WriteLine("Creating some heap activity...");
            var a = new Task(()=>
            {
                DoCrazyStuffInParallel();
            });
            CreateLookupData();
            UseLookups();
        }

        private static void CreateLookupData()
        {
            throw new NotImplementedException();
        }

        private static void UseLookups()
        {
            throw new NotImplementedException();
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
