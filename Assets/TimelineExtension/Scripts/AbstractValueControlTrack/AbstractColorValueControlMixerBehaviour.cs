using UnityEngine;
using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public class AbstractColorValueControlMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var trackBinding = playerData as AbstractColorValueController;

            if (!trackBinding)
                return;

            var color0 = Color.white;
            var color1 = Color.white;
            var weight = 0f;

            var inputCount = playable.GetInputCount();

            // var count = 0;
            for (var i = 0; i < inputCount; i++)
            {
                var inputWeight = playable.GetInputWeight(i);

                if (inputWeight == 0f)
                    continue;

                var inputPlayable0 = (ScriptPlayable<AbstractColorValueControlBehaviour>)playable.GetInput(i);
                var inputBehaviour = inputPlayable0.GetBehaviour();
                
                if (inputWeight == 1f)
                {
                    color0 = inputBehaviour.Value;
                    weight = 1f;
                }
                else
                {
                    color0 = inputBehaviour.Value;
                    var inputPlayable1 = (ScriptPlayable<AbstractColorValueControlBehaviour>)playable.GetInput(i + 1);
                    color1 = inputPlayable1.GetBehaviour().Value;
                    weight = inputWeight;
                }
                
                break;
            }

            //Apply
            trackBinding.SetValue(Color.Lerp(color0,color1, 1f - weight));
        }
    }
}