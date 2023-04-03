using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [TrackClipType(typeof(AbstractColorValueControlClip))]
    [TrackBindingType(typeof(AbstractColorValueController))]
    [DisplayName("Additional/Abstract Color Value Control Track")]
    public class AbstractColorValueControlTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var customClip = clip.asset as AbstractColorValueControlClip;
                var c = customClip.Value;
                var s = $"{c.r:F1}, {c.g:F1}, {c.b:F1}, {c.a:F1}";
                clip.displayName = s;
            }
            
            var playable = ScriptPlayable<AbstractColorValueControlMixerBehaviour>.Create(graph, inputCount);
            return playable;
        }
    }
}