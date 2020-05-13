using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[UpdateAfter(typeof(ParameterAnimatorSetter))]
[AlwaysSynchronizeSystem]
public class DamageTaker : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithoutBurst()
            .ForEach((ref TakeDamage takeDamage, ref Hp hp, ref Dead dead) =>
            {
                if (takeDamage.takeDamage && !takeDamage.alreadyTakeDamage)
                {
                    hp.Value -= takeDamage.damage;
                    takeDamage.alreadyTakeDamage = true;
                    if(hp.Value < 1)
                    {
                        dead.Value = true;
                    }
                }

            }).Run();
        return default;
    }
}
