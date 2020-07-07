using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;

public class WaitingManager : SystemBase
{
    private bool guardian1Dead;
    private bool guardian2Dead;
    bool guardiansDead = false;
    public static bool wait;
    private EntityManager eM;
    protected override void OnCreate()
    {
        eM = World.DefaultGameObjectInjectionWorld.EntityManager;
        wait = false;
    }
    protected override void OnUpdate()
    {
        if (guardiansDead == false)
        {
            Entities
            .WithoutBurst()
            .ForEach((Entity entity, in Dead dead) =>
            {
                if (eM.GetName(entity) == "Guardian1")
                    guardian1Dead = dead.Value;
                if (eM.GetName(entity) == "Guardian2")
                    guardian2Dead = dead.Value;
                    if (guardian1Dead && guardian2Dead )
                    {
                     wait = true;
                    guardiansDead = true;
                    }
            }).Run();
        }

            Entities
                .WithoutBurst()
                .ForEach((ref Wait entitiesWait) =>
                {

                    entitiesWait.Value = wait;


                }).Run();
    }
}
