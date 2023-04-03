using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractIntValueControlClip : PlayableAsset, ITimelineClipAsset
    {
        public int Value;
        
        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var data = new AbstractIntValueControlBehaviour
            {
                Value = Value
            };
            
            var playable = ScriptPlayable<AbstractIntValueControlBehaviour>.Create(graph, data);

            return playable;
        }
    }
}