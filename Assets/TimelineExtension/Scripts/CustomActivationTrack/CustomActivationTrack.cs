using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack
{
    [TrackClipType(typeof(CustomActivationClip))]
    [TrackBindingType(typeof(GameObject))]
    [DisplayName("Additional/Custom Activation Track")]
    public class CustomActivationTrack : TrackAsset
    {
        [HideInInspector] public int TrackIndex;

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var clips = GetClips().ToArray();
    
            for (var i = 0; i < clips.Length; i++)
            {
                var customActivationClip = clips[i].asset as CustomActivationClip;
                customActivationClip.TrackIndexPassingThrough = TrackIndex;
                customActivationClip.ClipIndexPassingThrough = i;
                customActivationClip.StartTimingPassthrough = clips[i].start;
                customActivationClip.EndTimingPassthrough = clips[i].end;
                customActivationClip.DurationPassthrough = clips[i].duration;
            }

            var playable = ScriptPlayable<CustomActivationMixerBehavior>.Create(graph, inputCount);
            var behaviour = playable.GetBehaviour();
            behaviour.Director = go.GetComponent<PlayableDirector>();
            behaviour.TrackBindingObject = (GameObject)go.GetComponent<PlayableDirector>().GetGenericBinding(this);
            return playable;
        }
    }   
}