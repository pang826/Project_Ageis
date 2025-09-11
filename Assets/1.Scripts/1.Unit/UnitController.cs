using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitData _unitData;
    [SerializeField] private UnitView _unitView;
    [SerializeField] private NavMeshAgent _agent;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _unitData.IsSelectable)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 target = hit.point;

                _agent.SetDestination(target);
            }
        }
    }
    public void SetSelected(bool selected)
    {
        _unitData.IsSelectable = selected;
        _unitView.SetSelected(selected);
    }

    public void TakeDamage(int amount)
    {
        _unitData.Hp -= amount;
    }
}
