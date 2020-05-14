using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
[UpdateAfter(typeof(DamageTaker))]

public class DeadManager : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
           .ForEach((ref PhysicsCollider collider, ref Dead dead, in Hp hp) =>
           {
               if (hp.Value < 1)
               {
                   collider.Value.Value.Filter = CollisionFilter.Zero;
                  dead.Value = true;
               }

           }).Run();
    }
}
