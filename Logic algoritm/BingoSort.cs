using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    using System;

    public class BingoSort:Algoritm
    {
        // Метод для выполнения бинго сортировки
        public override void DoAlgoritm(Array array)
        {
            int max = array.Length -1;
            var nextValue = (IComparable)array.GetValue(max);
            for (int i = max-1; i >-1; i--)
            {
                if (nextValue.CompareTo(array.GetValue(i)) < 0)
                    nextValue = (IComparable)array.GetValue(i);
            }

            while (max >= 0 && nextValue.CompareTo(array.GetValue(max)) == 0)
            {
                max --;
            }
            // Индекс отсортированного участка
            while (max >= 0)
            {
                var value = nextValue;
                nextValue = (IComparable)array.GetValue(max);

                for (int i = max-1;i > -1; i--)
                {
                    if (value.CompareTo(array.GetValue(i)) == 0)
                    {
                        Swap(array, i, max);
                        max --;
                    }
                    else if(nextValue.CompareTo(array.GetValue(i)) < 0)
                    {
                        nextValue = (IComparable)array.GetValue(i) ;
                    }                   
                }
                while (max >= 0 && nextValue.CompareTo(array.GetValue(max)) == 0)
                {
                    max--;
                }
            }
        }

        // Метод для обмена элементов
        private static void Swap(Array array, int pivot, int i)
        {
            var temp = array.GetValue(i);
            array.SetValue(array.GetValue(pivot), i);
            array.SetValue(temp, pivot);
        }
    }

}
