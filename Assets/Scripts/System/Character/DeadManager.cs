using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
[UpdateAfter(typeof(DamageTaker))]

public class DeadManager : SystemBase
{
    EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;
    protected override void OnCreate()
    {
        base.OnCreate();
        // Find the ECB system once and store it for later usage
        m_EndSimulationEcbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer();
        Entities
            .WithoutBurst()
           .ForEach((ref Entity entity, ref PhysicsCollider collider, ref Dead dead, in Hp hp) =>
           {
           if (hp.Value < 1 && dead.Value == false)
               {
                  dead.Value = true;
                  ecb.RemoveComponent<PhysicsCollider>(entity);
               }

           }).Run();
    }
}
