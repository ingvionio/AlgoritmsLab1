using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class Const : Algoritm
    {
        public override void DoAlgoritm(Array array)
        {
            int factorial = 1;

            for (int i = 0; i < 100000; i++)
            {
                factorial = factorial * i;
            }
        }
    }
}
