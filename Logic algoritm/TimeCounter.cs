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
        public static TimeSpan[] TimeCount(int nMin, int nMax, Algoritm algoritm)
        {
            Stopwatch sw = new Stopwatch();

            Array array = Generator.Generate(nMax);

            TimeSpan[] time = new TimeSpan[nMax-nMin+1];

            for (int i = nMin; i <= nMax; i = i+100)
            {
                int[] nArray = new int[i];
                Array.Copy(array, nArray, i);
                sw.Start();
                algoritm.DoAlgoritm(nArray);
                sw.Stop();
                time[i-nMin] = sw.Elapsed;
                sw.Reset();
        
            }

            return time;
        }
    }
}
