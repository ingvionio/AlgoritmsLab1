using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class RecPow : PowersAlg
    {
        int num = 2;

        public override void DoAlgoritm(Array array)
        {
            DoAlgAndStepCount(array);
        }

        public override int DoAlgAndStepCount(Array powers)
        {
            int totalSteps = 0;

            for (int i = 1; i < powers.Length; i++)
            {
                (long result, int steps) a = RecPower(num, Convert.ToInt32(powers.GetValue(i)));
                totalSteps += a.steps;
            }
            return totalSteps;
        }
        public static (long result, int steps) RecPower(int num, int power, int steps = 0)
        {
            long result = 1;
            steps++;

            if(power != 0)
            {
                var (recursiveResult, recursiveStep) = RecPower(num, power / 2, steps);
                steps = recursiveStep;
                result = recursiveResult;

                if(power % 2 == 1)
                {
                    result = result * result * num;
                    steps++;
                }
                else
                {
                    result = result * result;
                    steps++;
                }
            }

            return (result, steps);
        }
    }
}
