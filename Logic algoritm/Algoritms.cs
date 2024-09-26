using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Algoritms.Logic
{
    public abstract class Algoritm
    {
        public abstract void DoAlgoritm(Array array);
    }

    public class Sum : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            long sumValue = 0;

            for(int i = 0; i < array.Length; i++)
            {
                sumValue += Convert.ToInt64(array.GetValue(i));
            }
        }
    }

    public class QuickSortAlgoritm : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        public static Array QuickSort(Array array, int minIndex, int maxIndex)
        {
            if(minIndex >= maxIndex)
            {
                return array;
            }

            int pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static int Partition(Array array, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;

            for(int i = minIndex; i< maxIndex; i++)
            {
                if (Convert.ToInt64(array.GetValue(i)) < Convert.ToInt64(array.GetValue(maxIndex)))
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
            long temp = Convert.ToInt64(array.GetValue(i));
            array.SetValue(array.GetValue(pivot), i);
            array.SetValue(temp, pivot);
        }
    }

    public class NaiveAssessment : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            double sum = 0;

            for(int i = 0; i < array.Length; i++)
            {
                double power = i - 1;
                sum += Convert.ToDouble(array.GetValue(i)) * Math.Pow(1.5, power);
            }
        }
    }

    public class GornerMethod : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            double result = Convert.ToDouble(array.GetValue(0));

            for(int i = 1; i < array.Length; i++)
            {
                result = result * 1.5 + Convert.ToDouble(array.GetValue(i));
            }
        }
    }
}


