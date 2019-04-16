using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ExercisingPerformanceCounters
{
    class Program
    {

        static void RunBigArrayDemo()
        {
            int count = 20000;
            var tasks = new List<Task>(count);
            Console.WriteLine("Creating some traffic for the BigArray Demo");
            Console.WriteLine("Thread large object Initialization Starting");
            for (int i = 0; i < count; i++)
            {
                if (i == 1)
                {
                    tasks.Add(new Task(() =>
                    {
                        var b = 75;
                        var c = DateTime.Now;
                        var d = new int[1] { 7 };
                        var e = new double[86000];
                        var h = new SampleObject();
                        h.c.BigArray[0] = h.c.BigArray[0] + 1.0;
                        Console.WriteLine($"Allocated objects are going on {GC.GetGeneration(h)}");
                        Console.WriteLine($"Allocated large property objects are going on {GC.GetGeneration(h.c)}");
                    }));
                }
                else
                {
                    tasks.Add(new Task(() =>
                    {
                        var b = 75;
                        var c = DateTime.Now;
                        var d = new int[1] {7};
                        var e = new double[86000];
                        var h = new SampleObject();
                        h.c.BigArray[0] = h.c.BigArray[0] + 1.0;
                    }));
                }
            }
            Console.WriteLine($"Thread large object Initialization Complete for {count} threads");

            Console.WriteLine($"Tasks allocated on GEN{GC.GetGeneration(tasks[0])}");
            Console.WriteLine($"The highest generation is {GC.MaxGeneration}");
            foreach (var task in tasks)
            {
                task.Start();
            }
            Console.WriteLine("All Tasks Started...");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Finished executing tasks for large objects.");
        }
        static void RunNoBigArrayDemo()
        {
            int count = 200000;
            var tasks = new List<Task>(count);
            Console.WriteLine("Creating some traffic for the NOT-BigArray Demo");
            Console.WriteLine("Thread value object creation starting");
            for (int i = 0; i < count; i++)
            {
                if (i == 1)
                {
                    tasks.Add(new Task(() =>
                    {
                        var b = 75;
                        var c = DateTime.Now;
                        var d = new int[1] { 7 };
                        var h = new NoBigSampleObject();
                        h.c.BigArray[0] = h.c.BigArray[0] + 1.0;
                        Console.WriteLine($"Allocated objects are going on {GC.GetGeneration(h)}");
                        Console.WriteLine($"Allocated small property objects are going on {GC.GetGeneration(h.c)}");
                    }));
                }
                else
                {
                    tasks.Add(new Task(() =>
                    {
                        var b = 75;
                        var c = DateTime.Now;
                        var d = new int[1] { 7 };
                        var h = new NoBigSampleObject();
                        h.c.BigArray[0] = h.c.BigArray[0] + 1.0;
                    }));
                }
            }
            Console.WriteLine($"Thread value object creation Complete for {count} threads");

            Console.WriteLine($"Tasks allocated on GEN{GC.GetGeneration(tasks[0])}");
            Console.WriteLine($"The highest generation is {GC.MaxGeneration}" );
            foreach (var task in tasks)
            {
                task.Start();
            }
            Console.WriteLine("All Tasks Started...");
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Finished executing tasks.");
        }

        static void RunTwo()
        {

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start Warmup of the GC Gens");
            Console.ReadLine();
            WarmUp();

            RunNoBigArrayDemo();
            Console.WriteLine();
            Console.WriteLine("First Run Completed.  Press Any Key to continue...");
            Console.WriteLine();
            Console.ReadLine();
            RunBigArrayDemo();
            
            Console.ReadLine();
        }
        

        private static void WarmUp()
        {
            Console.WriteLine("Warming up");
            var a = Console.Out;
            var b = TextWriter.Null;
            Console.SetOut(b);
            RunNoBigArrayDemo();
            a.WriteLine("Almost there...");
            RunBigArrayDemo();
            Console.SetOut(a);
            Console.WriteLine("Warm up complete");
            Console.ReadLine();
        }

        public class SampleObject
        {

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

        public class NoBigSampleObject
        {

            public NoBigSubClass c;
            public NoBigSampleObject()
            {
                c = new NoBigSubClass();
            }

        }

        public class NoBigSubClass
        {
            public double[] BigArray = new double[1];

            public NoBigSubClass()
            {
                for (int j = 0; j < 1; j++)
                {
                    BigArray[j] = 9.0;
                }
            }

        }
    }
}
//Console.WriteLine($"'h.c.BigArray' is on the GEN {GC.GetGeneration(h.c.BigArray)}");
//Console.WriteLine($"'b.c.BigArray' is on the GEN {GC.GetGeneration(b.c.BigArray)}");
