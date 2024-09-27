using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class HornerMethod : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            double result = Convert.ToDouble(array.GetValue(0));

            for(int i = 1; i < array.Length; i++)
            {
                result = result * 1.5 + Convert.ToDouble(array.GetValue(i));
            }
        }
    }
}
