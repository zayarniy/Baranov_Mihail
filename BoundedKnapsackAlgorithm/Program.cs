using System;
using System.Collections.Generic;

namespace BoundedKnapsackAlgorithm
{

    class Item
    {

        public string Description;
        public int Weight;
        public int Value;
        public int Quantity;

        public Item(string description, int weight, int value, int quantity)
        {
            Description = description;
            Weight = weight;
            Value = value;
            Quantity = quantity;
        }

    }

    class ItemCollection
    {

        public Dictionary<string, int> Contents = new Dictionary<string, int>();
        public int TotalValue;
        public int TotalWeight;

        public void AddItem(Item item, int quantity)
        {
            if (Contents.ContainsKey(item.Description)) Contents[item.Description] += quantity;
            else Contents[item.Description] = quantity;
            TotalValue += quantity * item.Value;
            TotalWeight += quantity * item.Weight;
        }

        public ItemCollection Copy()
        {
            var ic = new ItemCollection();
            ic.Contents = new Dictionary<string, int>(this.Contents);
            ic.TotalValue = this.TotalValue;
            ic.TotalWeight = this.TotalWeight;
            return ic;
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var items = new List<Item>(){
  new Item("Apple", 39, 40, 4),
  new Item("Banana", 27, 60, 4),
  new Item("Beer", 52, 10, 12),
  new Item("Book", 30, 10, 2),
  new Item("Camera", 32, 30, 1),
  new Item("Cheese", 23, 30, 4),
  new Item("Chocolate Bar", 15, 60, 10),
  new Item("Compass", 13, 35, 1),
  new Item("Jeans", 48, 10, 1),
  new Item("Map", 9, 150, 1),
  new Item("Notebook", 22, 80, 1),
  new Item("Sandwich", 50, 160, 4),
  new Item("Ski Jacket", 43, 75, 1),
  new Item("Ski Pants", 42, 70, 1),
  new Item("Socks", 4, 50, 2),
  new Item("Sunglasses", 7, 20, 1),
  new Item("Suntan Lotion", 11, 70, 1),
  new Item("T-Shirt", 24, 15, 1),
  new Item("Tin", 68, 45, 1),
  new Item("Towel", 18, 12, 1),
  new Item("Umbrella", 73, 40, 1),
  new Item("Water", 153, 200, 1)
};

            int capacity = 1000;

            int[] m = new int[W + 1];

            for (int i = 0; i < n; i++)
                for (int j = W; j >= 0; j--)
                    m[j] = j < w[i] ? m[j] : Math.Max(m[j], m[j - w[i]] + v[i]);

        }
    }
}
