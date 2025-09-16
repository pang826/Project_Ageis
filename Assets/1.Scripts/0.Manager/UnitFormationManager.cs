using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFormationManager : MonoBehaviour
{
    [SerializeField] DragSelection _dragSelection;
    [SerializeField] private float spacing = 1.5f; // 유닛 간 간격

    private LayerMask _groundMask;

    private void Start()
    {
        _groundMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _dragSelection.SelectedUnits.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
            {
                MoveGroup(hit.point);
            }
        }
    }

    void MoveGroup(Vector3 target)
    {
        var units = _dragSelection.SelectedUnits;
        if (units.Count == 0) return;

        // 리더는 중앙 목표
        units[0].SetDestination(target);

        // 나머지 대형 배치
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(units.Count));
        for (int i = 1; i < units.Count; i++)
        {
            int row = i / rowSize;
            int col = i % rowSize;
            Vector3 offset = new Vector3(col * spacing, 0, row * spacing);
            Vector3 unitTarget = target + offset;

            units[i].SetDestination(unitTarget);
        }
    }
}
