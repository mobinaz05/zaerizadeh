using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiFunctionProgram
{
    public class Keyboard
    {
        public int GetNumber()
        {
            Console.Write("Enter a number: ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return number;
        }

        public bool GetYesOrNo(string message)
        {
            Console.Write($"{message} (y/n): ");
            while (true)
            {
                var input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.Y)
                    return true;
                else if (input == ConsoleKey.N)
                    return false;
                else
                    Console.WriteLine("Invalid input. Press 'y' for Yes or 'n' for No.");
            }
        }

        public void GetHomeControl()
        {
            Console.WriteLine("\nPress ESC to exit, SPACE to clear the screen.");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    Console.WriteLine("Screen cleared.");
                    break;
                }
            }
        }
    }

    public class Calculator
    {
        public void PrimeNumbers(int number)
        {
            if (number <= 1000)
                SimplePrime(number);
            else
                OptimizedPrime(number);
        }

        private void SimplePrime(int max)
        {
            Console.WriteLine("Using Simple Prime Method...");
            var timer = Stopwatch.StartNew();

            for (int i = 2; i <= max; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    Console.Write($"{i} ");
            }

            timer.Stop();
            Console.WriteLine($"\nExecution time: {timer.ElapsedMilliseconds}ms");
        }

        private void OptimizedPrime(int max)
        {
            Console.WriteLine("Using Optimized Prime Method...");
            var timer = Stopwatch.StartNew();

            List<int> primes = new List<int>();

            for (int i = 2; i <= 1000; i++)
            {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    primes.Add(i);
            }

            for (int i = 1001; i <= max; i++)
            {
                bool isPrime = true;
                int limit = (int)Math.Sqrt(i);
                foreach (var prime in primes)
                {
                    if (prime > limit)
                        break;
                    if (i % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    Console.Write($"{i} ");
            }

            timer.Stop();
            Console.WriteLine($"\nExecution time: {timer.ElapsedMilliseconds}ms");
        }

        public void MirrorNumbers(int number)
        {
            if (number > 100000)
                MirrorNumbersMethod1(number);
            else
                MirrorNumbersMethod2(number);
        }

        private void MirrorNumbersMethod1(int max)
        {
            Console.WriteLine("Using Mirror Numbers Method 1...");
            for (int i = 1; i <= max; i++)
            {
                if (IsMirrorNumber(i))
                    Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        private void MirrorNumbersMethod2(int max)
        {
            Console.WriteLine("Using Mirror Numbers Method 2...");
            for (int i = 1; i <= max; i++)
            {
                if (IsPalindrome(i))
                    Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        private bool IsMirrorNumber(int number)
        {
            int reversed = 0, original = number;
            while (number > 0)
            {
                int digit = number % 10;
                reversed = reversed * 10 + digit;
                number /= 10;
            }
            return original == reversed;
        }

        private bool IsPalindrome(int number)
        {
            int reversed = 0, original = number;
            while (number > 0)
            {
                int remainder = number % 10;
                reversed = reversed * 10 + remainder;
                number /= 10;
            }
            return original == reversed;
        }

        public void DecisionTree()
        {
            int low = 1, high = 100;
            Console.WriteLine("Think of a number between 1 and 100.");
            while (low <= high)
            {
                int mid = (low + high) / 2;
                Console.WriteLine($"Is your number {mid}?");

                var keyboard = new Keyboard();
                bool isYes = keyboard.GetYesOrNo("Is this correct?");
                if (isYes)
                {
                    Console.WriteLine($"Your number is {mid}!");
                    break;
                }

                bool isGreater = keyboard.GetYesOrNo($"Is your number greater than {mid}?");
                if (isGreater)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var keyboard = new Keyboard();
            var calculator = new Calculator();

            while (true)
            {
                Console.WriteLine("Select a program:");
                Console.WriteLine("A: Prime Number");
                Console.WriteLine("B: Mirror Number");
                Console.WriteLine("C: Decision Tree");

                var input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.A)
                {
                    Console.WriteLine("\nPrime Number Program");
                    int number = keyboard.GetNumber();
                    calculator.PrimeNumbers(number);
                }
                else if (input == ConsoleKey.B)
                {
                    Console.WriteLine("\nMirror Number Program");
                    int number = keyboard.GetNumber();
                    calculator.MirrorNumbers(number);
                }
                else if (input == ConsoleKey.C)
                {
                    Console.WriteLine("\nDecision Tree Program");
                    calculator.DecisionTree();
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }

                keyboard.GetHomeControl();
            }
        }
    }
}
