using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class PowAlgorithm : Algoritm
    {
        int num = 2;

        public override void DoAlgoritm(Array powers)
        {
            for(int i = 0; i < powers.Length; i++)
            {
                long result = 1;

                for (int j = 0; j < Convert.ToInt32(powers.GetValue(i)); j++)
                {
                    result *= num;
                }
            }
        }
    }
}
