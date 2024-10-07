using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class StandartQuickPower : PowersAlg
    {
        int num = 2;

        public override void DoAlgoritm(Array array)
        {
            DoAlgAndStepCount(array);
        }

        public override int DoAlgAndStepCount(Array powers)
        {
            int totalSteps = 0;

            for (int i = 0; i < powers.Length; i++)
            {
                totalSteps += QuickPower(num, Convert.ToInt32(powers.GetValue(i)));
            }
            return totalSteps;
        }

        public static int QuickPower(int num, int power)
        {
            long result = 1;
            int steps = 0;

            while(power != 0)
            {
                if(power % 2 == 0)
                {
                    num *= num;
                    power /= 2;
                    steps += 2;
                }
                else
                {
                    result *= num;
                    power--;
                    steps += 2;
                }
            }
            return steps;
        }
    }
}
