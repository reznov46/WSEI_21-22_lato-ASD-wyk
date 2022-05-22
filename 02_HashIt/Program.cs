using System;

namespace _02_HashIt
{
    internal class Program
    {
        const int mod = 101;
        const int numb = 19;
        static int count = 0;
        static string[] hash = new string[mod];
        static void Main(string[] args)
        {
            int numberOfTestCases = int.Parse(Console.ReadLine());
            while (numberOfTestCases != 0)
            {
                int testCases = int.Parse(Console.ReadLine());
                SolveIt(testCases);
                numberOfTestCases--;
            }


        }

        public static void SolveIt(int testCases)
        {
            ResetIt();
            for (int i = 0; i < testCases; i++)
            {
                string str = Console.ReadLine();
                string[] strArr = str.Split(':');
                if (strArr[0].Equals("ADD") && !ExistIt(strArr[1]))
                {
                    InsertIt(strArr[1]);
                }
                else if (strArr[0].Equals("DEL") && ExistIt(strArr[1]))
                {
                    DeleteIt(strArr[1]);
                }
            }
            Console.WriteLine(count);
            for (int i = 0; i < mod; i++)
            {
                if (!hash[i].Equals(""))
                {
                    Console.WriteLine($"{i}:{hash[i]}");
                }

            }
        }

        public static int HashIt(string key)
        {
            int ans = 0;
            for (int i = 0; i < key.Length; i++)
            {
                ans += (int)key[i] * (i + 1);
            }
            return (ans * numb) % mod;
        }

        public static void ResetIt()
        {
            for (int i = 0; i < hash.Length; i++)
            {
                hash[i] = "";
            }
            count = 0;
        }

        public static int NextPos(string key, int i)
        {
            return (HashIt(key) + i * i + 23 * i) % mod;
        }

        public static void DeleteIt(string key)
        {
            for (int i = 0; i <= numb; i++)
            {
                if (hash[NextPos(key, i)].Equals(key))
                {
                    hash[NextPos(key, i)] = "";
                    count--;
                }
            }
        }

        public static bool InsertIt(string key)
        {
            for (int i = 0; i <= numb; i++)
            {
                if (hash[NextPos(key, i)].Equals(""))
                {
                    hash[NextPos(key, i)] = key;
                    count++;
                    return true;
                }
            }
            return false;
        }

        public static bool ExistIt(string key)
        {
            for (int i = 0; i < mod; i++)
            {
                if (hash[i].Equals(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
