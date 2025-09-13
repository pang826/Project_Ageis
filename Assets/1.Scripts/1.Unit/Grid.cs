using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask UnWalkableMask;
    public Vector2 GridWorldSize = new Vector2(50, 50);
    public float NodeRadius = 0.5f;
    private Node[,] _nodes;
    private float _nodeDiameter;    // 노드 지름
    private int _gridSizeX;         // 그리드 X칸 개수
    private int _gridSizeY;         // 그리드 Y칸 개수
    private Vector3 _worldBottomLeft;

    private void Awake()
    {
        _nodeDiameter = NodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }

    // 격자 생성 메서드
    private void CreateGrid()
    {
        _nodes = new Node[_gridSizeX, _gridSizeY];
        _worldBottomLeft = transform.position - (Vector3.right * GridWorldSize.x / 2) - (Vector3.forward * GridWorldSize.y / 2);
        for(int i = 0; i <_nodes.GetLength(0); i++)
        {
            for(int j = 0; j < _nodes.GetLength(1); j++)
            {
                Vector3 worldPoint = _worldBottomLeft + Vector3.right * (i * _nodeDiameter + NodeRadius) + Vector3.forward * (j * _nodeDiameter + NodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, NodeRadius * 0.9f, UnWalkableMask);
                _nodes[i, j] = new Node(walkable, worldPoint, i, j);
            }
        }
    }

    // 월드 값을 받았을 때 해당 지점의 노드를 반환하는 메서드
    public Node GetNodeFromWorldPoint(Vector3 worldPoint)
    {
        float percentX = Mathf.Clamp01((worldPoint.x - _worldBottomLeft.x) / _gridSizeX);
        float percentY = Mathf.Clamp01((worldPoint.y - _worldBottomLeft.y) / _gridSizeY);
        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _nodes[x, y];
    }

    // 이웃 노드 리스트를 반환하는 메서드(두번째 매개변수는 대각선 허용/비허용)
    public List<Node> GetNeighborNode(Node node, bool allowDiagonals = true)
    {
        List<Node> neighbors = new List<Node>();
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                // 본인 위치면 스킵
                if (i == 0 && j == 0) continue;
                // 대각 비허용이면서 대각선 위치의 노드면 스킵
                if (allowDiagonals == false && Mathf.Abs(i) + Mathf.Abs(j) > 1) continue;
                // 찾는 인덱스가 격자 내에 위치한다면 반환할 이웃 노드 리스트에 추가
                int x = node.GridX + i;
                int y = node.GridY + j;
                if (x >= 0 && y >= 0 && x < _gridSizeX && y < _gridSizeY)
                    neighbors.Add(_nodes[x, y]);
            }
        }

        return neighbors;
    }
}
