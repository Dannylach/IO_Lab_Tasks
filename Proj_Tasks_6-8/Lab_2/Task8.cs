using System;
using System.Diagnostics;

namespace Lab_2
{
    delegate long DelegateType(int argument);

    class Task8
    {
        static DelegateType fact;
        static DelegateType fib;

        public void Run()
        {
            const int nCount = 45;

            var counter = new Stopwatch();

            fact = new DelegateType(FibonacciRec);
            fib = new DelegateType(FibonacciIter);

            counter.Start();

            fact.Invoke(nCount);
            fib.Invoke(nCount);

            counter.Stop();

            Console.WriteLine("Synch: " + counter.Elapsed);

            counter.Reset();
            counter.Start();

            var factAsync = fact.BeginInvoke(nCount, null, null);
            var fibAsync = fib.BeginInvoke(nCount, null, null);

            fact.EndInvoke(factAsync);
            fib.EndInvoke(fibAsync);

            counter.Stop();

            Console.WriteLine("Asynch: " + counter.Elapsed);
        }

        private static long FibonacciRec(int number)
        {
            if (number == 0) return 1;
            if (number == 1) return 1;
            return FibonacciRec(number - 1) + FibonacciRec(number - 2);
        }

        private static long FibonacciIter(int number)
        {
            int FibNumber = 1;
            int PrevFibNumb = 1;
            for (int i = 1; i <= number; i++)
            {
                int temp = FibNumber;
                FibNumber += PrevFibNumb;
                PrevFibNumb = temp;
            }
            return FibNumber;
        }

        private static long FactorialRec(int number)
        {
            if (number == 0) return 1;
            return number * FactorialRec(number - 1);
        }

        private static long FactorialIter(int number)
        {
            if (number == 0) return 1;
            int factorial = 1;
            for (int i = 1; i < number; i++)
            {
                factorial = factorial * i;
            }
            return factorial;
        }
    }
}
