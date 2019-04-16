using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    class TheCache
    {
        public WeakReference<Person> GetWeakItem(int employeeId)
        {
            return weakReferences[employeeId];
        }

        public Person GetItem(int employeeId)
        {
            return strongReferences[employeeId];
        }

        private readonly Dictionary<int, Person> strongReferences = new Dictionary<int, Person>();
        private readonly Dictionary<int, WeakReference<Person>> weakReferences = new Dictionary<int, WeakReference<Person>>();


        public TheCache(List<Person> people)
        {
            foreach (var person in people)
            {
                if ((person.EmployeeId % 5) == 0)
                {
                    weakReferences.Add(person.EmployeeId, new WeakReference<Person>(person));
                }
                else
                {
                    strongReferences.Add(person.EmployeeId, person);
                }
            }

        }
    }
}
