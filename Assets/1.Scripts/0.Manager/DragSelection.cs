using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelection : MonoBehaviour
{
    public List<UnitSelectable> SelectedUnits = new List<UnitSelectable>();

    private Vector3 _startPos;
    private Vector3 _endPos;
    private bool _isDragging;

    void Update()   
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
            _isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            SelectUnits();
        }
        if (Input.GetMouseButton(0))
        {
            _endPos = Input.mousePosition;
        }
    }

    void OnGUI()
    {
        if (_isDragging)
        {
            var rect = GetScreenRect(_startPos, _endPos);
            DrawScreenRect(rect, new Color(0.8f, 0.8f, 1f, 0.25f));
        }
    }

    // 화면 Rect 계산
    Rect GetScreenRect(Vector3 p1, Vector3 p2)
    {
        p1.y = Screen.height - p1.y;
        p2.y = Screen.height - p2.y;
        var topLeft = Vector3.Min(p1, p2);
        var bottomRight = Vector3.Max(p1, p2);
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    // 직사각형 채우기
    void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;
    }

    // 유닛 선택
    void SelectUnits()
    {
        Rect selectRect = GetScreenRect(_startPos, _endPos);

        // 기존 선택 해제
        foreach (var unit in SelectedUnits)
            unit.SetSelected(false);

        SelectedUnits.Clear();
        // 드래그 박스를 월드 좌표로 변환
        Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(_startPos.x, _startPos.y, Camera.main.transform.position.y));
        Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(_endPos.x, _endPos.y, Camera.main.transform.position.y));

        // 중심의 위치 구하기
        Vector3 center = (p1 + p2) * 0.5f;

        // 절반 크기의 상자 = (2, 2, 2) 크기의 상자라면 (1, 1, 1)로!
        Vector3 halfBox = new Vector3(Mathf.Abs(p1.x - p2.x) * 0.5f, 1, Mathf.Abs(p1.z - p2.z) * 0.5f);

        // OverlapBox로 충돌체 검사 (Unit 레이어만)
        // 첫번째 매개변수 = 중심위치
        // 두번째 매개변수 = 상자 크기의 절반(Half of the size of the box in each dimension)
        Collider[] hits = Physics.OverlapBox(center, halfBox, Quaternion.identity, LayerMask.GetMask("Unit"));
        
        foreach (var hit in hits)
        {
            UnitSelectable unit = hit.GetComponent<UnitSelectable>();
            if (unit != null)
            {
                SelectedUnits.Add(unit);
                unit.SetSelected(true);
            }
        }
    }
}
