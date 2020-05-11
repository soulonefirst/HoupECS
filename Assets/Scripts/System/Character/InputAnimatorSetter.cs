using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[AlwaysSynchronizeSystem]
public class InputAnimatorSetter : JobComponentSystem
{
    private Animator animator;
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithName("Hero")
            .WithoutBurst()
            .ForEach((in InputData inputData, in Id id) =>
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
        }).Run();

        return default;
    }

}
