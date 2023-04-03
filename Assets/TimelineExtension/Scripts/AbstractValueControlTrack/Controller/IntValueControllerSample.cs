using UnityEngine;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class IntValueControllerSample : AbstractIntValueController
    {
        [SerializeField] private int testValue;
        
        protected override void OnValueChanged(int value)
        {
            this.testValue = value;
        }
    }
}