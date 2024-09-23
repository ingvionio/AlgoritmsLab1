using Algoritms.Logic;

namespace Algoritms.ConsloeApp
{
    class Program
    {
        static void Main()
        {
            MultiplyElements algoritm = new MultiplyElements();
            List<TimeSpan> time = TimeCounter.TimeCount(2, 1000005, algoritm, 1000);

            foreach (TimeSpan t in time)
            {
                Console.WriteLine(t.TotalSeconds.ToString());
            }
            
        }
    }
}