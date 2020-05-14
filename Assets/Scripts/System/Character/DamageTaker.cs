using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[UpdateAfter(typeof(ParameterAnimatorSetter))]
[AlwaysSynchronizeSystem]
public class DamageTaker : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((ref TakeDamage takeDamage, ref Hp hp) =>
            {
                if (takeDamage.takeDamage && !takeDamage.alreadyTakeDamage)
                {
                    hp.Value -= takeDamage.damage;
                    takeDamage.alreadyTakeDamage = true;
                }

            }).Run();
    }
}
