using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractBoolValueControlBehaviour : PlayableBehaviour
    {
        [SerializeField] public bool Value = false;
    }
}