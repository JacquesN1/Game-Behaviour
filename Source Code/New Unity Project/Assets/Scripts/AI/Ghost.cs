using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    AStarGrid grid;
    Pathfinding path;
    PhysicsObject ghost;

    List<Node> nodePath;
    public PhysicsObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<AStarGrid>();
        path = GetComponent<Pathfinding>();
        ghost = GetComponent<PhysicsObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //generate the path to the player and get list of nodes
        path.generatePath(grid.GetObjectNode(ghost), grid.GetObjectNode(player));
        RetraceNodes(grid.GetObjectNode(player), grid.GetObjectNode(ghost));

        //if the next node has a valid position and the list of nodes is not empty, go to next node
        if (nodePath[0].nodePosition != null && nodePath.Count != 0)
        {
            ghost.rBody.SetPosition(nodePath[0].nodePosition);
        }
    }

    void RetraceNodes(Node start, Node end)
    {
        //set all nodes to not part of path
        foreach (Node n in grid.GetGrid())
        {
            n.path = false;
        }
        List<Node> path = new List<Node>();

        //if node has parent, add parent to list
        if (start.parent != null)
        {
            Node currentNode = start.parent;
            while (currentNode != end && currentNode != null)
            {
                currentNode.path = true;
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
        }
        nodePath = path;
    }
}
