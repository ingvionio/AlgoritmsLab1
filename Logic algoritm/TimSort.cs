using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class Run
    {
        public int startIndex;
        public int length;
    }
    public class TimSort : Algoritm
    {

        static int GetMinrun(int n)
        {
            int r = 0;           /* станет 1 если среди сдвинутых битов будет хотя бы 1 ненулевой */
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }
        public static void InsertionSort(Array arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                var temp = (IComparable)arr.GetValue(i);
                int j = i - 1;
                while (j >= left && ((IComparable)arr.GetValue(j)).CompareTo(temp) > 0)
                {
                    arr.SetValue(arr.GetValue(j),j + 1);
                    j--;
                }
                arr.SetValue(temp , j + 1);
            }
        }

        public static void Merge(Array arr, int startIndexLeft, int startIndexRight, int lengthRight)
        {
            int len1 = startIndexRight - startIndexLeft; // Длина левого подмассива
            int len2 = lengthRight + 1; // Длина правого подмассива

            // Создаем временные массивы для обоих подмассивов
            IComparable[] leftArr = new IComparable[len1];
            IComparable[] rightArr = new IComparable[len2];

            // Копируем элементы левого подмассива во временный массив
            for (int i = 0; i < len1; i++)
            {
                leftArr[i] = (IComparable)arr.GetValue(startIndexLeft + i);
            }

            // Копируем элементы правого подмассива во временный массив
            for (int i = 0; i < len2; i++)
            {
                rightArr[i] = (IComparable)arr.GetValue(startIndexRight + i);
            }

            int iLeft = 0, iRight = 0, iArr = startIndexLeft;

            // Указатели на массивы
            int gallopCount = 7; // Количество элементов для входа в режим галопа
            int countLeft = 0; // Счетчик для левого массива
            int countRight = 0; // Счетчик для правого массива

            // Основной цикл слияния
            while (iLeft < len1 && iRight < len2)
            {
                if (leftArr[iLeft].CompareTo(rightArr[iRight]) <= 0)
                {
                    arr.SetValue(leftArr[iLeft], iArr);
                    iLeft++;
                    countLeft++;
                    countRight = 0;
                }
                else
                {
                    arr.SetValue(rightArr[iRight], iArr);
                    iRight++;
                    countRight++;
                    countLeft = 0;
                }
                iArr++;

                if (countLeft >= gallopCount)
                {
                    int iLeftTemp = iLeft;
                    iLeft = Gallop(leftArr, iLeft, (IComparable)rightArr.GetValue(iRight), len1);
                    while (iLeftTemp < iLeft)
                    {
                        arr.SetValue(leftArr[iLeftTemp], iArr);
                        iLeftTemp++;
                        iArr++;
                    }
                }
                else if (countRight >= gallopCount)
                {
                    int iRightTemp = iRight;
                    iRight = Gallop(rightArr, iRight, (IComparable)leftArr.GetValue(iLeft), len2);

                    while (iRightTemp < iRight)
                    {
                        arr.SetValue(rightArr[iRightTemp], iArr);
                        iRightTemp++;
                        iArr++;
                    }
                }
            }

            // Если остались элементы в временном массиве, копируем их в основной массив
            while (iLeft < len1)
            {
                arr.SetValue(leftArr[iLeft], iArr);
                iLeft++;
                iArr++;
            }

            // Если остались элементы в правом массиве, копируем их в основной массив
            while (iRight < len2)
            {
                arr.SetValue(rightArr[iRight], iArr);
                iRight++;
                iArr++;
            }
        }

        private static int Gallop(Array array, int arrayStartIndex, IComparable value, int length)
        {
            int gallopStep = 1;
            int end = length;

            // Экспоненциальное увеличение шага
            while (arrayStartIndex + gallopStep < end && value.CompareTo(array.GetValue(arrayStartIndex + gallopStep)) >= 0)
            {
                gallopStep *= 2;
            }

            // Окончание экспоненциального поиска: устанавливаем границы для бинарного поиска
            int low = arrayStartIndex + gallopStep / 2;
            int high = Math.Min(arrayStartIndex + gallopStep, end - 1);

            // Выполняем бинарный поиск в пределах [low, high]
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (value.CompareTo(array.GetValue(mid)) < 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            // low указывает на место, куда должно быть вставлено значение
            return low;
        }


        static void StackPush(Array arr, Stack<Run> stack, int runStartIndex, int runLength)
        {
            Run run = new Run();
            run.startIndex = runStartIndex;
            run.length = runLength;

            if (stack.Count == 0)
            {
                stack.Push(run);
            }
            else if (stack.Count == 1 && stack.Peek().length > runLength)
            {
                stack.Push(run);
            }
            else if (stack.Count >= 2)
            {
                Run y = stack.Pop();
                if (stack.Peek().length > y.length + runLength)
                {
                    stack.Push(y);
                    stack.Push(run);
                }
                else
                {
                    if (stack.Peek().length < runLength)
                    {
                        Run x = stack.Pop();
                        Merge(arr, x.startIndex, y.startIndex, y.length);
                        x.length += y.length;
                        stack.Push(x);
                        stack.Push(run);
                    }
                    else
                    {
                        Merge(arr, y.startIndex, runStartIndex, runLength);
                        y.length += runLength;
                        stack.Push(y);
                    }
                }
            }

            else
            {
                Run x = stack.Pop();
                Merge(arr, x.startIndex, runStartIndex, runLength);
                x.length += runLength;
                stack.Push(x);

            }
        }

        public override void DoAlgoritm(Array arr)
        {
            if (arr == null || arr.Length == 0) { return; }
            int n = arr.Length;
            int minRun = GetMinrun(n);
            int i = 0;

            Stack<Run> stack = new Stack<Run>();

            while (i < n-1)
            {
                int runStartIndex = i;
                int runLength = 0;

                if (((IComparable)arr.GetValue(i)).CompareTo(arr.GetValue(i+1)) <= 0)
                {
                    while (i < n - 1 && ((IComparable)arr.GetValue(i)).CompareTo(arr.GetValue(i + 1)) <= 0)
                    {
                        runLength++;
                        i++;
                    }

                }

                else
                {
                    while (i < n - 1&& (((IComparable)arr.GetValue(i)).CompareTo(arr.GetValue(i + 1)) > 0))
                    {
                        runLength++;
                        i++;
                    }
                    Array.Reverse(arr, runStartIndex, runLength);
                    if (runStartIndex + minRun < n - 1)
                        runLength = Math.Max(minRun, runLength);

                    InsertionSort(arr, runStartIndex, runStartIndex + runLength);
                    StackPush(arr, stack, runStartIndex, runLength);
                }
            }

            while (stack.Count > 1)
            {
                Run y = stack.Pop();
                Run x = stack.Pop();
                Merge(arr, x.startIndex, y.startIndex, y.length);
                x.length += y.length;
                stack.Push(x);
            }
        }

    }
}
