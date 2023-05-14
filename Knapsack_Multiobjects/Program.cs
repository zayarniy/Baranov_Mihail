/*
Для решения задачи о рюкзаке в языке программирования C# можно использовать динамическое программирование и алгоритмы перебора. Вот пример кода, который решает эту задачу:

*/
using System;
using System.Collections.Generic;

//Правильное решение
class Knapsack
{


    static void Main()
    {
        int[] weights = { 2, 10, 4, 5 };
        //int[] values = { 3, 4, 5, 6 };
        int[] values = { 6, 5, 40, 30 };
        int maxWeight = 100;

        List<int> itemsInKnapsack = GetItemsInKnapsack(weights, values, maxWeight);

        Console.WriteLine("Items in knapsack:");
        foreach (int item in itemsInKnapsack)
        {
            Console.Write(item+" ");
        }
    }

    static List<int> GetItemsInKnapsack(int[] weights, int[] values, int maxWeight)
    {
        int n = weights.Length;
        int[,] dp = new int[n + 1, maxWeight + 1];
        bool[,] selected = new bool[n + 1, maxWeight + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= maxWeight; w++)
            {
                if (i == 0 || w == 0)
                {
                    dp[i, w] = 0;
                }
                else if (weights[i - 1] <= w && values[i - 1] + dp[i, w - weights[i - 1]] > dp[i - 1, w])
                {
                    dp[i, w] = values[i - 1] + dp[i, w - weights[i - 1]];
                    selected[i, w] = true;
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        List<int> itemsInKnapsack = new List<int>();
        int weight = maxWeight;

        for (int i = n; i > 0 && weight > 0; i--)
        {
            while (selected[i, weight])
            {
                itemsInKnapsack.Add(i - 1);
                weight -= weights[i - 1];
            }
        }

        itemsInKnapsack.Reverse();
        return itemsInKnapsack;
    }

}

/*
В этом примере мы создаем массивы weights и values, которые содержат вес и стоимость каждого предмета. Мы также задаем максимальный вес рюкзака.

Затем мы вызываем метод GetItemsInKnapsack, который использует динамическое программирование для определения максимальной стоимости предметов, которые можно положить в рюкзак. Метод возвращает список индексов предметов, которые должны быть помещены в рюкзак.

Мы затем выводим список выбранных предметов на консоль.

Метод GetItemsInKnapsack использует двумерный массив dp для хранения максимальной стоимости предметов для каждого возможного веса рюкзака. Он также использует массив selected для отслеживания того, был ли выбран предмет для помещения в рюкзак.

Мы начинаем с заполнения первой строки и первого столбца dp нулями. Затем мы перебираем все возможные комбинации предметов и веса рюкзака, чтобы определить максимальную стоимость.

Мы затем используем массив selected, чтобы найти предметы, которые должны быть помещены в рюкзак. Мы начинаем с последнего предмета и проверяем, был ли он выбран для помещения в рюкзак. Если да, мы добавляем его индекс в список itemsInKnapsack и вычитаем его вес из общего веса рюкзака. Мы продолжаем этот процесс до тех пор, пока не достигнем первого предмета или не достигнем минимального веса рюкзака.
*/