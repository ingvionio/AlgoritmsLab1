using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Algoritms.Logic
{
    public class TimeCounter
    {
        public static List<TimeSpan> TimeCount(int nMin, int nMax, Algoritm algoritm, int step, CancellationToken cancellationToken, int repetitions = 5)
        {
            List<TimeSpan> averageTimes = new List<TimeSpan>();

            for (int i = nMin; i <= nMax; i += step)
            {
                // Проверяем отмену
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                long totalTicks = 0;
                for (int j = 0; j < repetitions; j++)
                {
                    int[] nArray = Generator.Generate(i);
                    Stopwatch sw = Stopwatch.StartNew();
                    algoritm.DoAlgoritm(nArray);
                    sw.Stop();
                    totalTicks += sw.ElapsedTicks;
                }

                averageTimes.Add(new TimeSpan(totalTicks / repetitions));
            }

            return averageTimes;
        }
    }
}