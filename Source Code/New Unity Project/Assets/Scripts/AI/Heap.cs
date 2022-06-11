using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] nodes;
    int currentCount;

    //create the heap
    public Heap(int maxSize)
    {
        nodes = new T[maxSize];
    }

    // add new node to heap
    public void Add(T node)
    {
        node.HeapIndex = currentCount;
        nodes[currentCount] = node;
        SortUp(node);
        currentCount++;
    }

    //remove the top node from the heap and determine a new top node
    public T RemoveFirst()
    {
        T firstNode = nodes[0];
        currentCount--;
        nodes[0] = nodes[currentCount];
        nodes[0].HeapIndex = 0;
        SortDown(nodes[0]);
        return firstNode;
    }

    //check if heap contains a spesified node
    public bool Contains(T node)
    {
        return Equals(nodes[node.HeapIndex], node);
    }

    //get the number of nodes in the heap
    public int HeapCount
    {
        get
        {
            return currentCount;
        }
    }

    //update node position in the heap once h cost has been changed
    public void UpdateNode(T node)
    {
        SortUp(node);
    }

   // sort the order of the specified node by comparing with parent
    void SortUp(T node)
    {
        int parentIndex = (node.HeapIndex - 1) / 2;
        while(true)
        {
            T parentNode = nodes[parentIndex];
            if (node.CompareTo(parentNode) > 0)
            {
                Swap(node, parentNode);
            }
            else
            {
                break;
            }
            parentIndex = (node.HeapIndex - 1) / 2;
        }
    }

    // sort the order of the specified node by comparing with children
    void SortDown (T node)
    {
        while (true)
        {
            int childIndexLeft = node.HeapIndex * 2 + 1;
            int childIndexRight = node.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentCount)
                {
                    swapIndex = childIndexLeft;
                    if(nodes[childIndexLeft].CompareTo(nodes[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
                if(node.CompareTo(nodes[swapIndex]) < 0)
                {
                    Swap(node, nodes[swapIndex]);
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
   
    //swap two nodes in a heap
    void Swap (T nodeA, T nodeB)
    {
        nodes[nodeA.HeapIndex] = nodeB;
        nodes[nodeB.HeapIndex] = nodeA;

        int nodeIndex = nodeA.HeapIndex;
        nodeA.HeapIndex = nodeB.HeapIndex;
        nodeB.HeapIndex = nodeIndex;
    }
}

// get and set heap index
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}