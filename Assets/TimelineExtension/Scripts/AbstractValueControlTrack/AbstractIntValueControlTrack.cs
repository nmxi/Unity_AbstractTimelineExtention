using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [TrackClipType(typeof(AbstractIntValueControlClip))]
    [TrackBindingType(typeof(AbstractIntValueController))]
    [DisplayName("Additional/Abstract Int Value Control Track")]
    public class AbstractIntValueControlTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var customClip = clip.asset as AbstractIntValueControlClip;
                clip.displayName = $"{customClip.Value}";
            }
            
            var playable = ScriptPlayable<AbstractIntValueControlMixerBehaviour>.Create(graph, inputCount);
            return playable;
        }
    }
}