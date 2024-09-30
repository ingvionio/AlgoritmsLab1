using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class QuickSortAlgoritm : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        public static Array QuickSort(Array array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            int pivotIndex = Separation(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static int Separation(Array array, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;

            for (int i = minIndex; i < maxIndex; i++)
            {
                if (Convert.ToInt32(array.GetValue(i)) < Convert.ToInt32(array.GetValue(maxIndex)))
                {
                    pivot++;

                    Swap(array, pivot, i);
                }
            }

            pivot++;
            Swap(array, pivot, maxIndex);
            return pivot;
        }

        private static void Swap(Array array, int pivot, int i)
        {
            int temp = Convert.ToInt32(array.GetValue(i));
            array.SetValue(array.GetValue(pivot), i);
            array.SetValue(temp, pivot);
        }
    }
}
