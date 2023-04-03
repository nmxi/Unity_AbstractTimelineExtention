using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack.Editor
{
    [DisplayName("ActivationTrackConverter")]
    public class ActivationTrackConverter : EditorWindow
    {
        private const string DEBUGLOG_PREFIX = "[<color=#FF9654>Converter</color>]";
        
        [MenuItem("Tools/ActivationTrackConverter")]
        public static void ConvertActivationTrackToCustomActivationTrack()
        {
            var directors = FindObjectsOfType<PlayableDirector>();
            Debug.Log($"{DEBUGLOG_PREFIX} Found playable directors : {directors.Length}");
            
            foreach (var director in directors)
            {
                var timelineAsset = director.playableAsset as TimelineAsset;
                Debug.Log($"{DEBUGLOG_PREFIX} Target Timeline Asset : {timelineAsset.name}");
                var tracks = timelineAsset.GetOutputTracks().ToList();

                var activationTrackCount = 0;
                //Activation Track convert to custom activation track
                for (var index = 0; index < tracks.Count; index++)
                {
                    var track = tracks[index];
                    if (track.GetType() == typeof(ActivationTrack))
                    {
                        if (track.muted)
                        {
                            Debug.Log($"{DEBUGLOG_PREFIX} track muted, skip : {track.name}");
                            continue;
                        }

                        //create custom activation track
                        var newCustomActivationTrack = timelineAsset.CreateTrack<CustomActivationTrack>(null, track.name);
                        TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
                        newCustomActivationTrack.name = track.name;

                        //copy clip
                        var activationClips = track.GetClips().ToList();
                        foreach (var activationClip in activationClips)
                        {
                            var customActivationClip = newCustomActivationTrack.CreateDefaultClip();
                            customActivationClip.displayName = activationClip.displayName;
                            customActivationClip.start = activationClip.start;
                            customActivationClip.duration = activationClip.duration;
                        }
                        
                        //get binding
                        var binding = director.playableAsset.outputs.ToArray()[index].sourceObject;
                        var bindingObject = director.GetGenericBinding(binding);
                        
                        //set binding
                        director.SetGenericBinding(newCustomActivationTrack, bindingObject);

                        activationTrackCount++;
                        track.muted = true;
                    }
                }
                
                Debug.Log($"{DEBUGLOG_PREFIX} Convert Activation Track Count : {activationTrackCount}");
                
                //save asset
                AssetDatabase.SaveAssets();
            }
        }
    }
}