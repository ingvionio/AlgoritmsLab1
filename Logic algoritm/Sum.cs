using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class Sum : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            long sumValue = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sumValue += Convert.ToInt64(array.GetValue(i));
            }
        }
    }
}
