using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectable : MonoBehaviour, ISelectable
{
    public static List<UnitSelectable> AllUnits = new List<UnitSelectable>();
    private UnitController _controller;

    private void Awake()
    {
        _controller = GetComponent<UnitController>();
    }
    private void OnEnable() => AllUnits.Add(this);
    private void OnDisable() => AllUnits.Remove(this);
    public Transform GetTransform()
    {
        return this.transform;
    }

    public void SetSelected(bool selected)
    {
        _controller.SetSelected(selected);
    }
}
