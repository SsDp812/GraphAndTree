using System;

namespace Graph_22_3_20
{
    internal class Program
    {
        static void Main()
        {
            Graph g = new Graph("input.txt");
            Console.WriteLine("-----Наш граф-----");
            g.Show();
            g.Floyd();
            Console.WriteLine("N:");
            int N = int.Parse(Console.ReadLine());
            int[] answer = g.CheckForDistances(N);
            if (answer[0] == 1)
            {
                g.GraphEdit(answer[1], answer[2]);
                g.Show();
                g.Floyd();
            }

        }
    }
}
