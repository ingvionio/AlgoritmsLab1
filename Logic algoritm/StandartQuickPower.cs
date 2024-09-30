using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class StandartQuickPower : Algoritm
    {
        int num = 2;
        public override void DoAlgoritm(Array powers)
        {
            for (int i = 0; i < powers.Length; i++)
            {
                QuickPower(num, Convert.ToInt32(powers.GetValue(i)));
            }
        }

        public static void QuickPower(int num, int power)
        {
            long result = 1;
            
            while(power != 0)
            {
                if(power % 2 == 0)
                {
                    num *= num;
                    power /= 2;
                }
                else
                {
                    result *= num;
                    power--;
                }
            }
        }
    }
}
