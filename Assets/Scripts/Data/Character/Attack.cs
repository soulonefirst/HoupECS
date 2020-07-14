using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct Attack : IComponentData
{
    public float attack;
    public int attackDamage;
    public float attackDistance;
    public float attackMargin;
}
