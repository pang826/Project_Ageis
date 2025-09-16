using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitData", menuName = "Game/Unit Data")]

public class UnitData : ScriptableObject
{
    public string Name;
    public int Hp;
    public int Damage;
    public float MoveSpeed;
}
