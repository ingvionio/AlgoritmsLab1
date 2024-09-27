using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class QuickPow : Algoritm
    {
        int num = 2;
        public override void DoAlgoritm(Array powers)
        {
            for(int i = 0; i < powers.Length; i++)
            {
                QuickPower(num, Convert.ToInt32(powers.GetValue(i)));
            }
        }
        public static void QuickPower(int num, int power)
        {
            long result;

            if(power % 2 == 1)
            {
                result = num;
            }
            else
            {
                result = 1;
            }
            while(power != 0)
            {
                power /= 2;
                num *= num;

                if(power % 2 == 1)
                {
                    result *= num;
                }
            }
            Console.WriteLine(result);
        }
    }
}
