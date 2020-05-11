using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct TakeDamage : IComponentData
{
    public bool takeDamage;
    public bool alreadyTakeDamage;
    public int damage;
    public float attackDirection;
}
