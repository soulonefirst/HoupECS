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
    private TimelineAsset[] timelines;
    private Dictionary<int, Animator> animators = new Dictionary<int, Animator>();
    protected override void OnUpdate()
    {
        if(timelines == null)
            timelines = PlayableDirectorBuffer.timelines;
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

            if (animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("PD"))
            {
                    switch (animators[id.Value].GetFloat("Timeline"))
                    {
                        case 0:
                            Debug.Log(1);
                            PlayableDirectorBuffer.playableDirector.Play(timelines[0]);
                            animators[id.Value].PlayInFixedTime("WalkRight",-1, (float)PlayableDirectorBuffer.playableDirector.duration);
                            break;
                        case 1:
                            PlayableDirectorBuffer.playableDirector.Play(timelines[1]);
                            break;
                        case 2:
                            PlayableDirectorBuffer.playableDirector.Play(timelines[2]);
                            break;
                    }
                }
                animators[id.Value].SetBool("Wait", wait.Value);

            }).Run();
    }

}
