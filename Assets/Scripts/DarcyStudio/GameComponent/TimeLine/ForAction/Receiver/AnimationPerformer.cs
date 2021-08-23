/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:30:30
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class AnimationPerformer : IResponsePerformer
    {
        private PerformData _performData;

        public void Perform (PerformData data, Action finishCallback, GameObject sender)
        {
            _performData = data;
            var superAnimator = sender.GetComponent<SuperAnimator> ();
            if (!superAnimator)
                superAnimator = sender.AddComponent<SuperAnimator> ();

            // if (data.delayTime > 0)
            // {
            //     YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (),
            //         () => { superAnimator.Play (data.animationKey, data.waitDone ? finishCallback : null); },
            //         data.delayTime);
            // }
            // else
            superAnimator.Play (data.animationKey, data.waitDone ? finishCallback : null);
        }

        public PerformData GetResponseData () => _performData;
    }
}