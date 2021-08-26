/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午7:02:24
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class MovePerformer : IPerformer
    {
        private PerformConfig      _config;
        private Action<IPerformer> _finishCallback;
        private GameObject         _sender;

        public void Perform (PerformConfig config,         AttackActionConfig attackActionConfig,
            Action<IPerformer>             finishCallback, GameObject         sender, bool canBreak)
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
                var v = config.moveVelocity * attackActionConfig.k0;
                moveControl.Move (v, config.moveAcceleration, OnMoveEnd);
            }
            else
            {
                OnMoveEnd ();
            }
        }

        private void OnMoveEnd ()
        {
            //延迟一帧执行
            _finishCallback?.Invoke (this);
        }

        public PerformConfig GetPerformData () => _config;
    }
}