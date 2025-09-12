using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 WorldPos;
    public int GridX;
    public int GridY;
    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;
    public Node Parent;
    public Node(bool walkable ,Vector3 worldPos, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPos = worldPos;
        GridX = gridX;
        GridY = gridY;
    }
}
