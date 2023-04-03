using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    /// <summary>
    /// Bool値でListに設定されたオブジェクトを一括でActiveを切り替える
    /// </summary>
    public class ObjectActivationController : AbstractBoolValueController
    {
        [SerializeField] private List<GameObject> targetObjectList;
        [SerializeField] private UnityEvent<ObjectActivationEvent> OnObjectActivationEvent;

        private bool isActiveLastFrame;
        
        protected override void OnValueInput(bool value)
        {
            if (isActiveLastFrame == value)
            {
                return;
            }
            
            SetActive(value);
            
            isActiveLastFrame = value;
        }
        
        private void SetActive(bool isActive)
        {
            foreach (var targetObject in targetObjectList)
            {
                targetObject.SetActive(isActive);
                
                var eventData = new ObjectActivationEvent(isActive, targetObject.transform);
                OnObjectActivationEvent?.Invoke(eventData);
            }
        }
    }
}