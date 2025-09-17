using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectable : MonoBehaviour, ISelectable
{
    private UnitController _controller;

    private void Awake()
    {
        _controller = GetComponent<UnitController>();
    }
    public Transform GetTransform() => this.transform;

    public void SetSelected(bool selected)
    {
        _controller.SetSelected(selected);
    }
    public void SetDestination(Vector3 target)
    {
        _controller.SetDestination(target);
    }
}
