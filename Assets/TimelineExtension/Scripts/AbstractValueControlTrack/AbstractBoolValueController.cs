using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public abstract class AbstractBoolValueController : MonoBehaviour
    {
        public bool value { get; private set; }

        public void SetValue(bool value)
        {
            this.value = value;
            OnValueInput(this.value);
        }

        protected abstract void OnValueInput(bool value);
    }
}