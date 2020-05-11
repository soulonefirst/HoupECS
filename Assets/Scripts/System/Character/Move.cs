﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class Move : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities
            .ForEach((ref Translation translation, in Speed speed, in Direction dir, in TakeDamage takeDamage) =>
            {
                if (takeDamage.takeDamage)
                {
                    translation.Value.x += (speed.Value * takeDamage.attackDirection) * deltaTime;
                }
                else
                {
                    translation.Value.x += (speed.Value * dir.Value) *deltaTime;
                }
            }
            ).Run();
        return default;
    }
}