using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [TrackClipType(typeof(AbstractFloatValueControlClip))]
    [TrackBindingType(typeof(AbstractFloatValueController))]
    [DisplayName("Additional/Abstract Float Value Control Track")]
    public class AbstractFloatValueControlTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var customClip = clip.asset as AbstractFloatValueControlClip;
                clip.displayName = $"{customClip.Value:F2}";
            }
            
            var playable = ScriptPlayable<AbstractFloatValueControlMixerBehaviour>.Create(graph, inputCount);
            return playable;
        }
    }
}