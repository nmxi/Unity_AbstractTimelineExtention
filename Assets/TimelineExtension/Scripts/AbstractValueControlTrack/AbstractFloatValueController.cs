using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public abstract class AbstractFloatValueController : MonoBehaviour
    {
        public float Value => currentValueFromIndex0();

        public void SetValue(float value)
        {
            OnValueChanged(value);
        }

        protected abstract void OnValueChanged(float value);
        protected abstract float currentValueFromIndex0();
    }
}