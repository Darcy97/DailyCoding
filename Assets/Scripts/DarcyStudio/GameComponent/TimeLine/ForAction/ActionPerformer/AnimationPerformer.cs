/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:30:30
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class AnimationPerformer : IPerformer
    {
        private PerformData        _performData;
        private Action<IPerformer> _callback;

        public void Perform (PerformData data, Action<IPerformer> finishCallback, GameObject sender)
        {
            _performData = data;

            var superAnimator = sender.GetComponent<SuperAnimator> ();
            if (!superAnimator)
                superAnimator = sender.AddComponent<SuperAnimator> ();

            _callback = finishCallback;

            // if (data.delayTime > 0)
            // {
            //     YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (),
            //         () => { superAnimator.Play (data.animationKey, data.waitDone ? finishCallback : null); },
            //         data.delayTime);
            // }
            // else
            superAnimator.Play (data.animationKey, OnPlayEnd);
        }

        private void OnPlayEnd ()
        {
            _callback?.Invoke (this);
        }

        public PerformData GetPerformData () => _performData;
    }
}