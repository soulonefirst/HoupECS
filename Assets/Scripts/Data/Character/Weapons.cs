using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]

public struct Weapons : IComponentData
{
    public int pistolDamage;
    public float pistolDistance;
    public float pistolMargin;
    public int swordDamage;
    public float swordDistance;
    public float swordMargin;
}
