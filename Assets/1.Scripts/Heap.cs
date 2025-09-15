using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    private T[] _items;
    private int _curItemCount;

    public Heap(int maxHeapSize)
    {
        _items = new T[maxHeapSize];
        _curItemCount = 0;
    }

    public void Add(T item)
    {
        item.HeapIndex = _curItemCount;
        _items[_curItemCount] = item;
        SortUp(item);
        _curItemCount++;
    }

    public T RemoveFirst()
    {
        if (_curItemCount == 0) return default;
        T firstItem = _items[0];
        _curItemCount--;
        _items[0] = _items[_curItemCount];
        _items[0].HeapIndex = 0;
        SortDown(_items[0]);
        return firstItem;
    }

    public void UpdateItem(T item) => SortUp(item);

    public int Count => _curItemCount;
    private void Swap(T a, T b)
    {
        _items[a.HeapIndex] = b;
        _items[b.HeapIndex] = a;
        int temp = a.HeapIndex;
        a.HeapIndex = b.HeapIndex;
        b.HeapIndex = temp;
    }

    // 자식에서 부모 방향으로 비교/스왑
    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while(true)
        {
            if (parentIndex < 0) break;
            T parent = _items[parentIndex];
            // item이 parent보다 우선이면 스왑
            if (item.CompareTo(parent) > 0)
            {
                Swap(item, parent);
                parentIndex = (item.HeapIndex - 1) / 2;
            }
            else break;
        }
    }

    // 부모에서 자식 방향으로 비교/스왑
    private void SortDown(T item)
    {
        while(true)
        {
            int leftIndex = item.HeapIndex * 2 + 1;
            int rightIndex = item.HeapIndex * 2 + 2;
            int swapIndex = -1;

            if (leftIndex < _curItemCount)
            {
                swapIndex = leftIndex;
                if (rightIndex < _curItemCount && _items[leftIndex].CompareTo(_items[rightIndex]) < 0)
                {
                    swapIndex = rightIndex;
                }

                if (_items[item.HeapIndex].CompareTo(_items[swapIndex]) < 0)
                {
                    Swap(_items[item.HeapIndex], _items[swapIndex]);
                }
                else return;
            }
            else return;
        }
    }

    public bool Contains(T item)
    {
        if (item.HeapIndex < 0 || item.HeapIndex >= _curItemCount) return false;
        return Equals(_items[item.HeapIndex], item);
    }
}
