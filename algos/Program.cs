using Algoritms.Logic;

namespace Algoritms.ConsloeApp
{
    class Program
    {
        static void Main()
        {
            TimSort algoritm = new TimSort();
            BubbleSort bubbleSort = new BubbleSort();
            List<TimeSpan> time = TimeCounter.TimeCount(100000,100000, bubbleSort, 1);

            foreach (TimeSpan t in time)
            {
                Console.WriteLine(t.TotalSeconds.ToString());
            }
            
        }
    }
}