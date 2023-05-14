using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    static Dictionary<int,int> ToDic(List<int> items)
    {
        Dictionary<int,int> d = new Dictionary<int,int>();
        foreach (int item in items)
        {
            if (d.ContainsKey(item)) d[item]++;
            else d.Add(item, 1);
        }
        return d;
    }
    static void Main(string[] args)
    {
        int[] weights = { 1, 3, 4, 5 };
        int[] values = { 30, 4, 5, 6 };
        int[] limits = { 1, 2, 1, 3 };
        //int[] weights = { 1,2 };
        //int[] values = { 4,7};
        //int[] limits = { 2,2};

        int maxWeight = 7;

        List<int> itemsInKnapsack = GetItemsInKnapsack(weights, values, limits, maxWeight);
        Console.WriteLine("Limited weight:"+maxWeight);
        int sumWeights=0, sumValues=0, sumLimits=0;
        Console.WriteLine("ALL ITEMS:");
            
        for (int i = 0;i<weights.Length;i++)
        {
            Console.WriteLine("Item " + i + ": weight = " + weights[i] + ", value = " + values[i] + "(limit:" + limits[i] + ")");
            sumLimits += limits[i];
            sumWeights += weights[i]*limits[i];
            sumValues += values[i]*limits[i];
        }
        Console.WriteLine("Max weight:"+sumWeights);
        Console.WriteLine("Max value:"+sumValues);
        Console.WriteLine("All items:"+sumLimits);
        Console.WriteLine("SELECTED ITEMS:");
        Dictionary<int, int> d = ToDic(itemsInKnapsack);

        foreach (var item in d)
        {
            Console.WriteLine("Item " + item.Key + ": weight = " + weights[item.Key] + ", value = " + values[item.Key] +" in pack:"+item.Value+ " value:"+values[item.Key]*item.Value+" weight:" +weights[item.Key]*item.Value+" (limit:" + limits[item.Key] + ")");
        }

        //foreach (int item in itemsInKnapsack)
        //{
        //    Console.WriteLine("Item " + item + ": weight = " + weights[item] + ", value = " + values[item]+"(limit:"+limits[item]+")");
        //}
        Console.WriteLine("Total weight: " + itemsInKnapsack.Sum(item => weights[item]));
        Console.WriteLine("Total value: " + itemsInKnapsack.Sum(item => values[item]));
        Console.WriteLine("Total items: " + itemsInKnapsack.Count);
    }

    static List<int> GetItemsInKnapsack(int[] weights, int[] values, int[] limits, int maxWeight)
    {
        int n = weights.Length;
        int[,,] dp = new int[n + 1, maxWeight + 1, limits.Max() + 1];
        bool[,,] selected = new bool[n + 1, maxWeight + 1, limits.Max() + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= maxWeight; w++)
            {
                for (int k = 0; k <= limits.Max(); k++)
                {
                    if (i == 0 || w == 0 || k==0)
                    {
                        dp[i, w, k] = 0;
                    }
                    else 
                    if (weights[i - 1] <= w && k <= limits[i - 1] && values[i - 1] + dp[i, w - weights[i - 1], k - 1] >dp[i - 1, w, k])
                    {
                        dp[i, w, k] = values[i - 1] + dp[i, w - weights[i - 1], k - 1];
                        selected[i, w, k] = true;
                    }
                    else
                    {
                        dp[i, w, k] = dp[i - 1, w, k];
                        selected[i-1, w, k] = true;
                    }
                }
            }
        }

        List<int> itemsInKnapsack = new List<int>();
        int weight = maxWeight;
        int[] counts = new int[n];

        for (int i = n; i > 0 && weight > 0; i--)
        //for (int i = 0; i <n  && weight > 0; i++)
        {
            for (int k = 0; k <= limits.Max(); k++)
            {
                if (weight - weights[i-1] >=0 && selected[i, weight, k] && counts[i-1]<limits[i-1])
                {
                    itemsInKnapsack.Add(i - 1);
                    weight -= weights[i - 1];
                    counts[i - 1]++;
                }
            }
        }

        itemsInKnapsack.Reverse();
        return itemsInKnapsack;
    }
}