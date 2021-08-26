/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午4:43:57
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class WaitPerformer : IPerformer
    {

        private PerformConfig _config;

        public void Perform (PerformConfig config,         AttackActionConfig attackActionConfig,
            Action<IPerformer>             finishCallback, GameObject         sender,
            bool                           canBreak)
        {
            _config = config;

            YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (), () => { finishCallback?.Invoke (this); },
                attackActionConfig.waitTime * config.k0);
        }

        public PerformConfig GetPerformData () => _config;
    }
}