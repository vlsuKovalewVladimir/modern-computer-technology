using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string item in args)
            {
                Console.WriteLine(item);
            }
        }
    }
}
