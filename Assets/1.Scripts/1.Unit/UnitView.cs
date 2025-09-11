using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛의 시각적 표현 전담(Mesh, Animaition...)
public class UnitView : MonoBehaviour
{
    public void SetSelected(bool selected)
    {
        // 임시 드래그 확인용 색 변경
        transform.GetChild(0).GetComponent<Renderer>().material.color = selected ? Color.green : Color.white;
    }
}
