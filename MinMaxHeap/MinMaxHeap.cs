using System;
using System.Collections.Generic;
using System.Linq;
namespace MinMaxHeap
{
    public class MinMaxHeap<T> where T : IComparable<T>
    {
        // konstruktor, tworzy pusty min-max-kopiec, O(1)
        public List<T> array;
        private int indicator;
        public MinMaxHeap()
        {
            array = new List<T>();
            indicator = 1;
        }

        // konstruktor, tworzy kopiec na podstawie dostarczonych danych, O(n log n)
        public MinMaxHeap(IEnumerable<T> values)
        {
            array = values.ToList();
            int a = array.Count / 2;
            for (int i = a; i >= 1; i--)
            {
                PushDown(array, i);
            }
            indicator = Count;
        }

        // dodaje element do kopca, O(log n)
        public void Insert(T element)
        {
            if (Count > 0)
            {
                array.Add(element);
                indicator++;
            }
            else
            {
                array.Add(element);
                PushUp(array, indicator);
                indicator++;
            }
            Console.Write("L");
        }

        public void Insert(IEnumerable<T> elements)
        {
            foreach (T t in elements)
            {
                Insert(t);
            }
        }

        private void PushUp(List<T> array, int i)
        {
            if (i != 0)
            {
                if (IsEvenLevel(i))
                {
                    if (array[i - 1].CompareTo(array[GetParentIndex(i) - 1]) < 0)
                    {
                        PushUpMin(array, i);
                    }
                    else
                    {
                        Swap(i - 1, GetParentIndex(i) - 1, array);
                        i = GetParentIndex(i);
                        PushUpMax(array, i);
                    }
                }
                else if (array[i - 1].CompareTo(array[GetParentIndex(i) - 1]) > 0)
                {
                    PushUpMax(array, i);
                }
                else
                {
                    Swap(i - 1, GetParentIndex(i) - 1, array);
                    i = GetParentIndex(i);
                    PushUpMin(array, i);
                }
            }
        }

        private void PushUpMin(List<T> array, int i)
        {
            while (HasGrandparent(i) && array[i - 1].CompareTo(array[GetParentIndex(i) - 1]) < 0)
            {
                Swap(i - 1, GetParentIndex(GetParentIndex(i)) - 1, array);
                i = GetParentIndex(GetParentIndex(i));
            }
        }
        private void PushUpMax(List<T> array, int i)
        {
            while (HasGrandparent(i) && array[i - 1].CompareTo(array[GetParentIndex(i) - 1]) > 0)
            {
                Swap(i - 1, GetParentIndex(GetParentIndex(i)) - 1, array);
                i = GetParentIndex(GetParentIndex(i));
            }
        }

        // zwraca i usuwa element minimalny z kopca, O(log n)
        //public T DeleteMin()
        //{
        //    if (Count == 0)
        //        return default(T);
        //    else if (Count == 1)
        //    {
        //        var tmp = array[0];
        //        Clear();
        //        return tmp;
        //    }
        //    else
        //    {

        //    }
        //}

        // zwraca i usuwa element maksymalny z kopca, O(log n)
        public T DeleteMax()
        {
            var tmp = array.First();
            array.RemoveAt(0);
            return tmp;
        }

        // zwraca element minimalny kopca (nie usuwa go), O(1)
        public T Min { get => array.First(); }

        // zwraca element maksymalny kopca (nie usuwa go), O(1)
        public T Max { get => array.Last(); }

        // czyści kopiec, usuwa wszystkie jego elementy, O(1)
        public void Clear()
        {
            array.Clear();
        }

        // zwraca aktualną liczbę elementów kopca, O(1)
        public int Count { get => array.Count; }

        public bool IsEmpty { get => array.Count > 0 ? false : true; }

        // kopiuje elementy kopca do nowej tablicy, O(n)
        public T[] ToArray()
        {
            return array.ToArray();
        }

