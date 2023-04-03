using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractBoolValueControlClip : PlayableAsset, ITimelineClipAsset
    {
        public bool Value;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var data = new AbstractBoolValueControlBehaviour
            {
                Value = Value
            };

            var playable = ScriptPlayable<AbstractBoolValueControlBehaviour>.Create(graph, data);

            return playable;
        }
    }
}