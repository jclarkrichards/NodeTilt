using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public Vector3 position = new Vector3();
    public Dictionary<direction, Node> neighbors = new Dictionary<direction, Node>();

    public Node(Vector3 p)
    {
        position = p;       
    }

    public void AddNeighbor(Node node, direction D)
    {
        neighbors.Add(D, node);
    }
    
}
