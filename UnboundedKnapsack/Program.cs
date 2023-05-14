// C# program to find maximum achievable
// value with a knapsack of weight W and
// multiple instances allowed.
using System;

class UboundedKnapsack
{

    private static int max(int i, int j)
    {
        return (i > j) ? i : j;
    }

    // Returns the maximum value
    // with knapsack of W capacity
    private static int unboundedKnapsack(int W, int n,
                                  int[] val, int[] wt)
    {

        // dp[i] is going to store maximum value
        // with knapsack capacity i.
        int[] dp = new int[W + 1];

        // Fill dp[] using above recursive formula
        for (int i = 0; i <= W; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (wt[j] <= i)
                {
                    dp[i] = Math.Max(dp[i], dp[i -
                                        wt[j]] + val[j]);
                }
            }
        }
        return dp[W];
    }

    //new Item(3, 31), new Item(4, 16), new Item(2, 9), new Item(6, 30) 
    // Driver program
    public static void Main()
    {
        int W = 6;
        int[] val = { 31, 16, 9,30 };
        int[] wt = { 3, 4, 2,6 };
        int n = val.Length;
        Console.WriteLine(unboundedKnapsack(W, n, val, wt));
    }
}
