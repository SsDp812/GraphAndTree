using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_21_3_10
{
    public class Tree
    {
        private class Node
        {
            public int inf;	//информационное поле
            public int counter;
            public Node left;	//ссылка на левое поддерево
            public Node rigth;  //ссылка на правое поддерево
            public int min;
            public int max;
            //конструктор вложенного класса, создает узел дерева
            public Node(int nodeInf)
            {
                inf = nodeInf;
                counter = 1;
                left = null;
                rigth = null;
            }

            //добавляет узел в дерево так, чтобы дерево оставалось деревом бинарного поиска
            public static void Add(ref Node r, int nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                    r.min = int.MinValue;
                    r.max = int.MaxValue;
                }
                else
                {
                    r.counter++;

                    if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
                    {
                        Add(ref r.left, nodeInf);
                    }
                    else
                    {
                        Add(ref r.rigth, nodeInf);
                    }
                }
            }



            public static void Inorder(Node r) //симметричный обход дерева
            {
                if (r != null)
                {
                    Inorder(r.left);
                    Console.Write("{0} ", r.inf);
                    Inorder(r.rigth);
                }
            }

            public int Counter
            {
                get
                {
                    return (this != null) ? this.counter : 0;
                }
            }

            //}

            /*проверить, можно ли добавить не более n узлов в дерево так, чтобы дерево осталось
            деревом бинарного поиска и стало идеально сбалансированным (указать допустимые
            значения добавляемых узлов).

             Идеально сбалансированным называется дерево, у которого для каждой вершины выполняется 
            требование: число вершин в левом и правом поддеревьях различается не более чем на 1.
             модуль разности количеств узлов в любых двух его поддеревьях не превышает единицы.
            */
            // обход в которои проверяем можно ли сбалансировать дерево
            //если флаг ложн вызываем балансировку с добавлением
            //если балансировали больше раз чем n уже ничего не сделаешь

            public static void Balance_preorder(ref Node r, ref bool flag, ref int count, ref Queue<Node> s, ref int n)
            {
                if (r != null)
                {
                    int rc = (r.rigth != null) ? r.rigth.counter : 0;
                    int lc = (r.left != null) ? r.left.counter : 0;
                    if (Math.Abs(lc - rc) > 1)
                    {
                        flag = false;
                        count++;
                        s.Enqueue(r);
                    }
                    Balance_preorder(ref r.left, ref flag, ref count, ref s, ref n);
                    Balance_preorder(ref r.rigth, ref flag, ref count, ref s, ref n);
                }
            }

            public static void Func(ref Node r, ref bool flag, ref int count, ref Queue<Node> s, ref int n, ref Node x, ref int otvet, ref Stack<int> stack)
            {

                if (x != null)
                {

                    int rc = (x.rigth != null) ? x.rigth.counter : 0;
                    int lc = (x.left != null) ? x.left.counter : 0;
                    while (x.left != null && x.rigth != null && flag == false)
                    {
                        if (rc >= lc)
                            Func(ref r, ref flag, ref count, ref s, ref n, ref x.left, ref otvet, ref stack);
                        else
                            Func(ref r, ref flag, ref count, ref s, ref n, ref x.rigth, ref otvet, ref stack);
                    }
                    if (x.rigth == null && x.left == null && flag == false)
                    {
                        Add(ref x.left, x.inf - 1);
                        otvet = x.inf - 1;
                        stack.Push(x.inf - 1);
                        x.counter++;
                        NewCounter(r, x, ref flag);
                        n--;

                    }
                    else
                    {
                        if (x.rigth == null && flag == false)
                        {
                            Add(ref x.rigth, x.inf);
                            stack.Push(x.inf);
                            x.counter++;
                            otvet = x.inf;
                            NewCounter(r, x, ref flag);
                            n--;

                        }
                        else
                        {
                            if (x.left == null && flag == false)
                            {
                                Add(ref x.left, x.inf - 1);
                                otvet = x.inf - 1;
                                stack.Push(x.inf - 1);
                                x.counter++;
                                NewCounter(r, x, ref flag);
                                n--;

                            }
                        }
                    }
                }
            }
            public static void NewCounter(Node r, Node key, ref bool flag)
            {
                if (r != null) // если текущий узел не пустой
                {
                    if (key != r && flag == false)
                    {
                        r.counter++; // в любом случае увеличиваем длину пути от корня до текущего узла
                        NewCounter(r.left, key, ref flag); //обходим левое поддерево
                        if (flag == false)
                            NewCounter(r.rigth, key, ref flag); //обходим правое поддерево
                        if (flag == false)
                            r.counter--;
                    }
                    else
                    {
                        flag = true;
                    }
                }

            }

            public static void Min_Max_value(Node r, ref int key, ref int min_otv, ref int max_otv) //симметричный обход дерева
            {
                if (r != null)
                {
                    if (r.left != null)
                    {
                        r.left.min = r.min;
                        r.left.max = r.inf - 1;
                    }

                    Min_Max_value(r.left, ref key, ref min_otv, ref max_otv);
                    if (r.inf == key)
                    {
                        min_otv = r.min;
                        max_otv = r.max;
                    }

                    if (r.rigth != null)
                    {

                        r.rigth.min = r.inf;
                        r.rigth.max = r.max;
                    }
                    Min_Max_value(r.rigth, ref key, ref min_otv, ref max_otv);

                }

            }
            private static void Del(Node t, ref Node tr)
            {
                if (tr.rigth != null)
                {
                    Del(t, ref tr.rigth);
                }
                else
                {
                    t.inf = tr.inf;
                    tr = tr.left;
                }
            }
            public static void Delete(ref Node t, object key)
            {
                if (t == null)
                {
                    throw new Exception("Данное значение в дереве отсутствует");
                }
                else
                {
                    if (((IComparable)(t.inf)).CompareTo(key) > 0)
                    {
                        Delete(ref t.left, key);
                    }
                    else
                    {
                        if (((IComparable)(t.inf)).CompareTo(key) < 0)
                        {
                            Delete(ref t.rigth, key);
                        }
                        else
                        {
                            if (t.left == null)
                            {
                                t = t.rigth;
                            }
                            else
                            {

                                if (t.rigth == null)
                                {
                                    t = t.left;
                                }
                                else
                                {
                                    Del(t, ref t.left);
                                }
                            }
                        }
                    }
                }
            }
        }
        //конец вложенного класса



        Node tree;      //ссылка на корень дерева

        //свойство позволяет получить доступ к значению информационного поля корня дерева 
        public int Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }

        public int Counter
        {
            get { return tree.counter; }

        }


        public Tree()	//открытый конструктор
        {
            tree = null;
        }

        private Tree(Node r) //закрытый конструктор
        {
            tree = r;
        }

        public void Add(int nodeInf)	//добавление узла в дерево
        {
            Node.Add(ref tree, nodeInf);
        }

        public void Balance_preorder(ref int n)
        {
            bool flag = true;
            int count = 0;
            Queue<Node> s = new Queue<Node>();
            Stack<int> stack = new Stack<int>();
            Node.Balance_preorder(ref tree, ref flag, ref count, ref s, ref n);
            Console.WriteLine(flag);
            Console.WriteLine(count);

            while (flag == false && count <= n)
            {
                int otvet = 0;//значение которое добавляем
                int min_otv = 0;//диапозон
                int max_otv = 0;
                Node x = s.Dequeue();
                Node.Func(ref tree, ref flag, ref count, ref s, ref n, ref x, ref otvet, ref stack);//функция в которой добавляем значение
                count = 0;
                s.Clear();
                Node.Min_Max_value(tree, ref otvet, ref min_otv, ref max_otv);//пересчитываем диапозон
                Console.WriteLine("Добавим значение = {0}. Диапозон: [{1}, {2}]", otvet, min_otv, max_otv);
                Node.Inorder(tree);
                Console.WriteLine();
                Console.WriteLine();
                Node.Balance_preorder(ref tree, ref flag, ref count, ref s, ref n);//проверяем на баланс
            }
            if (flag == false && count > n)
            {
                Console.WriteLine("Дерево нельзя сбалансировать");
                Console.WriteLine();
                while (stack.Count != 0)
                {
                    Node.Delete(ref tree, stack.Pop());
                }
                Console.WriteLine("Первоначальный вид дерева:");
                Node.Inorder(tree);
            }
        }
        public void Inorder()
        {
            Node.Inorder(tree);
        }
    }
}

