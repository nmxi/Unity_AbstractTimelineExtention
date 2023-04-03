using System.Collections.Generic;
using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    [ExecuteAlways]
    public class RendererMaterialColorValueController : AbstractColorValueController
    {
        public List<RendererMaterialData> RendererMaterialDataList;

        private Dictionary<string, MaterialPropertyBlock> _materialPropertyBlockDict = new();
        
        private void Start()
        {
            _materialPropertyBlockDict = new Dictionary<string, MaterialPropertyBlock>();
        }
        
        protected override void OnValueChanged(Color value)
        {
            for (var i = 0; i < RendererMaterialDataList.Count; i++)
            {
                var item = RendererMaterialDataList[i];
                var renderer = RendererMaterialDataList[i].TargetRenderer;
                
                if (renderer == null)
                {
                    continue;
                }

                MaterialPropertyBlock block;
                if (!_materialPropertyBlockDict.ContainsKey(item.TargetRenderer.name))
                {
                    block = new MaterialPropertyBlock();
                    _materialPropertyBlockDict.Add(item.TargetRenderer.name, block);
                }
                else
                {
                    block = _materialPropertyBlockDict[item.TargetRenderer.name];
                }
                
                renderer.GetPropertyBlock(block);
                block.SetVector(item.PropertyName, value);
                renderer.SetPropertyBlock(block);
            }
        }
    }
}