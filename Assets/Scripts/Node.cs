using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public Vector3 position = new Vector3();
    public Dictionary<direction, Node> neighbors = new Dictionary<direction, Node>();
    public int row;
    public int col;

   

    public Node(float px, float py, int r, int c, Vector3 offset=new Vector3())
    {
        row = r;
        col = c;
        position = new Vector3(px, -py, 0) + offset;       
    }

    public void AddNeighbor(Node node, direction D)
    {
        if(node != null)
            neighbors.Add(D, node);
    }
    
}