        // indexes

        private int GetLeftChildIndex(int i) => 2 * i;
        private int GetRightChildIndex(int i) => (2 * i) + 1;
        private int GetParentIndex(int i) => i / 2;

        private bool HasGrandparent(int i) => GetParentIndex(i) > 1;


        private void PushDown(List<T> array, int i)
        {
            if (IsEvenLevel(i))
            {
                PushDownMin(array, i);
            }
            else
            {
                PushDownMax(array, i);
            }
        }
        private void PushDownMin(List<T> h, int i)
        {
            while (GetLeftChildIndex(i) < indicator)
            {
                int indexOfSmallest = GetIndexOfSmallestChildOrGrandChild(h, i);
                if (h[indexOfSmallest - 1].CompareTo(h[i - 1]) < 0)
                {
                    if (GetParentIndex(GetParentIndex(indexOfSmallest)) == i)
                    {
                        if (h[indexOfSmallest - 1].CompareTo(h[i - 1]) < 0)
                        {
                            Swap(indexOfSmallest - 1, i - 1, h);
                            if (h[indexOfSmallest - 1].CompareTo(h[GetParentIndex(indexOfSmallest) - 1]) > 0)
                            {
                                Swap(indexOfSmallest - 1, GetParentIndex(indexOfSmallest) - 1, h);
                            }
                        }
                    }
                    else if (h[indexOfSmallest - 1].CompareTo(h[i - 1]) < 0)
                    {
                        Swap(indexOfSmallest - 1, i - 1, h);
                    }
                }
                else
                {
                    break;
                }
                i = indexOfSmallest;
            }
        }

        private void Swap(int i, int j, List<T> array)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private void PushDownMax(List<T> array, int i)
        {
            while (GetLeftChildIndex(i) < indicator)
            {
                int indexOfGreatest = GetIndexOfGreatestChildOrGrandChild(array, i);
                if (array[indexOfGreatest - 1].CompareTo(array[i - 1]) > 0)
                {
                    if (GetParentIndex(GetParentIndex(indexOfGreatest)) == i)
                    {
                        if (array[indexOfGreatest - 1].CompareTo(array[i - 1]) > 0)
                        {
                            Swap(indexOfGreatest - 1, i - 1, array);
                            if (array[indexOfGreatest - 1].CompareTo(array[GetParentIndex(indexOfGreatest) - 1]) < 0)
                            {
                                Swap(indexOfGreatest - 1, GetParentIndex(indexOfGreatest) - 1, array);
                            }
                        }
                    }
                    else if (array[indexOfGreatest - 1].CompareTo(array[i - 1]) > 0)
                    {
                        Swap(indexOfGreatest - 1, i - 1, array);
                    }
                }
                else
                {
                    break;
                }
                i = indexOfGreatest;
            }
        }

