using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SpecialParameterAnimatorSetter : SystemBase
{
    bool jamp = false;
    private Object[] timelines;
    private Dictionary<int, Animator> animators = new Dictionary<int, Animator>();
    protected override void OnCreate()
    {
        timelines = Resources.LoadAll("Timelines", typeof(TimelineAsset));
    }
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((in Wait wait, in Id id) => {
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
                if (wait.Value == true && jamp == false && animators[id.Value].name == "BossDoll")
                {
                    animators[id.Value].GetComponent<PlayableDirector>().Play((TimelineAsset)timelines[0]);
                    jamp = true;
                }
            }).Run();
    }

}
