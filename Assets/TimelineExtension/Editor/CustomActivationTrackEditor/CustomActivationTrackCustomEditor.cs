using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack.Editor
{
    internal static class CustomActivationTrackEditorUtility
    {
        internal static Color PrimaryColor = new(0.5f, 1f, 0.5f);
    }
    
    [CustomTimelineEditor(typeof(CustomActivationTrack))]
    public class CustomActivationTrackCustomEditor : TrackEditor
    {
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "CustomTrack";

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = CustomActivationTrackEditorUtility.PrimaryColor;
            return options;
        }
    }
    
    [CustomTimelineEditor(typeof(CustomActivationClip))]
    public class CustomActivationClipCustomEditor : ClipEditor
    {
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var clipOptions = base.GetClipOptions(clip);
            clipOptions.icons = null;
            clipOptions.highlightColor = CustomActivationTrackEditorUtility.PrimaryColor;
            return clipOptions;
        }
    }
}