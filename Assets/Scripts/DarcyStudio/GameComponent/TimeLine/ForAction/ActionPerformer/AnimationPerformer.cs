/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:30:30
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class AnimationPerformer : IPerformer
    {
        private PerformConfig      _performConfig;
        private Action<IPerformer> _callback;
        private GameObject         _sender;

        public void Perform (PerformConfig config,         AttackActionConfig attackActionConfig,
            Action<IPerformer>             finishCallback, GameObject         sender, bool canBreak)
        {
            _performConfig = config;
            _sender        = sender;

            var superAnimator = sender.GetComponent<SuperAnimator> ();
            if (!superAnimator)
                superAnimator = sender.AddComponent<SuperAnimator> ();

            _callback = finishCallback;

            if (!superAnimator.IsPlaying || canBreak)
            {
                superAnimator.Stop ();
                superAnimator.Play (config.animationKey, OnPlayEnd);
            }
            else
            {
                OnPlayEnd ();
            }
        }

        private void OnPlayEnd ()
        {
            _callback?.Invoke (this);
        }

        public PerformConfig GetPerformData () => _performConfig;
    }
}