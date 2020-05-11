using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Mathematics;
using Unity.Transforms;
[UpdateBefore(typeof(Move))]
public class CollideDetection : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
               float deltaTime = Time.DeltaTime;
        Entities
           .WithoutBurst()
           .ForEach((ref Speed speed, in Translation translation, in Direction direction, in PhysicsCollider collider) =>
           {
               var physicsWorldSystem = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
               var collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
               if (speed.Value > 0)
               {
                   RaycastInput input = new RaycastInput()
                   {
                       Start = translation.Value,
                       End = translation.Value + new float3(0.1350121f * direction.Value, 0, 0),
                       Filter = collider.Value.Value.Filter
                   };
                   Unity.Physics.RaycastHit hit = new Unity.Physics.RaycastHit();
                   Debug.DrawLine(translation.Value, translation.Value + new float3(0.12f * direction.Value, 0, 0));
                   if (collisionWorld.CastRay(input, out hit))
                   {
                       speed.Value = 0;

                   }


               }

           }).Run();
        return default;
    }
}
