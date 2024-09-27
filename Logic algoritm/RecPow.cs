using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class RecPow : Algoritm
    {
        int num = 2;
        public override void DoAlgoritm(Array powers)
        {
            for (int i = 1; i < powers.Length; i++)
            {
                RecPower(num, Convert.ToInt32(powers.GetValue(i)));
            }
        }
        public static long RecPower(int num, int power)
        {
            long result = 1;

            if(power != 0)
            {
                result = RecPower(num, power / 2);

                if(power % 2 == 1)
                {
                    result = result * result * num;
                }
                else
                {
                    result = result * result;
                }
            }

            return result;
        }
    }
}
