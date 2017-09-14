using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    RIGHT,
    LEFT,
    UP,
    DOWN,
    NONE
}

public class NodeGroup : MonoBehaviour
{
    public static NodeGroup S;
    public GameObject nodePrefab;
    LevelLayout level = new LevelLayout();
    public List<Node> nodelist = new List<Node>();
    Vector3 offset = new Vector3(-2, 3.5f, 0);
    Stack<Node> nodestack = new Stack<Node>();
    public char[,] levelArray;

    

    private void Awake ()
    {
        S = this;
        levelArray = level.levelArray;
        print(level.levelArray.GetLength(0) + "   " + level.levelArray.GetLength(1));
        //CreateUnlinkedNodelist();
        CreateLinkedNodelist();
        ShowNodes();
	}
	
	
	void Update ()
    {
		
	}

    void LinkNodes()
    {

    }

    // Create a List of Nodes where all of the Nodes are linked together based on these rules
    void CreateLinkedNodelist()
    {
        Node startNode = FindFirstNode();
        //print(startNode.position + " " + startNode.row + " " + startNode.col);
        nodestack.Push(startNode);
        
        while(nodestack.Count > 0)
        {
            
            Node n = nodestack.Pop();
            AddNode(n);
            Node leftNode = GetPathNode(direction.LEFT, n);
            Node rightNode = GetPathNode(direction.RIGHT, n);
            Node upNode = GetPathNode(direction.UP, n);
            Node downNode = GetPathNode(direction.DOWN, n);

            n.AddNeighbor(leftNode, direction.LEFT);
            n.AddNeighbor(rightNode, direction.RIGHT);
            n.AddNeighbor(upNode, direction.UP);
            n.AddNeighbor(downNode, direction.DOWN);
            //print("LEFT="+leftNode + "  RIGHT=" + rightNode + "  UP=" + upNode + "  DOWN=" + downNode);
            //print("Add LEFT node to stack");
            AddNodeToStack(leftNode);
            //print("Add RIGHT node to stack");
            AddNodeToStack(rightNode);
            //print("Add UP node to stack");
            AddNodeToStack(upNode);
            //print("Add DOWN node to stack");
            AddNodeToStack(downNode);
            //print("Added new nodes to stack: " + nodestack.Count);
            //print("\n\n");
        }
        
    }

    // Looks in the levelArray and finds the first Node which is the first instance of '+'
    Node FindFirstNode()
    {
        for(int row=0; row<level.rows; row++)
        {
            for(int col=0; col<level.cols; col++)
            {
                if(levelArray[row, col] == '+')
                {
                    return new Node(row, col, offset);
                }
            }
        }
        return null; // No nodes were found, this should never happen
    }

    // Return either a Node or null.  Follow a path in a specific direction and return the node at the end of the path.
    Node GetPathNode(direction d, Node n)
    {
        Node temp = FollowPath(d, n);
        return GetNodeFromNode(temp);
    }

    // Add a Node to the nodelist if it already does not exist in the list
    void AddNode(Node n)
    {
        bool inList = NodeInList(n);
        if(!inList)
        {
            nodelist.Add(n);
        }
    }

    // Add node n to the stack if it isn't null and if it already is not in the nodelist
    void AddNodeToStack(Node n)
    {
        if(n != null)
        {
            //print(n.position + "  "+n.row + "  "+n.col);
            if(!NodeInList(n))
                nodestack.Push(n);  
        }
    }

    void GetNode()
    {

    }

    // Checks if Node n is in the nodelist.  Returns true if it is in the list
    bool NodeInList(Node n)
    {
        for(int i=0; i<nodelist.Count; i++)
        {
            if(n.position.x == nodelist[i].position.x &&
               n.position.y == nodelist[i].position.y)
            {
                return true;
            }
        }
        return false;
    }

    // Look for Node n in the nodelist.  If it is there, return that node.  If not then return Node n
    Node GetNodeFromNode(Node n)
    {
        if(n != null)
        {
            for(int i=0; i<nodelist.Count; i++)
            {
                if(n.position.x == nodelist[i].position.x && 
                   n.position.y == nodelist[i].position.y)
                {
                    return nodelist[i];
                }           
            }
        }
        return n;
    }

    Node FollowPath(direction d, Node n)
    {
        if (d == direction.LEFT && n.col - 1 >= 0)
        {
            return PathToFollow(d, n.row, n.col - 1, '-');
        }
        else if (d == direction.RIGHT && n.col + 1 < level.cols)
        {
            return PathToFollow(d, n.row, n.col + 1, '-');
        }

        else if (d == direction.UP && n.row-1 >= 0)
        {
            return PathToFollow(d, n.row-1, n.col, '|');
        }
        else if (d == direction.DOWN && n.row+1 < level.rows)
        {
            return PathToFollow(d, n.row+1, n.col, '|');
        }
        return null;
    }

    // Follow a path UP, DOWN, LEFT, or RIGHT along '-' and '|' symbols until you reach a '+' symbol
    Node PathToFollow(direction d, int row, int col, char symbol)
    {
        if(levelArray[row, col] == symbol || levelArray[row, col] == '+')
        {
            while(levelArray[row, col] != '+')
            {
                if(d == direction.LEFT) { col -= 1; }
                else if(d == direction.RIGHT) { col += 1; }
                else if(d == direction.UP) { row -= 1; }
                else if(d == direction.DOWN) { row += 1; }              
            }
            return new Node(row, col, offset);
        }
        else
        {
            return null;
        }
    }





    // This just loops through the levelArray and creates nodes without linking them.  
    void CreateUnlinkedNodelist()
    {
        for (int row = 0; row < level.rows; row++)
        {
            for (int col = 0; col < level.cols; col++)
            {
                char val = levelArray[row, col];
                if (val == '+')
                {                  
                    nodelist.Add(new Node(row, col, offset));
                }
            }
        }

        
    }



    void ShowNodes()
    {
        //This is just for visual purposes to show where the nodes are
        for (int i = 0; i < nodelist.Count; i++)
        {
            GameObject temp = Instantiate(nodePrefab) as GameObject;
            temp.transform.position = nodelist[i].position;
        }
    }


}
