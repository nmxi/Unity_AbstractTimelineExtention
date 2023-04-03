using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class EnvironmentAmbientColorController : AbstractColorValueController
    {
        protected override void OnValueChanged(Color value)
        {
            RenderSettings.ambientSkyColor = value;
        }
    }
}