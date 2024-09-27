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
            MatrixMultiplyer matrixMultiplyer = new MatrixMultiplyer();
            //List<TimeSpan> time = TimeCounter.TimeCount(1000,100000, hornerMethod, 10); 

            /*foreach (TimeSpan t in time) 
            { 
                Console.WriteLine(t.TotalSeconds.ToString()); 
            } */

            int[,] firstMatrix = Generator.GenerateMatrix(4, 10, 15);
            int[,] secondMatrix = Generator.GenerateMatrix(4, 10, 15);
            MatrixMultiplyer.DoAlgoritm(firstMatrix, secondMatrix);


            /*foreach (var el in test)
            {
                Console.WriteLine(el.ToString());
            }*/

        }
    }
}