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
[UpdateAfter(typeof(BuildPhysicsWorld))]
public class CheckAttackDistanse : SystemBase
{
    private BuildPhysicsWorld builtInPhysicsWorld;
    private EntityManager EM;



    protected override void OnCreate()
    {
        builtInPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        EM = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Attack attack, in Translation translation, in Direction direction, in PhysicsCollider collider) =>
            {
                     var collisionWorld = builtInPhysicsWorld.PhysicsWorld.CollisionWorld;
                if (attack.attack>0)
                {
                    RaycastInput input = new RaycastInput()
                    {
                        Start = translation.Value + new float3(attack.attackMargin * direction.Value, 0, 0),
                        End = translation.Value + new float3(attack.attackDistance * direction.Value, 0, 0),
                        Filter = collider.Value.Value.Filter
                    };

                    Unity.Physics.RaycastHit hit = new Unity.Physics.RaycastHit();
                    Debug.DrawLine(translation.Value + new float3(attack.attackMargin * direction.Value, 0, 0), translation.Value + new float3(attack.attackDistance * direction.Value, 0, 0));
                    if(collisionWorld.CastRay(input, out hit))
                    {
                        Entity hitEntity = builtInPhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                        if (EM.HasComponent<TakeDamage>(hitEntity) && !EM.GetComponentData<TakeDamage>(hitEntity).alreadyTakeDamage)
                        {
                            EM.SetComponentData(hitEntity, new TakeDamage {takeDamage = true, damage =attack.attackDamage, attackDirection = direction.Value });
                        }

                    }


                }

            }).Run();
    }
}
