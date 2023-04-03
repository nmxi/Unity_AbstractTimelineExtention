using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Editor
{
    internal static class AbstractColorValueControlTrackEditorUtility
    {
        internal static Color PrimaryColor = new(0.5f, 0.5f, 1f);
    }
    
    [CustomTimelineEditor(typeof(AbstractColorValueControlTrack))]
    public class AbstractColorValueControlTrackCustomEditor : TrackEditor
    {
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            track.name = "CustomTrack";

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = AbstractColorValueControlTrackEditorUtility.PrimaryColor;
            
            return options;
        }
    }
    
    [CustomTimelineEditor(typeof(AbstractColorValueControlClip))]
    public class AbstractColorValueControlCustomEditor : ClipEditor
    {
        Dictionary<AbstractColorValueControlClip, Texture2D> textures = new();
        
        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var clipOptions = base.GetClipOptions(clip);
            clipOptions.icons = null;
            clipOptions.highlightColor = AbstractColorValueControlTrackEditorUtility.PrimaryColor;
            return clipOptions;
        }
        
        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
        {
            var tex = GetGradientTexture(clip);
            if (tex) GUI.DrawTexture(region.position, tex);
        }
        
        public override void OnClipChanged(TimelineClip clip)
        {
            GetGradientTexture(clip, true);
        }

        Texture2D GetGradientTexture(TimelineClip clip, bool update = false)
        {
            var tex = Texture2D.whiteTexture;

            var customClip = clip.asset as AbstractColorValueControlClip;
            if (!customClip) return tex;

            var gradient = customClip.Value;
            if (gradient == null) return tex;

            if (update) 
            {
                textures.Remove(customClip);
            }
            else
            {
                textures.TryGetValue(customClip, out tex);
                if (tex) return tex;
            }

            var b = (float)(clip.blendInDuration / clip.duration);
            tex = new Texture2D(128, 1);
            for (int i = 0; i < tex.width; ++i)
            {
                var t = (float)i / tex.width;
                var color = customClip.Value;
                
                //get max color element
                var max = Mathf.Max(color.r, color.g, color.b);
                if (max > 1f)
                {
                    color.r /= max;
                    color.g /= max;
                    color.b /= max;
                }

                color.a = 1f;
                if (b > 0f) color.a = Mathf.Min(t / b, 1f);
                tex.SetPixel(i, 0, color);
            }
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
    [CustomEditor(typeof(AbstractColorValueControlClip))]
    public class AbstractColorValueControlClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}