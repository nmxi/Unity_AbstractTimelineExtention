using System.Collections.Generic;
using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class LightColorController : AbstractColorValueController
    {
        [SerializeField] private List<Light> lightList;

        protected override void OnValueChanged(Color value)
        {
            foreach (var light in lightList)
            {
                light.color = value;
            }
        }
    }
}