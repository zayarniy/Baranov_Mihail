using System;
using System.Collections.Generic;


namespace MaxWeightStones
{
    //Неограниченный рюкзак (англ.Unbounded Knapsack Problem) — обобщение ограниченного рюкзака,
    //в котором любой предмет может быть выбран любое количество раз.
    //Когда каждый предмет выбирается по одному разу
    
    //предмет в рюкзаке
    class Item
    {
        //поля
        public int Weight, Price;

        //конструктор - создает объект класса
        public Item(int Weight, int Price)
        {
            this.Weight = Weight;
            this.Price = Price;
        }

        public Item()
        {
            this.Weight = 0;
            this.Price = 0;            
        }
    }


    class Program
    {
        //Печать списка элементов
        static public void PrintList(List<Item> list)
        {
            Console.WriteLine("Price   Weight");
            //перебираем все элементы в списке
            foreach (Item item in list)
            {
                //выводим на экран
                Console.WriteLine(item.Price + "     " + item.Weight);
            }

        }
        //Печать таблицы (не используется)
        static void Print(int[,] m, List<Item> p)
        {
            Console.Write("         ");
            for (int i = 0; i < m.GetLength(1); i++)
                Console.Write(String.Format($"{i,4}"));
            Console.WriteLine();
            for (int i = 1; i < m.GetLength(0); i++)
            {
                Console.Write($"({i}).{p[i-1].Price,4}:");
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write($"{m[i, j],4}");
                Console.WriteLine();
            }
        }

        //Решение задачи
        static int[,] Solve(List<Item> p, int W)
        {
            Dictionary<Item, int> backpack=new Dictionary<Item, int>();
            int[,] T = new int[p.Count+1, W + 1];
            for (int i = 1; i <= p.Count; i++)//Перебор элементов

                for (int w = 0; w <= W; w++)
                    if (w < p[i - 1].Weight)//Если не помещается текущий, то
                    {
                        T[i, w] = T[i - 1, w];//берем тот вес, который был на предыдущем шаге

                    }
                    else
                    {
                        //выбираем, что положить
                        int t1 = T[i - 1, w];//Стоимость того, что уже лежит
                        int t2 = T[i, w - p[i - 1].Weight] + p[i - 1].Price;//стоимость того, что мы можем положить                        
                        int t3 = T[i-1, w - p[i - 1].Weight] + p[i - 1].Price;
                        if (t2 > t1)
                        {
                            if (backpack.ContainsKey(p[i-1]))
                                backpack[p[i-1]]++;
                            else
                                backpack.Add(p[i-1], 1);
                        }
                        //T[i, w] = Math.Max(t3, System.Math.Max(t1, t2));
                        T[i, w] = System.Math.Max(t1, t3);
                    }

            return T;//возвращаем результат

        }

        //выбираем элементы из рюкзака
        static void ItemsSelect(int W, List<Item> p, int[,] T, int startItem)
        {
            //список предметов в рюкзаке
            List<Item> backpack;
            backpack = new List<Item>();//объект
            //Словарь ключ/значение, где ключ - это наш элемент, а значение - это количество элеметов в словаре
            //Dictionary<Item, int> selectedItems = new Dictionary<Item, int>();

            int w = W;//структура
            int sumPrice = 0;
            //int w = startCount;
            //int i =  p.Count - 1;
            int i = startItem;
            while (i > 0)
            {
                if (T[i, w] == T[i - 1, w]) i--;
                else
                {
                    w -= p[i].Weight;
                    sumPrice += p[i].Price;
                    backpack.Add(p[i]);
                    i--;
                }
            }
            //Добираем рюкзак
            if (w >= p[0].Weight)
            {
                backpack.Add(p[0]);
                w -= p[0].Weight;
                sumPrice += p[i].Price;
            }
            //Console.WriteLine("Цена  Вес  Количество");
            foreach (var pair in backpack)
            {
                Console.Write(pair.Price + "$ с весом " + pair.Weight + " кг x " + "1 шт. +");
            }
            Console.WriteLine();
            Console.WriteLine(sumPrice);
        }

        //точка входа в программу
        static void Main()
        {
            List<Item> list = new List<Item>() { new Item(3, 32), new Item(1, 11), new Item(2, 9), new Item(6, 63) };
            Console.WriteLine("Список элементов");
            PrintList(list);
            Console.Write("Введите вместимость рюкзака (кг):");
            int W = Convert.ToInt32(Console.ReadLine());
            int[,] T = Solve(list, W);
            Print(T, list);
            Console.WriteLine(T[T.GetLength(0)-1,T.GetLength(1)-1]);



            Console.ReadKey();
        }
    }

}
