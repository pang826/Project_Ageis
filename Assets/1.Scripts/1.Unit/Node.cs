using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
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

    public int HeapIndex { get; set; }   // 힙에서 위치를 추적하기 위한 인덱스
    
    // 우선순위 비교 메서드
    // A* 에서는 FCost가 작을수록 우선이기 때문에 비교해서 기존값이 작을 경우 1 반환
    public int CompareTo(Node other)
    {
        if(other == null) return 1;
        if(FCost < other.FCost) return 1;
        if(FCost > other.FCost) return -1;
        if(HCost < other.HCost) return 1;
        if(HCost > other.HCost) return -1;
        return 0;
    }
}
