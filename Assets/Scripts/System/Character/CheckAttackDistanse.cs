using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Transforms;
[AlwaysSynchronizeSystem]
public class CheckAttackDistanse : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
       Entities
            .WithoutBurst()
            .ForEach((ref Attack attack, in Translation translation, in Direction direction, in PhysicsCollider collider) =>
            {
                var physicsWorldSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
                var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
                if (attack.attack>0)
                {
                    RaycastInput input = new RaycastInput()
                    {
                        Start = translation.Value + new float3(attack.attackMargin * direction.Value, 0, 0),
                        End = translation.Value + new float3(attack.attackDistance * direction.Value, 0, 0),
                        Filter = collider.Value.Value.Filter
                    };

                    Unity.Physics.RaycastHit hit = new Unity.Physics.RaycastHit();
                    Debug.DrawLine(translation.Value, translation.Value + new float3(attack.attackDistance * direction.Value, 0, 0));
                    if(collisionWorld.CastRay(input, out hit))
                    {
                        Entity hitEntity = physicsWorldSystem.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                        if (!World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<TakeDamage>(hitEntity).alreadyTakeDamage)
                        {
                            World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(hitEntity, new TakeDamage {takeDamage = true, damage =attack.attackDamage, attackDirection = direction.Value });
                        }

                    }


                }

            }).Run();
        return default;
    }

}
