using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class Move : SystemBase
{
    protected override void OnUpdate()
    {
                float deltaTime = Time.DeltaTime;
        Entities
            .ForEach((ref Translation translation, in Speed speed, in Direction dir, in TakeDamage takeDamage) =>
            {
                if (takeDamage.alreadyTakeDamage)
                {
                    translation.Value.x += (speed.Value * takeDamage.attackDirection) * deltaTime;
                }
                else
                {
                    translation.Value.x += (speed.Value * dir.Value) *deltaTime;
                }
            }
            ).Run();
    }
}
