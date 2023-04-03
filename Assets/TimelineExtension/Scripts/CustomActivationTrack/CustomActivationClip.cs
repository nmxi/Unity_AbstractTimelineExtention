using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack
{
    [DisplayName("Custom Activation Clip")]  
    [Serializable]
    public class CustomActivationClip : PlayableAsset, ITimelineClipAsset
    {
        [NonSerialized] public int TrackIndexPassingThrough;
        [NonSerialized] public int ClipIndexPassingThrough;
        [NonSerialized] public double StartTimingPassthrough;
        [NonSerialized] public double EndTimingPassthrough;
        [NonSerialized] public double DurationPassthrough;
    
        private CustomActivationPlayableBehavior data = new ();
    
        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            data.TrackIndex = TrackIndexPassingThrough;
            data.ClipIndex = ClipIndexPassingThrough;
            data.StartTiming = StartTimingPassthrough;
            data.EndTiming = EndTimingPassthrough;
            data.Duration = DurationPassthrough;

            return ScriptPlayable<CustomActivationPlayableBehavior>.Create(graph, data);
        }
    }   
}