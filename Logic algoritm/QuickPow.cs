using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class QuickPow : PowersAlg
    {
        int num = 2;

        public override void DoAlgoritm(Array array)
        {
            DoAlgAndStepCount(array);
        }

        public override int DoAlgAndStepCount(Array powers)
        {
            int totalSteps = 0;
            for(int i = 0; i < powers.Length; i++)
            {
                totalSteps += QuickPower(num, Convert.ToInt32(powers.GetValue(i)));
            }
            return totalSteps;
        }
        public static int QuickPower(int num, int power)
        {
            long result;
            int steps = 0;

            if(power % 2 == 1)
            {
                result = num;
                steps++;
            }
            else
            {
                result = 1;
                steps++;
            }
            while(power != 0)
            {
                power /= 2;
                num *= num;
                steps += 2;

                if(power % 2 == 1)
                {
                    result *= num;
                    steps++;
                }
            }
            return steps;
        }
    }
}
