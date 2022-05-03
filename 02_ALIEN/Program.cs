using System;
namespace _02_ALIEN
{
    internal class Program
    {
        static void Main()
        {
            // bad code
            int testCases = int.Parse(Console.ReadLine());
            for (int t = 0; t < testCases; t++)
            {
                int[] nm = Array.ConvertAll(
                    Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                    int.Parse);
                int stations = nm[0];
                int amount = nm[1];
                int[] array = Array.ConvertAll(
                    Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                    int.Parse);
                var elo = MaxSum(array, stations, amount);
                Console.WriteLine($"{elo.Item1} {elo.Item2}");
            }
        }
        static (int, int) MaxSum(int[] arr, int n, int m)
        {
            int sum = 0;
            int minpeople = 0;
            int minSteps = 0;
            int steps = 0;
            int p = 0;
            for (int i = 0; i < n; i++)
            {
                if (sum + arr[i] <= m)
                {
                    sum += arr[i];
                    steps++;
                }
                else
                {
                    sum += arr[i];
                    steps++;
                    while (sum > m)
                    {
                        sum -= arr[p];
                        steps--;
                        p++;
                    }
                }
                if (minSteps < steps)
                {
                    minSteps = steps;
                    minpeople = sum;
                }
                else if (minSteps == steps)
                {
                    if (i == 0)
                        minpeople = sum;
                    if (minpeople > sum)
                        minpeople = sum;
                }
            }
            return (minpeople, minSteps);
        }
    }
}
