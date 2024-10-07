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
            int sumValue = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sumValue += Convert.ToInt32(array.GetValue(i));
            }
        }
    }
}
