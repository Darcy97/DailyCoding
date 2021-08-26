/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 上午11:02:36
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ResetPositionPerformer : IPerformer
    {
        private PerformConfig      _config;
        private Action<IPerformer> _finishCallback;
        private GameObject         _sender;

        public void Perform (PerformConfig config, AttackActionConfig attackActionConfig, Action<IPerformer>
            finishCallback,                        GameObject         sender,             bool canBreak)
        {
            _config         = config;
            _finishCallback = finishCallback;
            _sender         = sender;

            var moveControl = sender.GetComponent<MoveControl> ();

            if (moveControl == null)
            {
                moveControl = sender.AddComponent<MoveControl> ();
            }

            if (!moveControl.IsMoving || canBreak)
            {
                moveControl.Stop ();
                moveControl.MoveToOriginPosition (config.duration, OnMoveEnd);
            }
            else
            {
                OnMoveEnd ();
            }
        }

        private void OnMoveEnd ()
        {
            _finishCallback?.Invoke (this);
            // YieldUtils.DoEndOfFrame (_sender.GetComponent<MonoBehaviour> (), () => _finishCallback?.Invoke (this));
        }

        public PerformConfig GetPerformData () => _config;
    }
}