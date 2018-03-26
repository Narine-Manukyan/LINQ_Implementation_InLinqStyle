using System;
using System.Collections.Generic;
using LINQImplementation;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> list = new List<int> { 1, 2, 5, 6, 7, 6, 9, 2, 3, 0, 6, 2, 3, 8 };
            foreach (var item in list)
                Console.Write(item + "\t");
            Console.WriteLine();

            IEnumerable<string> list2 = list.ExtensionSelect(x => x.ToString());
            foreach (var item in list2)
                Console.Write(item + "\t");
            Console.WriteLine();

            IEnumerable<int> list3 = list.ExtensionWhere(x => x > 4);
            foreach (var item in list3)
                Console.Write(item + "\t");
            Console.WriteLine();

            IEnumerable<int> list4 = LINQ.ExtensionWhere(list.ExtensionToList(), x => x > 5);
            foreach (var item in list4)
                Console.Write(item + "\t");
            Console.WriteLine();

            var list5 = list.ExtensionToDictionary(x => x.ToString()).ExtensionGroupBy(x => x.Key);
            foreach (var item in list5)
                Console.Write("{" + item.Key + "\t");
            Console.WriteLine();

            var list6 = list.ExtensionOrderByDescending(x => x);
            foreach (var item in list6)
                Console.Write(item + "\t");
            Console.WriteLine();

            Dictionary<string, int> dict = list.ExtensionToDictionary(x => x.ToString());
            foreach (var item in dict)
                Console.Write("[" + item.Key + "\t" + item.Value + "]\t");
            Console.WriteLine();
        }
    }
}
