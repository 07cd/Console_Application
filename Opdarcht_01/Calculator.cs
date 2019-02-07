using System;

namespace Opdracht_01
{
    /// <summary>
    /// this class is used to calculate different calculations  
    /// </summary>
    internal class Calculator
    {
        /// <summary>
        /// Instance for adding two integers to each other
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        public void Plus(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 + var2;
            Console.WriteLine($"Sum of {var1} + {var2} = {sum}");
            Console.ReadKey();
        }

        /// <summary>
        /// Instance to substract two integers
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        public void Min(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 - var2;
            Console.WriteLine($"Sum of {var1} - {var2} = {sum}");
            Console.ReadKey();
        }

        /// <summary>
        /// Instance to multilple two integers
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        public void Mult(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 * var2;
            Console.WriteLine($"Sum of {var1} * {var2} = {sum}");
            Console.ReadKey();
        }

        /// <summary>
        /// Instance to divived two integers
        /// </summary>
        /// <param name="var1"></param>
        /// <param name="var2"></param>
        public void Div(int var1, int var2)
        {
            Console.Clear();
            var sum = var1 / var2;
            Console.WriteLine($"Sum of {var1} / {var2} = {sum}");
            Console.ReadKey();
        }
    }
}