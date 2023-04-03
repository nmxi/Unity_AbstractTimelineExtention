using System;
using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    [Serializable]
    public class AbstractColorValueControlBehaviour : PlayableBehaviour
    {
        [SerializeField] public Color Value = Color.white;
    }
}