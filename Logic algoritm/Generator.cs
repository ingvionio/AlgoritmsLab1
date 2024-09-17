using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public static class Generator
    {
        public static int[] Generate(int n)
        {
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)

            {
                array[i] = random.Next(1,1000);
            }
            return array;
        }
    }
}
