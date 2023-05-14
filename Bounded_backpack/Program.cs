using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Bounded_backpack
{
    //Ограниченный рюкзак (англ.Bounded Knapsack Problem) — обобщение ограниченного рюкзака,
    //в котором любой предмет может быть выбран ограниченное количество раз.
    class Item
    {
        //поля
        public int Weight, Price, Amount;

        //конструктор - создает объект класса
        public Item( int Price, int Weight, int Amount)
        {
            this.Weight = Weight;
            this.Price = Price;
            this.Amount = Amount;
        }

        public Item()
        {
            this.Weight = 0;
            this.Price = 0;
            this.Amount = 0;
        }

        public override string ToString() => "W:" + Weight + " P:" + Price;
    }

    class Program
    {
        //Печать списка элементов
        static public void PrintList(List<Item> list)
        {
            int sumPrice=0,sumAmount=0,sumWeight=0;
            Console.WriteLine("P W A");
            //перебираем все элементы в списке
            foreach (Item item in list)
            {
                //выводим на экран
                Console.WriteLine(item.Price + " " + item.Weight+" "+item.Amount);
            }
            Console.WriteLine("Стоимость:"+sumPrice+" Вес:"+sumWeight+" Количество:"+sumAmount);
            Console.WriteLine("Стоимость:" + list.Sum(el=>el.Price*el.Amount) + " Вес:" + list.Sum(el => el.Weight*el.Amount) + " Количество:" + list.Sum(el => el.Amount));

        }
        //Печать таблицы
        static void PrintTable(int[,] m, List<Item> p)
        {
            Console.Write(String.Format("{0,9}", " "));
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
            int[,] T = new int[p.Count+1, W + 1];
            List<Item>[] list=new List<Item>[p.Count+1];
            for (int i = 1; i <= p.Count; i++)//Перебор элементов

                for (int w = 0; w <= W; w++)
                    if (w < p[i-1].Weight)//Если не помещается текущий, то
                        T[i, w] = T[i - 1, w];//берем тот вес, который был на предыдущем шаге
                    else
                    {
                        {
                            //Перебираем сколько предметов мы можем положить
                            for (int j = 0; j <= p[i-1].Amount; j++)
                            {
                                if (p[i - 1].Weight * j <= w)
                                {
                                    //выбираем, что положить
                                    int t1 = T[i - 1, w];//Стоимость того, что уже лежит
                                    int t2 = T[i - 1, w - p[i - 1].Weight] + p[i - 1].Price * j;//стоимость того, что мы можем положить
                                    T[i, w] = System.Math.Max(t1, t2);
                                }
                            }
                        }
                    }
            return T;//возвращаем результат

        }

        static Dictionary<Item,int> ItemsSelectBound(int W, List<Item> p, int[,] T, int startItem)
        {
            //Словарь ключ/значение, где ключ - это наш элемент, а значение - это количество элеметов в словаре
            Dictionary<Item, int> selectedItems = new Dictionary<Item, int>();
            for(int j=0;j<p.Count;j++)
            {
                selectedItems.Add(p[j], 0);
            }
            int w = W;
            int i = startItem+1;
            int k = w;
            while (i > 0)
            {
                if (T[i, k] == T[i - 1, k]) 
                    i--;
                else
                {

                    if (w > 0)
                    {
                        {
                            if (selectedItems[p[i-1]] < p[i - 1].Amount)
                            {
                                selectedItems[p[i - 1]]++;
                                w -= p[i - 1].Weight;
                                k-= p[i - 1].Weight;
                            }
                            else
                                i--;
                        }
                    }
                }
            }
            return selectedItems;
        }

        static void PrintDic(Dictionary<Item, int> dic) 
        {
            foreach (var pair in dic)
                if (pair.Value > 0)
                    Console.WriteLine(pair.Key.Price + "$ с весом " + pair.Key.Weight + " кг x " + pair.Value + " шт.");
            Console.WriteLine();
            Console.WriteLine("Стоимость:" +dic.Sum(el =>el.Value>0?el.Key.Price*el.Value:0) + " Вес:"+dic.Sum(el => el.Value > 0 ? el.Key.Weight * el.Value : 0) +  " Количество:" + dic.Sum(el => el.Value));
        }


        static List<Item> RandomItems(int n, int maxWeight, int maxValue,int maxCount)
        {
            Random random = new Random();

            List<Item> list=new List<Item>();
            for (int i = 0; i < n; i++)
                list.Add(new Item(random.Next(1,maxValue+1), random.Next(1,maxWeight+1), random.Next(1,maxCount + 1)));
            return list;

        }
        //точка входа в программу
        static void Main()
        {

            //List<Item> temp = new List<Item>() { new Item(2, 1, 7), new Item(2, 2, 5)};
            
            //List<Item> temp = new List<Item>() { new Item(1, 1, 5), new Item(2, 2, 8) };
            //List<Item> temp = new List<Item>() { new Item(1, 1, 3), new Item(2, 2, 10) };
            List<Item> temp = RandomItems(2, 10, 10, 10);
            temp.Sort(delegate (Item el1, Item el2)
            {
                return el1.Weight - el2.Weight;
            }
            );
            Console.WriteLine("Список элементов");
            PrintList(temp);
            
            int W;
            //Console.Write("Введите вместимость рюкзака (кг):");W =Convert.ToInt32(Console.ReadLine());
            W = 20;
            Console.WriteLine("Limit weight:"+W);
            int[,] T = Solve(temp, W);

            Console.WriteLine("Выбранные вещи:");
            Dictionary<Item,int> knapsack=ItemsSelectBound(W, temp, T, temp.Count - 1);
            PrintDic(knapsack);
           
            PrintTable(T, temp);

            Console.ReadKey();
        }
    }

}
