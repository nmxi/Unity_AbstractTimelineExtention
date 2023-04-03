using System;
using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    [Serializable]
    public class RendererMaterialData
    {
        public Renderer TargetRenderer;
        public int MaterialIndex;
        public string PropertyName;
        
        public void SetValue(float value)
        {
            TargetRenderer.materials[MaterialIndex].SetFloat(PropertyName, value);
        }
    }
}