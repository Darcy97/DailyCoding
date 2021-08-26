/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午2:34:05
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public interface IExecutor
    {
        void Execute (ActionPerformConfig config, Action<IExecutor> finishCallback, IObject sender, bool canBreak);
        void Stop ();
        ActionPerformConfig GetConfig ();

        void SetTag (int tag);
        int  Tag ();
    }
}