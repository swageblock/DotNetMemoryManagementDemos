using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    public static class CacheData
    {
        public static List<Person> CreateData()
        {
            Random gen = new Random();
            var a = new List<Person>();
            for (int i = 0; i < 200000; i++)
            {
                a.Add(new Person() { Name = $"Person-{i}", DOB = RandomDay(gen), EmployeeId = i});
            }

            return a;
        }
       public static DateTime RandomDay(Random generator)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(generator.Next(range));
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public int EmployeeId { get; set; }
    }
}
