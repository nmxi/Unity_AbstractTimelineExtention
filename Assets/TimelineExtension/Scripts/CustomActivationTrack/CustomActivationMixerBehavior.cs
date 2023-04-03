using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace dev.kemomimi.TimelineExtension.CustomActivationTrack
{
    public class CustomActivationMixerBehavior : PlayableBehaviour
    {
        public PlayableDirector Director;
        public GameObject TrackBindingObject;

        private List<ParticleSystem> particleSystemList = new();
        private List<Animator> animatorList = new();

        private bool initialized;
        private float deltaTime;    //前にフレームからの経過時間
        private float lastDeltaTime;
        private float lastTime;

        public override void OnGraphStart(Playable playable)
        {
            initialized = false;
            
            base.OnGraphStart(playable);
            
            Initialize();
        }

        private void Initialize()
        {
            if (TrackBindingObject == null)
            {
                return;
            }
            
            particleSystemList = TrackBindingObject.GetComponentsInChildren<ParticleSystem>().ToList();
            animatorList = TrackBindingObject.GetComponentsInChildren<Animator>().ToList();
            
            initialized = true;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (TrackBindingObject == null)
            {
                return;
            }

            if (!initialized)
            {
                Initialize();
            }

            var inputCount = playable.GetInputCount();
            var currentTime = Director.time <= Director.duration ? Director.time % Director.duration : Director.time;
            
            deltaTime = (float)currentTime - lastTime;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                //非Play時
                particleSystemList = TrackBindingObject.GetComponentsInChildren<ParticleSystem>().ToList();    
                animatorList = TrackBindingObject.GetComponentsInChildren<Animator>().ToList();
            }
#endif

            var isProcessed = false;
            for (var i = 0; i < inputCount; i++)
            {
                if (playable.GetInputWeight(i) <= 0)
                {
                    continue;
                }

                var scriptPlayable = (ScriptPlayable<CustomActivationPlayableBehavior>)playable.GetInput(i);
                var clip = scriptPlayable.GetBehaviour();
                var clipTime = currentTime - clip.StartTiming;

                if (clip.StartTiming <= currentTime && clip.EndTiming >= currentTime &&
                    !TrackBindingObject.activeSelf)
                {
                    TrackBindingObject.SetActive(true);
                    
                    foreach (var particleSystem in particleSystemList)
                    {
                        particleSystem.time = 0f;
                        particleSystem.Simulate((float)clipTime, true, true);
                    }
                }
                else if (lastDeltaTime <= 0 && 0 < deltaTime)
                {
                    // Debug.Log($"lastDeltaTime: {lastDeltaTime}, deltaTime: {deltaTime}");
                    
                    foreach (var particleSystem in particleSystemList)
                    {
                        particleSystem.Simulate((float)clipTime, true);
                    }
                }
                else
                {
                    //ParticleSystem manual update
                    foreach (var particleSystem in particleSystemList)
                    {
                        if (0 < deltaTime)
                        {
                            particleSystem.Simulate(deltaTime, true, false);
                        }
                        else
                        {
                            particleSystem.Simulate((float)clipTime, true);
                        }
                    }

                    //Animation manual update
                    foreach (var animator in animatorList)
                    {
                        for (var j = 0; j < animator.layerCount; j++)
                        {
                            var animatorClipInfoArray = animator.GetCurrentAnimatorClipInfo(j);

                            foreach (var clipInfo in animatorClipInfoArray)
                            {
                                var duration = clipInfo.clip.length;
                                var normalizedTime = clipTime % duration / duration;
                                animator.speed = 0f;
                                animator.ForceStateNormalizedTime((float)normalizedTime);
                                animator.playableGraph.Evaluate();
                            }
                        }
                    }
                }

                lastDeltaTime = deltaTime;
                
                isProcessed = true;
                
                break;
            }
            
            if (!isProcessed)
            {
                TrackBindingObject.SetActive(false);
            }

            lastTime = (float)currentTime;
        }
    }
}