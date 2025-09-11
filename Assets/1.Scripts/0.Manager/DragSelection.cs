using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelection : MonoBehaviour
{
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

        foreach (var unit in FindObjectsOfType<UnitSelectable>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            if (selectRect.Contains(new Vector2(screenPos.x, Screen.height - screenPos.y)))
            {
                unit.SetSelected(true);
            }
            else
            {
                unit.SetSelected(false);
            }
        }
    }
}
