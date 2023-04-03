using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractColorValueControlClip : PlayableAsset
    {
        [ColorUsage(true, true)] public Color Value;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var data = new AbstractColorValueControlBehaviour
            {
                Value = Value
            };
            
            var playable = ScriptPlayable<AbstractColorValueControlBehaviour>.Create(graph, data);

            return playable;
        }
    }
}