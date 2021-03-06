/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:43:33
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class InvalidPerformer : IPerformer
    {

        private PerformConfig _config;

        public void Perform (PerformConfig config,         AttackActionConfig attackActionConfig,
            Action<IPerformer>             finishCallback, GameObject         sender, bool canBreak)
        {
            _config = config;
            Log.Error ("Please set action type");
            if (_config.waitDone)
                finishCallback?.Invoke (this);
        }

        public PerformConfig GetPerformData () => _config;

        public static InvalidPerformer Default = new InvalidPerformer ();
    }
}