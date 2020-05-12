using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[AlwaysSynchronizeSystem]
public class ParameterAnimatorSetter : JobComponentSystem
{
    private Dictionary<int,Animator> animators = new Dictionary<int, Animator>();
    private bool swich = false;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Direction dir, ref Speed speed, ref TakeDamage takeDamage, ref Attack attack, in Dead dead, in Id id) =>
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
                if (animators[id.Value].GetFloat("Direction") != 0)
                {
                    dir.Value = animators[id.Value].GetFloat("Direction");
                }

                speed.Value = animators[id.Value].GetFloat("Speed");
                attack.attack = animators[id.Value].GetFloat("Attack");

                animators[id.Value].SetBool("Dead", dead.Value);

                if (takeDamage.alreadyTakeDamage)
                {
                    if (takeDamage.takeDamage)
                    {
                        animators[id.Value].SetBool("TakeDamage", true);
                        takeDamage.takeDamage = false;
                    } else
                    if (animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("TakeDamage"))
                    {
                        animators[id.Value].SetBool("TakeDamage", false);
                    } else
                    if (!animators[id.Value].GetBool("TakeDamage"))
                    {
                        takeDamage.alreadyTakeDamage = false;
                    }
                }
                if (animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("Sword"))
                {
                    attack = new Weapon("Sword").Value;
                }
                if (animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("Gun"))
                {
                    attack = new Weapon("Gun").Value;
                }

            }).Run();

        return default;
    }

}
