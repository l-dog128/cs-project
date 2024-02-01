using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    // Array to store the items in the heap
    T[] items;
    int currentItemCount;
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }
    // Add an item to the heap
    public void Add(T item)
    {
        // Set the heap index of the item
        item.HeapIndex = currentItemCount;
        // Add the item to the end of the array
        items[currentItemCount] = item;
        // Restore the heap property by sorting the item up
        SortUp(item);
        // Increase the item count
        currentItemCount++;
    }

    // Remove the first item from the heap (which is the root of the heap)
    public T RemoveFirst()
    {
        // Store the first item
        T firstItem = items[0];
        // Decrease the item count
        currentItemCount--;
        // Replace the first item with the last item in the array
        items[0] = items[currentItemCount];
        // Set the heap index of the new first item to 0
        items[0].HeapIndex = 0;
        // Restore the heap property by sorting the new first item down
        SortDown(items[0]);
        // Return the removed first item
        return firstItem;
    }

    // Update the position of an item in the heap
    public void UpdateItem(T item)
    {
        // Restore the heap property by sorting the item up
        SortUp(item);
    }
    // Property to get the current number of items in the heap
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }
    // Check if an item is in the heap
    public bool Contains(T item)
    {
        // Compare the item to the item at its heap index in the array
        return Equals(items[item.HeapIndex], item);
    }
    // Restore the heap property by sorting the item down
    void SortDown(T item)
    {
        // Loop until the heap property is satisfied
        while (true)
        {
            // Calculate the indices of the item's children
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            //
            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }


        }
    }
    // Restore the heap property by sorting the item up
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            //Compares the items and swaps them 
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
        }
    }

    void Swap(T itemA, T itemB)
    {
        // Swap the items in the array
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        // Swap the heap indices of the items
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}
public interface IHeapItem<T> : IComparable<T>
{
    // Property to get and set the heap index of the item
    int HeapIndex
    {
        get;
        set;
    }
}
