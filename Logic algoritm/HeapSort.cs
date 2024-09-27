using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class HeapSort : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            int len = array.Length;

            for(int i = len / 2 - 1; i >= 0; i--)
            {
                heapify(array, len, i);
            }

            for(int i = len - 1; i >= 0; i--)
            {
                int temp = Convert.ToInt32(array.GetValue(0));
                array.SetValue(Convert.ToInt32(array.GetValue(i)), 0);
                array.SetValue(temp, i);

                heapify(array, i, 0);
            }
        }
        public static void heapify(Array array, int len, int i)
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if(l < len && Convert.ToInt32(array.GetValue(l)) > Convert.ToInt32(array.GetValue(largest)))
            {
                largest = l;
            }
            if (r < len && Convert.ToInt32(array.GetValue(r)) > Convert.ToInt32(array.GetValue(largest)))
            {
                largest = r;
            }
            if(largest != i)
            {
                int temp = Convert.ToInt32(array.GetValue(i));
                array.SetValue(Convert.ToInt32(array.GetValue(largest)), i);
                array.SetValue(temp, largest);

                heapify(array, len, largest);
            }
        }
    }
}
