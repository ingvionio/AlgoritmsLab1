using System;
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

        public static void Merge(Array arr, int sartIndexLeft, int startIndexRight, int lenghtRight)
        {
            
            int len1 = startIndexRight - sartIndexLeft ;
            int len2 = lenghtRight;

            IComparable[] leftArr = new IComparable[len1];
            IComparable[] rightArr = new IComparable[len2];

            for (int i = 0; i < len1; i++)
            {
                leftArr[i] = (IComparable)arr.GetValue(sartIndexLeft + i);
            }

            for (int i = 0; i < len2; i++)
            {
                rightArr[i] = (IComparable)arr.GetValue(startIndexRight + i);
            }

            int iLeft = 0, iRight = 0, iArr = sartIndexLeft;

            while (iLeft < len1 && iRight < len2)
            {
                if (leftArr[iLeft].CompareTo(rightArr[iRight]) <= 0)
                {
                    arr.SetValue(leftArr[iLeft], iArr);
                    iLeft++;
                }
                else
                {
                    arr.SetValue(rightArr[iRight], iArr);
                    iRight++;
                }
                iArr++;
            }

            while (iLeft < len1)
            {
                arr.SetValue(leftArr[iLeft], iArr);
                iLeft++;
                iArr++;
            }

            while (iRight < len2)
            {
                arr.SetValue(rightArr[iRight], iArr);
                iRight++;
                iArr++;
            }
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
                    while (i<n-1 && (runLength < minRun || ((IComparable)arr.GetValue(i)).CompareTo(arr.GetValue(i + 1)) <= 0))
                    {
                        runLength++;
                        i++;
                    }

                    InsertionSort(arr, runStartIndex, runStartIndex + runLength);
                    StackPush(arr, stack, runStartIndex, runLength);
                }

                else
                {
                    while (i < n - 1&& (runLength < minRun || ((IComparable)arr.GetValue(i)).CompareTo(arr.GetValue(i + 1)) > 0))
                    {
                        runLength++;
                        i++;
                    }

                    InsertionSort(arr, runStartIndex, runStartIndex + runLength);
                    StackPush(arr, stack, runStartIndex, runLength);
                }
            }
        }

    }
}
