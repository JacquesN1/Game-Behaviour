using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    AStarGrid grid;
    float hDistance;

    private void Start()
    {
        grid = GetComponent<AStarGrid>();
    }

    //generates path to player usin A*
    public void generatePath (Node startNode, Node targetNode)
    {
        hDistance = GetDistance(startNode, targetNode);
        Heap<Node> openNodes = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedNodes = new HashSet<Node>();
        openNodes.Add(startNode);
    
        // get open node with lowest h cost
        while (openNodes.HeapCount > 0)
        {
            Node currentNode = openNodes.RemoveFirst();
            closedNodes.Add(currentNode);
            hDistance = GetDistance(currentNode, targetNode);

            //return if player reached
            if (currentNode == targetNode)
            {
                return;
            }

            //check nodes reachable from current node
            foreach (Node reachableNode in grid.GetRechableNodes(currentNode))
            {
                if (reachableNode.isBlocked == false)
                {
                    if (!closedNodes.Contains(reachableNode))
                    {
                        if (openNodes.Contains(reachableNode))
                        {
                            //if already open, check for better h cost
                            if (reachableNode.fCost > GetDistance(currentNode, reachableNode))
                            {
                                reachableNode.fCost = GetDistance(currentNode, reachableNode) + hDistance;
                                reachableNode.parent = currentNode;
                                openNodes.UpdateNode(reachableNode);
                            }
                        }
                        else
                        {
                            //if not on open, calculate h cost and add to open.
                            reachableNode.fCost = GetDistance(currentNode, reachableNode) + hDistance;
                            reachableNode.parent = currentNode;
                            openNodes.Add(reachableNode);
                        }
                    }
                }
            }
        }

    }

    // returns distance between any two nodes
    float GetDistance (Node nodeA, Node nodeB)
    {
        float distance = (int)System.Math.Sqrt(System.Math.Pow(nodeB.nodePosition.x - nodeA.nodePosition.x, 2) + System.Math.Pow(nodeB.nodePosition.y - nodeA.nodePosition.y, 2));
         return distance;
    }
}
