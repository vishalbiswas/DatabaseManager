using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassicSwitcher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Type> list = Assembly.GetExecutingAssembly().GetTypes().ToList();
            string thisName = MethodBase.GetCurrentMethod().DeclaringType.Name;
            int assemblyNameSize = Assembly.GetExecutingAssembly().GetName().Name.Length;

            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Name == thisName)
                {
                    list.RemoveAt(i);
                    if (i == list.Count - 1) break;
                }
                Console.WriteLine(list[i].ToString().Substring(assemblyNameSize + 1));
            }

            Console.Write("Launch the App: ");
            short choice = Convert.ToInt16(Console.ReadLine());
            if (choice > 0 && choice <= list.Count) {
                Console.Clear();
                object instance = Activator.CreateInstance(list[choice + 1]);
            }
        }
    }
}
