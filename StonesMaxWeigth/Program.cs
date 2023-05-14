using System;
using System.Collections.Generic;
using System.Linq;

//Реализация задачи о куче из учебника Полякова за 11 класс. Часть 2. стр. 124
namespace MaxWeigthStones
{
    class Program
    {

        static void Print(int[,] m, List<int> p)
        {
            Console.Write(String.Format("{0,9}"," "));
            for (int i=0; i<m.GetLength(1);i++)
                Console.Write(String.Format($"{i,4}"));
            Console.WriteLine();
            for (int i = 0; i < m.GetLength(0); i++)
            {
                Console.Write($"({i}).{p[i],4}:");
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write($"{m[i, j],4}");
                Console.WriteLine();
            }
        }

        static List<int> InputStoneWeigth()
        {
            string stoneWeigth = "";
            int k = 0;
            List<int> p = new List<int>();
            int temp;
            do
            {
                Console.Write($"Введите {++k} вес камня(кг) (Enter - закончить):");
                stoneWeigth = Console.ReadLine();

                int.TryParse(stoneWeigth, out temp);
                if (temp != 0)
                    p.Add(temp);


            }
            while (temp != 0);
            return p;            
        }

        static int[,] Solve(List<int> p,int W)
        {
            int[,] T = new int[p.Count, W+1];
            for (int w = 0; w <= W; w++)
                T[0, w] = (p[0] > w) ? 0 : p[0];
            for (int i = 1; i < p.Count; i++)//Перебор камней

                for (int w = 0; w <= W; w++)
                    if (w < p[i])
                        T[i, w] = T[i - 1, w];
                    else
                    {
                        int t1 = T[i - 1, w];//То что уже лежит
                        int t2 = T[i - 1, w - p[i]] + p[i];//то что мы можем положить
                        T[i, w] = System.Math.Max(t1, t2);
                    }
            return T;

        }

        static void Main(string[] args)
        {
            List<int> p=new List<int>();
            //p= InputStoneWeigth();
            p.Add(2);
            p.Add(4);
            p.Add(5);
            p.Add(7);
            Console.Write("Введите вместимость рюкзака (кг):");
            int W = Convert.ToInt32(Console.ReadLine());
            int[,] T = Solve(p, W);
            Print(T,p);
            for (int i = p.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(i);
                StoneSelect(W, p, T, i);
            }
            Console.ReadKey();
        }

        static void StoneSelect(int W, List<int> p, int[,] T,int startStone)
        {
            List<int> stones = new List<int>();
            int w = W;
            //int w = startCount;
            //int i =  p.Count - 1;
            int i = startStone;
            while (i > 0)
            {
                if (T[i, w] == T[i - 1, w]) i--;
                else
                {
                    w -= p[i];
                    stones.Add(p[i]);
                }
            }
            while (w >= p[0])
            {
                stones.Add(p[0]);
                w -= p[0];
            }
            foreach (var item in stones)
                Console.Write($"{item,4}");
            Console.WriteLine();
            Console.WriteLine($"Сумма:{stones.Sum()}");

        }


    }
}
