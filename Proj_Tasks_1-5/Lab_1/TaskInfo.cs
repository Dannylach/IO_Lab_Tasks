using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_1
{
    class TaskInfo
    {
        public int[] OperatingArray { get; set; }
        public int StartingPoint { get; set; }
        public int EndingPoint { get; set; }
        public AutoResetEvent Handle { get; set; }

        public TaskInfo(int[] operatingArray, int startingPoint, int endingPoint, AutoResetEvent handle)
        {
            this.OperatingArray = operatingArray;
            this.StartingPoint = startingPoint;
            this.EndingPoint = endingPoint;
            this.Handle = handle;
        }
    }
}
