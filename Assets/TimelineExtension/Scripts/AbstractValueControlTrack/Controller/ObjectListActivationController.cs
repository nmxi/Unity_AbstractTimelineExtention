using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    /// <summary>
    /// Int値でオブジェクトのListの要素のActiveを切り替える
    /// </summary>
    public class ObjectListActivationController : AbstractIntValueController
    {
        [SerializeField] private List<GameObject> targetObjectList;
        [SerializeField] private UnityEvent<ObjectActivationEvent> OnObjectActivationEvent;
        
        private int lastFrameValue = -1;
        
        protected override void OnValueChanged(int value)
        {
            if (lastFrameValue == value)
            {
                return;
            }
            
            SetActiveObjects(value);
            
            lastFrameValue = value;
        }
        
        private void SetActiveObjects(int value)
        {
            for (var i = 0; i < targetObjectList.Count; i++)
            {
                var isActive = i == value;
                targetObjectList[i].SetActive(isActive);
                
                var eventData = new ObjectActivationEvent(isActive, targetObjectList[i].transform);
                OnObjectActivationEvent?.Invoke(eventData);
            }
        }
    }
}