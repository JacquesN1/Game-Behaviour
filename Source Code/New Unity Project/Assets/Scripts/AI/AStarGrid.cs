using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    Node[,] nodeGrid;
    private Vector2 gridSize;
    private Vector2 startingPoint;
    private float nodeWidth = 10;
    bool gridShowing = false;

    public GameObject redNode;
    public GameObject greenNode;
    public GameObject whiteNode;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        // get the corosponding world position of grid position [0,0]
        gridSize = new Vector2(250, 180);
        startingPoint = new Vector2(-70f, -85);
        //determine how many nodes can fit in grid
        gridSize = new Vector2(gridSize.x / nodeWidth, gridSize.y / nodeWidth);
        CreateGrid();
    }

    void CreateGrid()
    {
        nodeGrid = new Node[(int)gridSize.x, (int)gridSize.y];
        Vector2 currentGridPosition = startingPoint;

        //for each node in grid
        for (int x = 0; x < (int)gridSize.x; x++)
        {
            for (int y = 0; y < (int)gridSize.y; y++)
            {
                // get the corosponding world position of the current node
                currentGridPosition = new Vector2(startingPoint.x + (x * nodeWidth), startingPoint.y + (y * nodeWidth));
                // add node to grid
                nodeGrid[x, y] = new Node(currentGridPosition, CheckCollision(currentGridPosition), new Vector2(x, y));
            }
        }
    }

    //return refernce to grid
    public Node [,] GetGrid()
    {
        return nodeGrid;
    }

    //return sizee of grid
    public int MaxSize
    {
        get
        {
           return (int)gridSize.x * (int)gridSize.y;
        }
    }

    bool CheckCollision(Vector2 node)
    {
        bool colliding = false;

        //for each Physics object in the game
        foreach (PhysicsObject Obj in ObjectList.Objects)
        {
            // if the object is non-moving
            if (Obj.rBody.mass == 0)
            {
                float colliderLeft = Obj.transform.position.x - (Obj.GetComponent<SpriteRenderer>().bounds.size.x / 2);
                float colliderRight = Obj.transform.position.x + (Obj.GetComponent<SpriteRenderer>().bounds.size.x / 2);
                float nodeLeft = node.x - (nodeWidth / 2);
                float nodeRight = node.x + (nodeWidth / 2);

                // check if the left and right x values overap
                if  ((colliderLeft >= nodeLeft && colliderLeft <= nodeRight) || (colliderRight >= nodeLeft && colliderRight <= nodeRight) 
                    || (colliderLeft <= nodeLeft && colliderRight >= nodeRight) || (colliderLeft >= nodeLeft && colliderRight <= nodeRight))
                {
                    //if they overlap, check if the current node collides with the object using AABB
                    Vector2 colliderMin = new Vector2 (Obj.transform.position.x - (Obj.GetComponent<SpriteRenderer>().bounds.size.x / 2), Obj.transform.position.y - (Obj.GetComponent<SpriteRenderer>().bounds.size.y / 2));
                    Vector2 colliderMax = new Vector2(Obj.transform.position.x + (Obj.GetComponent<SpriteRenderer>().bounds.size.x / 2), Obj.transform.position.y + (Obj.GetComponent<SpriteRenderer>().bounds.size.y / 2));
                    Vector2 nodeMin = new Vector2(node.x - (nodeWidth / 2), node.y - (nodeWidth / 2));
                    Vector2 nodeMax = new Vector2(node.x + (nodeWidth / 2), node.y + (nodeWidth / 2));

                    if (((colliderMin.x <= nodeMax.x && colliderMax.x >= nodeMin.x)
                        || (colliderMin.x >= nodeMax.x && colliderMax.x <= nodeMin.x))
                        &&
                        ((colliderMin.y <= nodeMax.y && colliderMax.y >= nodeMin.y)
                        || (colliderMin.y >= nodeMax.y && colliderMax.y <= nodeMin.y)))
                    {
                        colliding = true;
                        return colliding;
                    }
                }
            }
        }

        return colliding;
    }

    //returns a list of all nodes acsessable from a given node
    public List<Node> GetRechableNodes (Node node)
    {
        List<Node> reachableNodes = new List<Node>();
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x != 0 && y != 0)
                {
                    Vector2 checkVector = new Vector2(node.gridPosition.x + x, node.gridPosition.y + y);
                    if (checkVector.x >= 0 && checkVector.x < gridSize.x && checkVector.y >= 0 && checkVector.y < gridSize.y)
                    {
                        reachableNodes.Add(nodeGrid[(int)checkVector.x, (int)checkVector.y]);
                    }
                }
            }
        }
        return reachableNodes;
    }

    public Node GetObjectNode(PhysicsObject Obj)
    {
        // get the node a specified physics object is on
        Node ObjectNode = null;
        Vector2 objectPosition = Obj.rBody.GetPosition();
        Vector2 distance = objectPosition - startingPoint;

        distance = new Vector2((float)System.Math.Round(distance.x / nodeWidth), (float)System.Math.Round (distance.y / nodeWidth));
        ObjectNode = nodeGrid[(int)distance.x, (int)distance.y];

         // if node is blocked, find reachable node to swap to
        if (ObjectNode.isBlocked == true)
        {
            foreach (Node reachableNode in GetRechableNodes(ObjectNode))
            {
                if (reachableNode.isBlocked == false)
                {
                    ObjectNode = reachableNode;
                }
            }
        }
        return ObjectNode;
    }
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Toggle Grid "))
        {
            if (gridShowing == false)
            {
                foreach (Node n in nodeGrid)
                {
                    if (n.isBlocked == true)
                    {
                        Object.Instantiate(redNode, n.nodePosition, transform.rotation);
                    }
                    else if (n.path == true)//spawn green node to represent path nodes
                    {
                        Object.Instantiate(greenNode, n.nodePosition, transform.rotation);
                    }
                    else //Spawn white nodes to represent all other nodes
                    {
                        Object.Instantiate(whiteNode, n.nodePosition, transform.rotation);
                    }

                }
                gridShowing = true;
            }
            else
            {
                GameObject[] nodeSprites;
                nodeSprites = GameObject.FindGameObjectsWithTag("Node");
               
                //Delete all node sprites
                foreach (GameObject node in nodeSprites)
                {
                    Destroy(node);
                }
                gridShowing = false;
            }
        }
    }
}
