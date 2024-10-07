using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Algoritms.Logic
{
    public abstract class Algoritm
    {
        public abstract void DoAlgoritm(Array array);
    }

    public abstract class PowersAlg : Algoritm
    {
        public abstract int DoAlgAndStepCount(Array array);
    }
}


