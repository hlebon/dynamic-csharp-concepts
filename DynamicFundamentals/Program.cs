using System;
using System.Reflection;

namespace DynamicFundamentals
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DynamicAndObjectTypes();

            Console.WriteLine("\n\nPress enter to exit...");
            Console.ReadLine();
        }

        //static binding
        private static void OutputTimeStaticBinding()
        {
            DateTime dt = DateTime.Now;
            string time = dt.ToShortTimeString();
            Console.WriteLine(time);
        }

        //DYNAMIC CONVERSION

        //the compiler doesnt know about it in compiler time
        private static void OutputTimeDynamicBinding()
        {
            dynamic dt = DateTime.Now;
            string time = dt.ToShortTimeString();
            Console.WriteLine(time);
        }

        //get error at runtime ; runtimeBinderException
        private static void OutputTimeDynamicBindingError()
        {
            dynamic dt = DateTime.Now;
            string time = dt.whatEver();
            Console.WriteLine(time);
        }

        //conver from int to dynamic and from dynamic to int
        //must be convertible at runtime
        public static void ImpliciyDynamicConversions()
        {
            int i = 42;
            dynamic di = i;
            int i2 = di;

            Console.WriteLine($"i = {i}, di = {di}, i2 = {i2}");

            //this two produces error at runtime
            /*string s = "hello";
            dynamic ds = s;
            int x = ds;*/

            /*long l = 99999;
            dynamic dl = l;
            int y = dl;*/

            //this work
            long l = 99999;
            dynamic dl = l;
            int y = (int)dl;

            //change at run time //this work
            dynamic z = "hi there";
            Console.WriteLine($"z is a {z.GetType()} = {z}");

            z = 42;
            Console.WriteLine($"z is a {z.GetType()} = {z}");
        }

        //RUNTIME METHOD RESOLUTION
        public static void CallOverloads()
        {
            int i = 42;
            //PrintMe(i); //overload ocurrs in compile time

            dynamic d;
            Console.WriteLine("Create [i]nt or [d]ouble");
            ConsoleKeyInfo choice = Console.ReadKey(intercept: true);
            if (choice.Key == ConsoleKey.I)
            {
                d = 99;
            }
            else
            {
                d = 55.5;
            }
            PrintMe(d);
        }

        //dynamic and object are the same type, the diff obj not allow to perform dynamic operation
        public static void PrintMe(int value)
        {
            Console.WriteLine($"PrintMe(int) called value: {value}");
        }

        public static void PrintMe(long value)
        {
            Console.WriteLine($"PrintMe(long) called value: {value}");
        }

        public static void PrintMe(dynamic value)
        {
            Console.WriteLine($"PrintMe(dynamic) called value: {value}");
        }

        //DYNAMIC AND OBJECT TYPES DEMO
        private class Customer
        {
            public object FirstName { get; set; }
            public dynamic SecondName { get; set; }
        }

        public static void DynamicAndObjectTypes()
        {
            Customer c = new Customer();

            PropertyInfo firstNameProperty = typeof(Customer).GetProperty("FirstName");
            foreach (var customAttributeData in firstNameProperty.CustomAttributes)
            {
                Console.WriteLine(customAttributeData);
            }

            Console.WriteLine($"{firstNameProperty.PropertyType} FirstName");
            Console.WriteLine();

            PropertyInfo secondNameProperty = typeof(Customer).GetProperty("SecondName");
            foreach (var customAttributeData in secondNameProperty.CustomAttributes)
            {
                Console.WriteLine(customAttributeData);
            }

            Console.WriteLine($"{secondNameProperty.PropertyType} SecondName");
            Console.WriteLine();
        }

        //LIMITATIONS OF CALLABLE METHODS
    }
}