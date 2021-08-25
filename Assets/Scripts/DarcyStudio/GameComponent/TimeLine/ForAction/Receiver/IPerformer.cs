/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:26:58
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public interface IPerformer
    {

        /// <summary>
        /// FinishCallback 必须要调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="finishCallback"></param>
        /// <param name="sender"></param>
        void Perform (PerformData data, Action<IPerformer> finishCallback, GameObject sender, bool canBreak);

        PerformData GetPerformData ();
    }
}