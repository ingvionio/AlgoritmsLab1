using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Algoritms.Logic
{
    public class TimeCounter
    {
        public static List<TimeSpan> TimeCount(int nMin, int nMax, Algoritm algoritm, int step, CancellationToken cancellationToken)
        {
            Stopwatch sw = new Stopwatch();
            Array array = Generator.Generate(nMax);
            List<TimeSpan> time = new List<TimeSpan>();

            for (int i = nMin; i <= nMax; i += step)
            {
                // Проверяем, не запрошена ли отмена
                if (cancellationToken.IsCancellationRequested)
                {
                    // Выбрасываем OperationCanceledException, чтобы сигнализировать об отмене
                    throw new OperationCanceledException();
                }

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