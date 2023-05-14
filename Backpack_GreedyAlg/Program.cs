using System;
using System.Collections.Generic;
using System.Linq;

namespace Backpack_GreedyAlg
{
    //На основе этого видео
    //https://youtu.be/MKAcThsmeX8

    class Item
    {

        //конструктор - создает объект класса
        public Item(int Count, int Price)
        {
            this.Count=Count;
            this.Price=Price;
        }

        //свойства класса
        public int Count { get; private set; }
        public int Price { get; private set;}

        public double UnitValue //вещественное - дробное значение
        { 
            get
            {
                return (double)Price / (double)Count;
            } 
        }

        //Переопределили поведение ToString
        public override string ToString()
        {
            return String.Format($"{this.Price,4:G}{this.Count,4}{this.UnitValue,6:F2}");
        }
    }

    //Класс - рюкзак
    class Backpack
    {



        //консктор - который принимает на вход список элементов
        public Backpack(List<Item> list)
        {
            this.list = list;
        }


        //конструктор, который создает пустой список элементов
        public Backpack() 
        { 
            this.list=new List<Item>();
        }
        
        //Список
        private List<Item> list;

        //свойство
        public List<Item> BackpackList
        {
            get
            {
                return list;
            }
        }

        public int PriceSum
        {
            get
            {
                int s = 0;
                foreach(Item item in list)
                {
                    s=s+item.Price;
                }
                return s;
            }
        }


        public void PrintList()
        {
            //перебираем все элементы в списке
            foreach(Item item in list)
            {
                //выводим на экран
                Console.WriteLine(item.ToString());
            }
            
        }


    }

    internal class Program
    {
        static int Comparer(Item item1, Item item2)
        {
            return  Math.Sign(item2.UnitValue - item1.UnitValue);
        }
        static void Main(string[] args)
        {

            //Backpack backpack=new Backpack(new List<Item>() { new Item(4,20), new Item(3,18),new Item(2,14) });
            List<Item> temp = new List<Item>() { new Item(3, 14), new Item(4, 16), new Item(2, 9), new Item(6, 30) };
            Backpack itemsList = new Backpack(temp);
            //сортировка рюкзака
            itemsList.BackpackList.Sort(Comparer);
            Console.WriteLine("Список элементов");
            itemsList.PrintList();
            Console.WriteLine($"Сумма:{itemsList.PriceSum}"); 
            
            Console.Write("Введите вместимость рюкзака (кг):");
            int W = Convert.ToInt32(Console.ReadLine());
            int w = W;
            Backpack newBackpack = new Backpack();
            foreach(Item item in itemsList.BackpackList) 
            {
                if (w>=item.Count)
                {
                    newBackpack.BackpackList.Add(item);
                    w-= item.Count;
                }
                else
                {
                    newBackpack.BackpackList.Add(new Item(w, item.Price));
                    w = 0;
                    break;
                }
            }
            Console.WriteLine("Вещи в рюкзаке");
            newBackpack.PrintList();
            Console.WriteLine($"Сумма:{newBackpack.BackpackList.Sum<Item>(item=>item.Price)}") ;

        }
    }
}
