using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms.Logic
{
    public class MatrixMultiplyer
    {
        public static void DoAlgoritm(int[,] firstMatrix, int[,] secondMatrix)
        {
            int[,] result = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(0)];
            
            for(int i = 0; i < firstMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < secondMatrix.GetLength(0); j++)
                {
                    for(int k = 0; k < secondMatrix.GetLength(0); k++)
                    {
                        result[i, j] = firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }
        }
        //Для теста
        public static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write("{0} ", a[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
