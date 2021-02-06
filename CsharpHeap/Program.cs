using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using CsharpHeap;

namespace CsharpHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayMinHeap<BigInteger> arrayMinHeap = new ArrayMinHeap<BigInteger>();
            Random random = new Random();
            for (int k = 0; k < 1000000; k++)
            {
                arrayMinHeap.Push(random.Next(0, 1024));
            }
            int i = 0;
            while (true)
            {
                try
                {

                    Console.WriteLine(arrayMinHeap[i]);
                }
                catch
                {
                    break;
                }
                i++;
            }
        }
    }
}
