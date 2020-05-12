using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using System;

public struct Weapon
{
    public Attack Value;

    public Weapon(string weapon)
    {
        if(weapon =="Sword")
        {
            Value = new Attack { attack = 0, attackDamage = 1, attackDistance = 0.5f };
        } else
        Value = new Attack { attack = 0, attackDamage = 1, attackDistance = 3 };
    }
}

