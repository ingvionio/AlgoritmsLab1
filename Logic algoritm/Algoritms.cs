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


    public class BubbleSort : Algoritm
    {
       
        public override void DoAlgoritm(Array array)
        {
            for (int i = array.Length; i >= 0; i--)
            {
                for(int j = 1; j < i; j++)
                {
                    var element1 = (IComparable)array.GetValue(j);
                    var element0 = array.GetValue(j-1);

                    if (element1.CompareTo(element0) < 0)
                    {
                        var value = array.GetValue(j-1);
                        array.SetValue(array.GetValue(j), j - 1);
                        array.SetValue(value, j);
                    }
                }
            } 
        }

    }

    public class Const : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            int factorial = 1;

            for (int i =0; i < 100000000; i++)
            {
                factorial = factorial * i;
            }
        }
    }

    public class MultiplyElements: Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            double multiplyValue = 1;
            var a = array.GetType();

            for (int i = 0; i < array.Length; i++)
            {
                multiplyValue = Convert.ToDouble(array.GetValue(i)) * multiplyValue;
            }
        }
    }

    public class TimSort : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            
        }
    }

}
