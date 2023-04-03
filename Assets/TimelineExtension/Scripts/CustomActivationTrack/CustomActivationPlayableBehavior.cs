using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack
{
    [Serializable]
    public class CustomActivationPlayableBehavior : PlayableBehaviour
    {
        [HideInInspector] public int TrackIndex;
        [HideInInspector] public int ClipIndex;
        [HideInInspector] public double StartTiming;
        [HideInInspector] public double EndTiming;
        [HideInInspector] public double Duration;
    }
}