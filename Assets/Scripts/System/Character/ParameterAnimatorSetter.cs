using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[AlwaysSynchronizeSystem]
public class ParameterAnimatorSetter : SystemBase
{
    private Dictionary<int,Animator> animators = new Dictionary<int, Animator>();
    EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Speed speed, ref TakeDamage takeDamage, ref Attack attack, in Entity entity, in Dead dead, in Id id) =>
            {
                if (!animators.ContainsKey(id.Value))
                {
                    foreach (GameObject gObj in DollsBuffer.dolls)
                    {
                        if (gObj.GetHashCode() == id.Value)
                        {
                            animators.Add(id.Value,gObj.GetComponent<Animator>());
                        }
                    }
                }
                

                speed.Value = animators[id.Value].GetFloat("Speed");
                attack.attack = animators[id.Value].GetFloat("Attack");
                animators[id.Value].SetBool("Dead", dead.Value);


                if (takeDamage.alreadyTakeDamage)
                {
                    if (takeDamage.takeDamage)
                    {
                        animators[id.Value].SetBool("TakeDamage", true);
                    } else
                    if (animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("TakeDamage"))
                    {
                        animators[id.Value].SetBool("TakeDamage", false);
                    } else
                    if (!animators[id.Value].GetBool("TakeDamage"))
                    {
                        takeDamage.alreadyTakeDamage = false;
                        takeDamage.takeDamage = false;
                    }
                }

            }).Run();
    }

}
