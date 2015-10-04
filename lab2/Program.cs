using System;
using static System.Console;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string item in args)
            {
                WriteLine(item);
            }
        }
    }
}
