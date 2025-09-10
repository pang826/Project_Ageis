using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectable : MonoBehaviour, ISelectable
{
    public bool IsSelectable { get; private set; }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void SetSelected(bool selected)
    {
        IsSelectable = selected;
        // 임시 드래그 확인용 색 변경
        transform.GetChild(0).GetComponent<Renderer>().material.color = selected ? Color.green : Color.white;
    }
}
