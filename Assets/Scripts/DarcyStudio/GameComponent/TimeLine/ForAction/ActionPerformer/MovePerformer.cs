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
        private PerformData _data;

        public void Perform (PerformData data, Action<IPerformer> finishCallback, GameObject sender)
        {
            _data = data;

            var moveControl = sender.GetComponent<MoveControl> ();

            if (moveControl == null)
            {
                // Log.Error ("No move control in --> {0} use default", sender.transform.GetPath ());
                moveControl = sender.AddComponent<MoveControl> ();
            }

            //TODO:处理完成回调
            Log.Error ("处理移动完成的回调");
            moveControl.Stop ();
            moveControl.Move (data.moveVelocity, data.moveAcceleration);
        }

        public PerformData GetPerformData () => _data;
    }
}