using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_1
{
    class Task5
    {
        private long _sumed = 0;
        private object _locker = new object();


        /// <summary>
        /// Problemem związanym z tym przykładem jest niemożliwość stworzenia więcej niż 64 wątków
        /// </summary>
        public static void Main(int arrSize = 64, int arrFragment = 1000)
        {
            var summingArray = new int[arrSize];
            var randGenerator = new Random();
            var threadCount = Math.Ceiling(arrSize / (double)arrFragment);
            var waitHandles = new WaitHandle[(int)threadCount];
            var counter = 0;
            var bottomNumber = 0;
            var topNumber = 10;

            for (int i = 0; i < arrSize; i++)
            {
                summingArray[i] = randGenerator.Next(bottomNumber, topNumber);
            }

            Console.WriteLine($"Suma w tablicy: {summingArray.Sum()}");
            
            for (int i = 0; i < arrSize; i += arrFragment)
            {
                var handle = new AutoResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), new TaskInfo(summingArray, i, i + arrFragment - 1, handle));
                waitHandles[counter] = handle;
                counter++;
            }

            WaitHandle.WaitAll(waitHandles);

            Console.WriteLine($"Suma obliczona przez wątki: {_sumed}");
            Console.ReadKey();
        }

        public void ThreadProc(object stateInfo)
        {
            lock (_locker)
            {
                var data = stateInfo as TaskInfo;

                for (int i = data.StartingPoint; i <= data.EndingPoint; i++)
                {
                    _sumed += data.OperatingArray[i];
                    data.Handle.Set();
                }
            }
        }
    }
}
