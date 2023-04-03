using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public abstract class AbstractIntValueController : MonoBehaviour
    {
        public int value { get; private set; }

        public void SetValue(int value)
        {
            this.value = value;
            OnValueChanged(this.value);
        }

        protected abstract void OnValueChanged(int value);
    }
}