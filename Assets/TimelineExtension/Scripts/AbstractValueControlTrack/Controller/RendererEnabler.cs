using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class RendererEnabler : AbstractBoolValueController
    {
        private Renderer[] renderers;

        [SerializeField] private bool initializeValue = false;
        [SerializeField] private int customDelayMs = 0; //Enable Disableを遅らせる時間
        [SerializeField] public UnityEvent<bool> OnStateChanged; //enable,disableが切り替わったときに呼ばれる

        private bool isFirst;
        private bool prevState;

        private void Start()
        {
            isFirst = true;

            GetRenderers();
            prevState = initializeValue;
            SetEnable(initializeValue);
        }

        protected override void OnValueInput(bool value)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying && renderers == null)
            {
                GetRenderers();
            }
#endif
            if (renderers == null)
            {
                GetRenderers();
            }

            SetEnable(value);
        }

        public void SetEnable(bool enable)
        {
            if (prevState != enable || isFirst)
            {
                if (!isFirst)
                {
                    OnStateChanged?.Invoke(enable);
                }

                prevState = enable;
                isFirst = false;

                Debug.Log($"RendererEnabler: name={gameObject.name} state={enable}");
                
#if UNITY_EDITOR
                if (!EditorApplication.isPlaying)
                {
                    foreach (var renderer in renderers)
                    {
                        renderer.enabled = enable;
                    }
                }
                else
                {
                    if (customDelayMs <= 0)
                    {
                        foreach (var renderer in renderers)
                        {
                            renderer.enabled = enable;
                        }
                    }
                    else
                    {
                        StartCoroutine(DelaySetEnable(enable, customDelayMs));
                    }
                }
#else
            if (customDelayMs <= 0)
            {
                foreach (var renderer in renderers)
                {
                    renderer.enabled = enable;
                }
            }
            else
            {
                StartCoroutine(DelaySetEnable(enable, customDelayMs));
            }
#endif
            }
        }

        private IEnumerator DelaySetEnable(bool enable, int customDelayMs)
        {
            yield return new WaitForSeconds((float)customDelayMs / 1000f);

            foreach (var renderer in renderers)
            {
                renderer.enabled = enable;
            }
        }

        [ContextMenu("EnableRenderers")]
        public void EnableRenderers()
        {
            SetEnable(true);
        }

        [ContextMenu("DisableRenderers")]
        public void DisableRenderers()
        {
            SetEnable(false);
        }

        public void GetRenderers()
        {
            Debug.Log($"GetRenderers : root={gameObject.name}");
            renderers = GetComponentsInChildren<Renderer>();

            prevState = renderers[0].enabled;
        }
    }
}