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
            GornerMethod gornerMethod = new GornerMethod();
            //List<TimeSpan> time = TimeCounter.TimeCount(10000,1000000, gornerMethod, 100);

            //foreach (TimeSpan t in time)
            //{
            //    Console.WriteLine(t.TotalSeconds.ToString());
            //}

            int[] test = Generator.Generate(100000);
            algoritm.DoAlgoritm(test);


            foreach (var el in test)
            {
                Console.WriteLine(el.ToString());
            }
            
        }
    }
}