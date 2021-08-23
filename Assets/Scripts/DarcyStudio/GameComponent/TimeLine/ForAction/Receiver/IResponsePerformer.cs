/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:26:58
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public interface IResponsePerformer
    {
        void         Perform (PerformData data, Action finishCallback, GameObject sender);
        PerformData GetResponseData ();
    }
}