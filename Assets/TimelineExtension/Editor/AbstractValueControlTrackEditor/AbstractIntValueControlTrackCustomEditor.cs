using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Editor
{
    internal static class AbstractIntValueControlTrackEditorUtility
    {
        internal static Color PrimaryColor = new(1f, 1f, 0.5f);
    }
    
    [CustomTimelineEditor(typeof(AbstractIntValueControlTrack))]
    public class AbstractIntValueControlTrackCustomEditor : TrackEditor
    {
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "CustomTrack";

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = AbstractIntValueControlTrackEditorUtility.PrimaryColor;
            return options;
        }
    }
    
    [CustomTimelineEditor(typeof(AbstractIntValueControlClip))]
    public class AbstractIntValueControlCustomEditor : ClipEditor
    {
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var clipOptions = base.GetClipOptions(clip);
            clipOptions.icons = null;
            clipOptions.highlightColor = AbstractIntValueControlTrackEditorUtility.PrimaryColor;
            return clipOptions;
        }
    }
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AbstractIntValueControlClip))]
    public class AbstractIntValueControlClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}