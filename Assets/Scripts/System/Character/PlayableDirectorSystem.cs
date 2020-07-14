
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.Timeline;

public class PlayableDirectorSystem : SystemBase
{
    private TimelineAsset[] timelines;
    private Dictionary<int, Animator> animators = new Dictionary<int, Animator>();
    private double timelineDuration;
    protected override void OnUpdate()
    {
        var time = Time.ElapsedTime;
        if (timelines == null)
            timelines = PlayableDirectorBuffer.timelines;
        Entities
            .WithoutBurst()
            .ForEach((ref PlayableDirectorData playable, in StrikeData strike, in Id id) =>
            {
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
                animators[id.Value].SetBool("Strike", strike.Value);
                if (!playable.Value && animators[id.Value].GetCurrentAnimatorStateInfo(0).IsTag("PD"))
                {
                    switch (animators[id.Value].GetFloat("Timeline"))
                    {
                        case 0:
                            PlayableDirectorBuffer.playableDirector.Play(timelines[0]);
                            timelineDuration = time + timelines[0].duration;
                            playable.Value = true;
                            break;
                        case 1:
                            PlayableDirectorBuffer.playableDirector.Play(timelines[1]);
                            timelineDuration = time + timelines[1].duration;
                            playable.Value = true;
                            break;
                        case 2:
                            PlayableDirectorBuffer.playableDirector.Play(timelines[2]);
                            timelineDuration = time + timelines[2].duration;
                            playable.Value = true;
                            break;
                    }
                }
                else if (playable.Value && time > timelineDuration)
                {
                    animators[id.Value].SetTrigger("TimelineEnd");
                    PlayableDirectorBuffer.playableDirector.Stop();
                    playable.Value = false;
                    WaitingManager.wait = false;
                }


            }).Run();
    }
}
