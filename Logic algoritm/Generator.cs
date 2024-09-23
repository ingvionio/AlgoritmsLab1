using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public static class Generator
    {
        public static int[] Generate(int n, int nMin = 100, int nMax = 1000)
        {
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)

            {
                array[i] = random.Next(nMin,nMax);
            }
            return array;
        }
    }
}
