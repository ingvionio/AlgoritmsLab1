using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class NaiveAssessment : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            double sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                double power = i - 1;
                sum += Convert.ToDouble(array.GetValue(i)) * Math.Pow(1.5, power);
            }
        }
    }
}
