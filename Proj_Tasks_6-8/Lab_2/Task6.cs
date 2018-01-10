using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_2
{
    class Task6
    {
        private FileStream stream;
        private byte[] data = new byte[1000];
        private AutoResetEvent autoReset;

        public void ReadFile()
        {
            string path = @"F:\Projekty\Studia\Inżynieria Oprogramowania\Lab2\zad6\zad6\bin\tzt.txt";

            stream = new FileStream(path, FileMode.Open);
            stream.BeginRead(data, 0, data.Length, ReadCallback, null);

            autoReset = new AutoResetEvent(false);
            autoReset.WaitOne();
        }

        private void ReadCallback(IAsyncResult ar)
        {
            stream.EndRead(ar);
            stream.Close();

            Console.WriteLine(Encoding.ASCII.GetString(data));

            autoReset.Set();
        }
    }
}
