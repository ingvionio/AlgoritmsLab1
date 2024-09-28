using System;

namespace Algoritms.Logic
{
    public class GnomeSort : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            int index = 0;
            int n = array.Length;

            while (index < n)
            {
                if (index == 0)
                {
                    index++;
                }

                if (((IComparable)array.GetValue(index)).CompareTo(array.GetValue(index - 1)) >= 0)
                {
                    index++;
                }
                else
                {
                    Swap(array, index, index - 1);
                    index--;
                }
            }
        }

        private static void Swap(Array arr, int i, int j)
        {
            object temp = arr.GetValue(i);
            arr.SetValue(arr.GetValue(j), i);
            arr.SetValue(temp, j);
        }
    }
}