using UnityEngine.Playables;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack
{
    public class AbstractIntValueControlMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var trackBinding = playerData as AbstractIntValueController;

            if (!trackBinding)
                return;

            var Value = 0;

            var inputCount = playable.GetInputCount();

            for (var i = 0; i < inputCount; i++)
            {
                var inputWeight = playable.GetInputWeight(i);

                if (inputWeight == 0f)
                {
                    continue;
                }
                
                var inputPlayable = (ScriptPlayable<AbstractIntValueControlBehaviour>)playable.GetInput(i);
                var inputBehaviour = inputPlayable.GetBehaviour();
                Value = inputBehaviour.Value;
                break;
            }

            //Apply
            trackBinding.SetValue(Value);
        }
    }
}