using System.Collections.Generic;
using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class RendererMaterialFloatValueController : AbstractFloatValueController
    {
        [SerializeField] private float multiplier = 1f;
        public List<RendererMaterialData> RendererMaterialDataList;

        private Dictionary<string, MaterialPropertyBlock> _materialPropertyBlockDict = new();

        private void Start()
        {
            _materialPropertyBlockDict = new Dictionary<string, MaterialPropertyBlock>();
        }

        protected override void OnValueChanged(float value)
        {
            // Debug.Log($"{gameObject.name} {value}");
            
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
                value *= multiplier;
                block.SetFloat(item.PropertyName, value);
                renderer.SetPropertyBlock(block);
            }
        }

        protected override float currentValueFromIndex0()
        {
            var item = RendererMaterialDataList[0];
            var renderer = RendererMaterialDataList[0];

            return renderer.TargetRenderer.materials[renderer.MaterialIndex].GetFloat(item.PropertyName);
        }
    }
}