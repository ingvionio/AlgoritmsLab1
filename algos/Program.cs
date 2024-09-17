using Algoritms.Logic;

namespace Algoritms.ConsloeApp
{
    class Program
    {
        static void Main()
        {
            BubbleSort algoritm = new BubbleSort();
            TimeSpan[] time = TimeCounter.TimeCount(2, 3005, algoritm);

            foreach (TimeSpan t in time)
            {
                Console.WriteLine(t.TotalSeconds.ToString());
            }
            
        }
    }
}