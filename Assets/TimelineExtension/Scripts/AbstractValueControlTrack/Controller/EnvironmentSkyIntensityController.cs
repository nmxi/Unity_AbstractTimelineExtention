using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class EnvironmentSkyIntensityController : AbstractFloatValueController
    {
        protected override void OnValueChanged(float value)
        {
            RenderSettings.ambientIntensity = value;
        }

        protected override float currentValueFromIndex0()
        {
            return RenderSettings.ambientIntensity;
        }
    }
}