using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Algoritms.Logic
{
    public class TimeCounter
    {
        public static List<TimeSpan> TimeCount(int nMin, int nMax, Algoritm algoritm, int step = 1)
        {
            Stopwatch sw = new Stopwatch();

            Array array = Generator.Generate(nMax);

            List<TimeSpan> time = new List<TimeSpan>();

            for (int i = nMin; i <= nMax; i = i+step)
            {
                int[] nArray = new int[i];
                Array.Copy(array, nArray, i);
                sw.Start();
                algoritm.DoAlgoritm(nArray);
                sw.Stop();
                time.Add(sw.Elapsed);
                sw.Reset();
            }

            return time;
        }
    }
}
