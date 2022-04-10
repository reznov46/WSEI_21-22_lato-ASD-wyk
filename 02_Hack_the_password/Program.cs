using System;
using System.Text;

namespace _02_Hack_the_password
{
    internal class Program
    {
        internal class Node
        {
            internal Node prev = null;
            internal Node next = null;
            internal char cha;

            internal Node(char ch)
            {
                cha = ch;
            }
        }

        static void Main()
        {
            int amount = int.Parse(Console.ReadLine());
            while (amount > 0)
            {
                string line = Console.ReadLine();
                Node current = null;
                Node head = null;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<')
                    {
                        if (current == null)
                            continue;
                        current = current.prev;
                    }
                    if (line[i] == '>')
                    {
                        if (current == null)
                            current = head;
                        else if (current.next != null)
                            current = current.next;
                    }
                    if (line[i] == '-')
                    {
                        if (current == null)
                            continue;
                        if (current.prev == null)
                        {
                            head = current.next;
                            current = null;
                            if (head != null)
                                head.prev = null;
                        }
                        else
                        {
                            current.prev.next = current.next;
                            if (current.next != null)
                                current.next.prev = current.prev;
                            current = current.prev;
                        }
                    }
                    else
                    {
                        if (current == null)
                        {
                            current = new Node(line[i]);
                            current.next = head;
                            if (head != null)
                                head.prev = current;
                            head = current;
                        }
                        else
                        {
                            Node newNode = new Node(line[i]);
                            newNode.next = current.next;
                            newNode.prev = current;
                            if (current.next != null)
                                current.next.prev = newNode;
                            current.next = newNode;
                            current = newNode;
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                while (head != null)
                {
                    sb.Append(head.cha);
                    head = head.next;
                }
                Console.WriteLine(sb);
                amount--;
            }
        }
    }
}
