using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    /// <summary>
    /// ObjectのActiveが切り替わったときに渡されるデータ
    /// </summary>
    public class ObjectActivationEvent
    {
        public readonly bool IsActive;
        public readonly Transform ObjectTransform;

        public ObjectActivationEvent(bool isActive, Transform objectTransform)
        {
            IsActive = isActive;
            ObjectTransform = objectTransform;
        }
    }
}