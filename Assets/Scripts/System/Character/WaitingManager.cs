using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;


public class WaitingManager : SystemBase
{
    private Dead guardian1;
    private Dead guardian2;
    private EntityManager entityManager;
    private NativeArray<Entity> entities;

    bool guardiansDead = false;
    bool wait = false;

    protected override void OnCreate()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entities = entityManager.GetAllEntities();
    }
    protected override void OnUpdate()
    {
        if(guardiansDead == false)
        {
            foreach(Entity entity in entities)
            {
                if (entityManager.GetName(entity) == "Guardian1")
                    guardian1 = entityManager.GetComponentData<Dead>(entity);
                if (entityManager.GetName(entity) == "Guardian2")
                    guardian2 = entityManager.GetComponentData<Dead>(entity);
                    if(guardian1.Value && guardian2.Value )
                    {
                        wait = true;
                        guardiansDead = true;
                    }
            }
        }
        Entities
            .WithoutBurst()
            .ForEach((Entity ent, ref Wait entitiesWait) =>
            {

                entitiesWait.Value = wait;


            }).Run();
    }
}
