using System;
using System.IO;

namespace Tree_21_3_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree(); //инициализируем дерево
                                    //на основе данных файла создаем дерево
            int n;
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                string line = fileIn.ReadToEnd();
                string[] data = line.Split(' ');
                foreach (string item in data)
                {
                    tree.Add(int.Parse(item));
                }
            }

            tree.Inorder(); //используя прямой обход выводим на экран узлы дерева
            Console.WriteLine();
            Console.Write("N: ");
            n = int.Parse(Console.ReadLine());
            tree.Balance_preorder(ref n);
        }
    }
}
