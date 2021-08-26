/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:26:58
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public interface IPerformer
    {

        /// FinishCallback 必须要调用
        void Perform (PerformConfig config, AttackActionConfig attackActionConfig, Action<IPerformer> finishCallback,
            GameObject              sender, bool               canBreak);

        PerformConfig GetPerformData ();
    }
}