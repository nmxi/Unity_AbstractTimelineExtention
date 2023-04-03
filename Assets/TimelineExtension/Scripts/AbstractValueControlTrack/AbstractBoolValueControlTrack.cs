using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [TrackClipType(typeof(AbstractBoolValueControlClip))]
    [TrackBindingType(typeof(AbstractBoolValueController))]
    [DisplayName("Additional/Abstract Bool Value Control Track")]
    public class AbstractBoolValueControlTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var customClip = clip.asset as AbstractBoolValueControlClip;
                clip.displayName = customClip.Value.ToString();
            }
            
            var playable = ScriptPlayable<AbstractBoolValueControlMixerBehaviour>.Create(graph, inputCount);
            return playable;
        }
    }
}