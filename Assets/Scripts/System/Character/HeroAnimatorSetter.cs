using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[AlwaysSynchronizeSystem]
public class HeroAnimatorSetter : SystemBase
{
    private Animator animator;
    protected override void OnUpdate()
    {
        Entities
            .WithName("Hero")
            .WithoutBurst()
            .ForEach((ref Attack attack, ref Direction dir, in InputData inputData, in Id id, in Weapons weapons) =>
        {
            if(animator == null)
            {
                foreach (GameObject gObj in DollsBuffer.dolls)
                {
                    if (gObj.GetHashCode() == id.Value)
                    {
                        animator = gObj.GetComponent<Animator>();
                    }
                }
            }
            animator.SetBool("Left", inputData.left);
            animator.SetBool("Right", inputData.right);
            animator.SetBool("MouseLeft", inputData.mouseLeft);
            animator.SetBool("MouseRight", inputData.mouseRight);

            if (animator.GetFloat("Direction") != 0)
            {
                dir.Value = animator.GetFloat("Direction");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Sword"))
            {
                attack.attackDamage = weapons.swordDamage;
                attack.attackDistance = weapons.swordDistance;
                attack.attackMargin = weapons.swordMargin;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Gun"))
            {
                attack.attackDamage = weapons.pistolDamage;
                attack.attackDistance = weapons.pistolDistance;
                attack.attackMargin = weapons.pistolMargin;
            }
        }).Run();
    }

}
