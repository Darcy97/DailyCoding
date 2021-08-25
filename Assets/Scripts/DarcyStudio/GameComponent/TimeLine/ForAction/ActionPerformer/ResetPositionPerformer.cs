/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 上午11:02:36
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ResetPositionPerformer : IPerformer
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
            moveControl.MoveToOriginPosition (data.duration, OnMoveEnd);
        }

        private void OnMoveEnd ()
        {
            _finishCallback?.Invoke (this);
        }

        public PerformData GetPerformData () => _data;
    }
}