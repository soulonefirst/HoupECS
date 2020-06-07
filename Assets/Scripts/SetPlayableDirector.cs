using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SetPlayableDirector : MonoBehaviour
{
    public TimelineAsset[] timelineAssets;
    private void Start()
    { 
        PlayableDirectorBuffer.playableDirector = GetComponent<PlayableDirector>();
        PlayableDirectorBuffer.timelines = timelineAssets;
    }
}
