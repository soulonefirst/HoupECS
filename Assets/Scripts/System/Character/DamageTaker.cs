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
            .ForEach((ref TakeDamage takeDamage, ref Hp hp) =>
            {
                if (takeDamage.takeDamage && !takeDamage.alreadyTakeDamage)
                {
                    hp.Value -= takeDamage.damage;
                    takeDamage.alreadyTakeDamage = true;
                }

            }).Run();
        return default;
    }
}
