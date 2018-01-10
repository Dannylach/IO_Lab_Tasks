using System;
using System.Threading;

namespace Lab_1
{
    class Task1
    {
        public static void Main()
        {
            var firstWaitingTime = 1000;
            var secondWaitingTime = 2000;
            ThreadPool.QueueUserWorkItem(ThreadProc, firstWaitingTime);
            ThreadPool.QueueUserWorkItem(ThreadProc, secondWaitingTime);
            Thread.Sleep(firstWaitingTime + secondWaitingTime);
        }

        private static void ThreadProc(Object stateInfo)
        {
            var waitingTime = (int)stateInfo;
            Thread.Sleep(waitingTime);
            Console.WriteLine("Czekalem {0}", waitingTime);
        }
    }
}
