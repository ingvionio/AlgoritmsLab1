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

        private static void QuickSort(Array array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                QuickSort(array, low, partitionIndex - 1);
                QuickSort(array, partitionIndex + 1, high);
            }
        }

        private static int Partition(Array arr, int low, int high)
        {
            IComparable pivot = (IComparable)arr.GetValue(high);
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (((IComparable)arr.GetValue(j)).CompareTo(pivot) < 0)
                {
                    i++;

                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, high);
            return (i + 1);
        }

        private static void Swap(Array arr, int i, int j)
        {
            object temp = arr.GetValue(i);
            arr.SetValue(arr.GetValue(j), i);
            arr.SetValue(temp, j);
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


