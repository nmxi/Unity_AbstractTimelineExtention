using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractIntValueControlBehaviour : PlayableBehaviour
    {
        [SerializeField] public int Value = 0;
    }
}