using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using Form_Functions;

namespace TestingPurp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void Main()
        {
            List<int> l = new List<int>();
            int curr = int.MaxValue;
            while (curr > 0)
            {
                var readLine = int.Parse(Console.ReadLine());
                l.Add(readLine);
                if (readLine < curr) 
                    curr = readLine;
            }

            int temp = 0;
            for (int i = 0; i < l.Count; i++)
            {
                if (temp < l[i]) temp = l[i];
            }
            Console.WriteLine($"Highest - {temp} Lowest - {curr}");
        }
    }
}
