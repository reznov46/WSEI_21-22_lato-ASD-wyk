using System;
using System.Linq;

namespace TBATTLE
{

    public struct Node
    {
        public int Previous;
        public int Number;
    }

    internal class Program
    {
        const int maxTestCases = 100005;
        static void Main(string[] args)
        {

            int place1 = 0;
            int place = 0;
            int[,] a = new int[maxTestCases, 10];
            int[] c = new int[maxTestCases];
            int[] d = new int[maxTestCases];
            Node[] nodes = new Node[10];

            int testCases = int.Parse(Console.ReadLine());
            int[] numbers = new int[testCases];
            string[] input = Console.ReadLine().Split(" ").ToArray();

            if (testCases == 1)
            {
                Console.WriteLine("1");
                Environment.Exit(0);
            }

            for (int i = 0; i < testCases; i++)
            {
                numbers[i] = int.Parse(input[i]);
            }

            for (int i = 2; i < maxTestCases; i++)
            {
                for (int j = 2; i * j < maxTestCases; i++)
                {
                    c[i * j] = 1;
                }
            }

            for (int i = 2; i < maxTestCases; i++)
            {
                if (c[i] == 0)
                    d[place1++] = i;
            }

            int s = testCases;
            while (s != 1)
            {
                for (int i = 0; i < place1; i++)
                {
                    if (s % d[i] == 0)
                    {
                        nodes[place].Previous = d[i];
                        while (s % d[i] == 0)
                        {
                            nodes[place].Number++;
                            s /= d[i];
                        }
                        place++;
                    }
                }
            }

            for (int i = 0; i < testCases; i++)
            {
                for (int j = 0; j < place; j++)
                {
                    while (numbers[i] % nodes[j].Previous == 0)
                    {
                        a[i, j]++;
                        numbers[i] /= nodes[j].Previous;
                    }
                }
            }

            int h = 0, g = 0, stretch1 = 0, stretch2 = 0, len = 999999, flag = 0, num;
            while (g < testCases)
            {
                num = 0;
                for (int i = 0; i < place; i++)
                {
                    nodes[i].Number -= a[g, i];
                }
                for (int i = 0; i < place; i++)
                {
                    if (nodes[i].Number <= 0) num++;
                }
                if (num == place)
                {
                    flag = 1;
                    while (true)
                    {
                        num = 0;
                        for (int i = 0; i < place; i++)
                        {
                            nodes[i].Number += a[h, i];
                        }
                        for (int i = 0; i < place; i++)
                        {
                            if (nodes[i].Number <= 0) num++;
                        }
                        if (num != place && g - h + 1 < len)
                        {
                            len = g - h + 1;
                            stretch1 = h;
                            stretch2 = g;
                        }
                        h++;
                        if (num != place)
                            break;
                    }
                }
                g++;
            }

            if (flag == 0)
            {
                Console.WriteLine("-1");
            }
            else
            {
                Console.WriteLine($"{stretch1} {stretch2}");
            }
        }
    }
}