        private int GetIndexOfGreatestChildOrGrandChild(List<T> array, int i)
        {
            int maxIndex = GetLeftChildIndex(i);    //we know left child exists
            T maxValue = array[maxIndex - 1];

            if (GetRightChildIndex(i) < indicator)
            {
                if (array[GetRightChildIndex(i) - 1].CompareTo(maxValue) > 0)
                {
                    maxValue = array[GetRightChildIndex(i) - 1];
                    maxIndex = GetRightChildIndex(i);
                }
            }
            else
            {
                return maxIndex;
            }

            if (GetLeftChildIndex(GetLeftChildIndex(i)) < indicator)
            {
                if (array[GetLeftChildIndex(GetLeftChildIndex(i)) - 1].CompareTo(maxValue) > 0)
                {
                    maxValue = array[GetLeftChildIndex(GetLeftChildIndex(i)) - 1];
                    maxIndex = GetLeftChildIndex(GetLeftChildIndex(i));
                }
            }
            else
            {
                return maxIndex; //if no leftmost grandchild
            }

            if (GetRightChildIndex(GetLeftChildIndex(i)) < indicator)
            {
                if (array[GetRightChildIndex(GetLeftChildIndex(i)) - 1].CompareTo(maxValue) > 0)
                {
                    maxValue = array[GetRightChildIndex(GetLeftChildIndex(i)) - 1];
                    maxIndex = GetRightChildIndex(GetLeftChildIndex(i));
                }
            }
            else
            {
                return maxIndex; //if no left-right grandchild
            }

            if (GetLeftChildIndex(GetRightChildIndex(i)) < indicator)
            {
                if (array[GetLeftChildIndex(GetRightChildIndex(i)) - 1].CompareTo(maxValue) > 0)
                {
                    maxValue = array[GetLeftChildIndex(GetRightChildIndex(i)) - 1];
                    maxIndex = GetLeftChildIndex(GetRightChildIndex(i));
                }
            }
            else
            {
                return maxIndex;
            }

            if (GetRightChildIndex(GetRightChildIndex(i)) < indicator)
            {
                if (array[GetRightChildIndex(GetRightChildIndex(i)) - 1].CompareTo(maxValue) > 0)
                {
                    maxValue = array[GetRightChildIndex(GetRightChildIndex(i)) - 1];
                    maxIndex = GetRightChildIndex(GetRightChildIndex(i));
                }
            }
            else
            {
                return maxIndex;
            }

            return maxIndex;
        }

        private int GetIndexOfSmallestChildOrGrandChild(List<T> array, int i)
        {
            int minIndex = GetLeftChildIndex(i);
            T minValue = array[minIndex - 1];

            if (GetRightChildIndex(i) < indicator)
            {
                if (array[GetRightChildIndex(i) - 1].CompareTo(minValue) < 0)
                {
                    minValue = array[GetRightChildIndex(i)];
                    minIndex = GetRightChildIndex(i);
                }
            }
            else
            {
                return minIndex;
            }

            if (GetLeftChildIndex(GetLeftChildIndex(i)) < indicator)
            {
                if (array[GetLeftChildIndex(GetLeftChildIndex(i)) - 1].CompareTo(minValue) < 0)
                {
                    minValue = array[GetLeftChildIndex(GetLeftChildIndex(i)) - 1];
                    minIndex = GetLeftChildIndex(GetLeftChildIndex(i));
                }
            }
            else
            {
                return minIndex;    //if no leftmost grandchild
            }

            if (GetRightChildIndex(GetLeftChildIndex(i)) < indicator)
            {
                if (array[GetRightChildIndex(GetLeftChildIndex(i)) - 1].CompareTo(minValue) < 0)
                {
                    minValue = array[GetRightChildIndex(GetLeftChildIndex(i)) - 1];
                    minIndex = GetRightChildIndex(GetLeftChildIndex(i));
                }
            }
            else
            {
                return minIndex; //if no left-right grandchild
            }

            if (GetLeftChildIndex(GetRightChildIndex(i)) < indicator)
            {
                if (array[GetLeftChildIndex(GetRightChildIndex(i)) - 1].CompareTo(minValue) < 0)
                {
                    minValue = array[GetLeftChildIndex(GetRightChildIndex(i)) - 1];
                    minIndex = GetLeftChildIndex(GetRightChildIndex(i));
                }
            }
            else
            {
                return minIndex; //if no right-left grandchild
            }

            if (GetRightChildIndex(GetRightChildIndex(i)) < indicator)
            {
                if (array[GetRightChildIndex(GetRightChildIndex(i)) - 1].CompareTo(minValue) < 0)
                {
                    minValue = array[GetRightChildIndex(GetRightChildIndex(i)) - 1];
                    minIndex = GetRightChildIndex(GetRightChildIndex(i));
                }
            }
            else
            {
                return minIndex;
            }

            return minIndex;
        }

        private bool IsEvenLevel(int i) => i % 2 == 0 ? true : false;

    }
}
