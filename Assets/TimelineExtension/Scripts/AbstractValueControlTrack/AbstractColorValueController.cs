using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public abstract class AbstractColorValueController : MonoBehaviour
    {
        public Color value { get; private set; }

        public void SetValue(Color value)
        {
            if (value == this.value)
            {
                return;
            }
            
            this.value = value;
            OnValueChanged(this.value);
        }

        protected abstract void OnValueChanged(Color value);
    }
}