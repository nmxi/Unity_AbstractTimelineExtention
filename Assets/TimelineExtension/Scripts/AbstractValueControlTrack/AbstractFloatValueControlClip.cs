using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractFloatValueControlClip : PlayableAsset
    {
        public float Value;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var data = new AbstractFloatValueControlBehaviour
            {
                Value = Value
            };
            
            var playable = ScriptPlayable<AbstractFloatValueControlBehaviour>.Create(graph, data);

            return playable;
        }
    }
}