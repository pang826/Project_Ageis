using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitData _unitData;
    [SerializeField] private UnitView _unitView;
    private PathFinder _pathFinder;

    private List<Vector3> _currentPath;
    private int _pathIndex = 0;
    private void OnEnable()
    {
        _pathFinder = GameObject.FindGameObjectWithTag("PathFinder").GetComponent<PathFinder>();
    }
    private void Update()
    {
        // 경로 따라 이동
        if(_currentPath != null && _pathIndex < _currentPath.Count)
        {
            Vector3 targetPos = _currentPath[_pathIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _unitData.MoveSpeed * Time.deltaTime);

            // 목표 지점과 일정 거리내로 가까워지면 다음 인덱스로
            if(Vector3.Distance(transform.position, targetPos) < 0.1f)
                _pathIndex++;
        }
    }
    public void SetSelected(bool selected)
    {
        _unitView.SetSelected(selected);
    }

    public void TakeDamage(int amount)
    {
        _unitData.Hp -= amount;
    }

    public void SetDestination(Vector3 target)
    {
        _currentPath = _pathFinder.FindPath(transform.position, target);
        _pathIndex = 0;
    }
}
