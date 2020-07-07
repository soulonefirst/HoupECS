using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;


public class SpecialParameterAnimatorSetter : SystemBase
{
    private Dictionary<int, Animator> animators = new Dictionary<int, Animator>();
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Wait wait, in Id id) => {
            if (!animators.ContainsKey(id.Value))
            {
                foreach (GameObject gObj in DollsBuffer.dolls)
                {
                    if (gObj.GetHashCode() == id.Value)
                    {
                        animators.Add(id.Value, gObj.GetComponent<Animator>());
                    }
                }
            }

                animators[id.Value].SetBool("Wait", wait.Value);

            }).Run();
    }

}
