using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class MultiplyElements : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            int multiplyValue = 1;

            for (int i = 0; i < array.Length; i++)
            {
                multiplyValue *= Convert.ToInt32(array.GetValue(i));
            }
        }
    }
}
