using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Grid _grid;
    private int _straightCost = 10; // 직선이동 1을 10을 곱해 스케일링한 값
    private int _diagonalCost = 14; // 대각이동 1.414...를 10을 곱해 스케일링한 14.14...근사값
    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    // 노드 간 거리를 측정할 때 사용하는 휴리스틱 메서드
    private int GetDistance(Node a, Node b)
    {
        int x = Mathf.Abs(a.GridX - b.GridX);
        int y = Mathf.Abs(a.GridY - b.GridY);
        int big = Mathf.Max(x, y);
        int small = Mathf.Min(x, y);
        // 대각선 이동을 할 경우 x와 y가 한번에 각각 1씩 줄어들기 때문에 대각 이동만큼 직선 이동이 1회 줄어듦
        // 따라서 대각선 이동한만큼 직선이동에서 차감하여 직선이동 진행
        // 이를 옥타일 휴리스틱 메서드라고 함 (= 직선이동 + 4방향 대각 이동 가능)
        return _diagonalCost * small + _straightCost * (big - small);
    }

    // 왔던 경로를 추적하는 메서드
    private List<Vector3> RetracePath(Node start, Node end)
    {
        List<Vector3> path = new List<Vector3>();
        // 최종 노드를 현재 노드로 설정
        Node cur = end;
        // 노드의 부모노드를 현재 노드로 바꿔가면서 시작노드가 될때까지 반복하여 경로 리스트에 저장
        while(cur != start)
        {
            path.Add(cur.WorldPos);
            cur = cur.Parent;
        }
        path.Add(start.WorldPos);
        // 경로 정방향으로
        path.Reverse();

        return path;
    }

