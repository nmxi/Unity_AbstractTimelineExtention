using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Editor
{
    internal static class AbstractFloatValueControlTrackEditorUtility
    {
        internal static Color PrimaryColor = new(1f, 0.5f, 0.5f);
    }
    
    [CustomTimelineEditor(typeof(AbstractFloatValueControlTrack))]
    public class AbstractFloatValueControlTrackCustomEditor : TrackEditor
    {
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "CustomTrack";

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = AbstractFloatValueControlTrackEditorUtility.PrimaryColor;
            return options;
        }
    }
    
    [CustomTimelineEditor(typeof(AbstractFloatValueControlClip))]
    public class AbstractFloatValueControlCustomEditor : ClipEditor
    {
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var clipOptions = base.GetClipOptions(clip);
            clipOptions.icons = null;
            clipOptions.highlightColor = AbstractFloatValueControlTrackEditorUtility.PrimaryColor;
            return clipOptions;
        }
    }
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AbstractFloatValueControlClip))]
    public class AbstractFloatValueControlClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}