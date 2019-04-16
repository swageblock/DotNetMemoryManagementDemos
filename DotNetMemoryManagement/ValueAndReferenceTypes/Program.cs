using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueAndReferenceTypes
{
    class Program
    {
        class SimpleWrapper
        {
            public int value;
        }

        static void RunOne()
        {
            Console.WriteLine("Assigning Values to int variables");
            int a = 7;
            int b = a;
            b = 9;
            Console.WriteLine($"The value of a and b are: {a} - {b}");

            Console.WriteLine("Assigning Values to wrapped objects wrapping int values");
            SimpleWrapper c = new SimpleWrapper() { value = 7 };
            SimpleWrapper d = c;
            d.value = 9;
            Console.WriteLine($"The value of c.value and d.value are: {c.value} - {d.value}");

            Console.ReadLine();
        }

        struct SimpleStruct
        {
            public int value;
        }

        static void RunTwo()
        {
            Console.WriteLine("Assigning Values to int variables");
            int a = 7;
            int b = int.Parse(a.ToString());
            b = 9;
            Console.WriteLine($"The value of a and b are: {a} - {b}");

            Console.WriteLine("Assigning Values to wrapped struct wrapping int values");
            SimpleStruct c = new SimpleStruct() { value = 7 };
            SimpleStruct d = c;
            d.value = 9;
            Console.WriteLine($"The value of c.value and d.value are: {c.value} - {d.value}");

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            RunOne();
           // RunTwo();
        }
    }
}

