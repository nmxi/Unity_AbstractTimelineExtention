using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Editor
{
    internal static class AbstractBoolValueControlTrackEditorUtility
    {
        internal static Color PrimaryColor = new(0.5f, 1f, 0.5f);
    }
    
    [CustomTimelineEditor(typeof(AbstractBoolValueControlTrack))]
    public class AbstractBoolValueControlTrackCustomEditor : TrackEditor
    {
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "CustomTrack";

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = AbstractBoolValueControlTrackEditorUtility.PrimaryColor;
            
            // Debug.Log(binding.GetType());
            return options;
        }
    }
    
    [CustomTimelineEditor(typeof(AbstractBoolValueControlClip))]
    public class AbstractBoolValueControlCustomEditor : ClipEditor
    {
        Dictionary<AbstractBoolValueControlClip, Texture2D> textures = new();
        
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var clipOptions = base.GetClipOptions(clip);
            clipOptions.icons = null;
            clipOptions.highlightColor = AbstractBoolValueControlTrackEditorUtility.PrimaryColor;
            return clipOptions;
        }
        
        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
        {
            var tex = GetSolidColorTexture(clip);
            if (tex) GUI.DrawTexture(region.position, tex);
        }
        
        public override void OnClipChanged(TimelineClip clip)
        {
            GetSolidColorTexture(clip, true);
        }
        
        Texture2D GetSolidColorTexture(TimelineClip clip, bool update = false)
        {
            var tex = Texture2D.blackTexture;
        
            var customClip = clip.asset as AbstractBoolValueControlClip;
        
            if (update) 
            {
                textures.Remove(customClip);
            }
            else
            {
                textures.TryGetValue(customClip, out tex);
                if (tex) return tex;
            }
        
            var c = customClip.Value ? new Color(0.8f, 0.8f, 0.8f) : new Color(0.2f, 0.2f, 0.2f);
            tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, c);
            tex.Apply();
            
            if (textures.ContainsKey(customClip))
            {
                textures[customClip] = tex;
            }
            else
            {
                textures.Add(customClip, tex);   
            }
        
            return tex;
        }
    }
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AbstractBoolValueControlClip))]
    public class AbstractBoolValueControlClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}