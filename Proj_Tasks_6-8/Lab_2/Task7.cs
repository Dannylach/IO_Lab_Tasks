using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Lab_2
{
    class Task7
    {
        private FileStream stream;
        private byte[] data = new byte[1000];
        private AutoResetEvent autoReset;

        public void ReadFile()
        {
            string path = @"F:\Projekty\Studia\Inżynieria Oprogramowania\Lab2\zad6\zad6\bin\tzt.txt";

            stream = new FileStream(path, FileMode.Open);

            var data = new byte[stream.Length];
            var asyncResult = stream.BeginRead(data, 0, data.Length, null, null);

            stream.EndRead(asyncResult);

            Console.WriteLine(Encoding.UTF8.GetString(data));
        }
    }
}
