/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:30:30
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class AnimationPerformer : IResponsePerformer
    {
        private ResponseData _responseData;

        public void Perform (ResponseData data, Action finishCallback, GameObject sender)
        {
            _responseData = data;
            var superAnimator = sender.GetComponent<SuperAnimator> ();
            if (!superAnimator)
                superAnimator = sender.AddComponent<SuperAnimator> ();

            if (data.DelayTime > 0)
            {
                YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (),
                    () => { superAnimator.Play (data.AnimationKey, data.WaitDone ? finishCallback : null); },
                    data.DelayTime);
            }
            else
                superAnimator.Play (data.AnimationKey, data.WaitDone ? finishCallback : null);
        }

        public ResponseData GetResponseData () => _responseData;
    }
}