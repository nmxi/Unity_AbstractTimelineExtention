using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractFloatValueControlBehaviour : PlayableBehaviour
    {
        [SerializeField] public float Value = 1f;
    }
}