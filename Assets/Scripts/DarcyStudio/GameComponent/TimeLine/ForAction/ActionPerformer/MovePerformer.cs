/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午7:02:24
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class MovePerformer : IPerformer
    {
        private PerformData        _data;
        private Action<IPerformer> _finishCallback;

        public void Perform (PerformData data, Action<IPerformer> finishCallback, GameObject sender)
        {
            _data           = data;
            _finishCallback = finishCallback;

            var moveControl = sender.GetComponent<MoveControl> ();

            if (moveControl == null)
            {
                moveControl = sender.AddComponent<MoveControl> ();
            }

            moveControl.Stop ();
            var v = data.moveVelocity * data.ActionInfo.k0;
            moveControl.Move (v, data.moveAcceleration, OnMoveEnd);
        }

        private void OnMoveEnd ()
        {
            _finishCallback?.Invoke (this);
        }

        public PerformData GetPerformData () => _data;
    }
}