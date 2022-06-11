using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public float fCost;
    public Vector2 gridPosition;
    public Vector2 nodePosition;
    public bool isBlocked;
    public Node parent;
    public int heapIndex;
    public bool path = false;

    public Node (Vector2 _position, bool _isBlocked, Vector2 _gridPosition)
    {
        //position in world
        nodePosition = _position;
        // weather the node can be traveld through
        isBlocked = _isBlocked;
        // position in grid
        gridPosition = _gridPosition;
    }

    //the index of the node in the heap
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    //compare the f cost of this node to another
    public int CompareTo (Node node)
    {
        int compare = fCost.CompareTo(node.fCost);
        return -compare;
    }
}
