using System;

namespace Opdracht_01
{
    /// <summary>
    /// this class is used to calculate different calculations  
    /// </summary>
    internal class Calculator
    {
        public void Plus(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 + var2;
            Console.WriteLine($"Sum of {var1} + {var2} = {sum}");
            Console.ReadKey();
        }

        public void Min(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 - var2;
            Console.WriteLine($"Sum of {var1} - {var2} = {sum}");
            Console.ReadKey();
        }

        public void Mult(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 * var2;
            Console.WriteLine($"Sum of {var1} * {var2} = {sum}");
            Console.ReadKey();
        }

        public void Div(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 / var2;
            Console.WriteLine($"Sum of {var1} / {var2} = {sum}");
            Console.ReadKey();
        }
    }
}