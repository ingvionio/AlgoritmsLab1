using Algoritms.Logic;

namespace Algoritms.ConsloeApp
{
    class Program
    {
        static void Main()
        {
            TimSort algoritm = new TimSort();
            BubbleSort bubbleSort = new BubbleSort();
            QuickSortAlgoritm quickSortAlgoritm = new QuickSortAlgoritm();
            NaiveAssessment naiveAssessment = new NaiveAssessment();
            HornerMethod hornerMethod = new HornerMethod();
            PowAlgorithm powAlgorithm = new PowAlgorithm();
            RecPow recPow = new RecPow();
            QuickPow quickPow = new QuickPow();
            HeapSort heapSort = new HeapSort();
            //List<TimeSpan> time = TimeCounter.TimeCount(1000,100000, hornerMethod, 10); 

            /*foreach (TimeSpan t in time) 
            { 
                Console.WriteLine(t.TotalSeconds.ToString()); 
            } */

            int[] test = Generator.Generate(20);
            heapSort.DoAlgoritm(test);


            foreach (var el in test)
            {
                Console.WriteLine(el.ToString());
            }

        }
    }
}