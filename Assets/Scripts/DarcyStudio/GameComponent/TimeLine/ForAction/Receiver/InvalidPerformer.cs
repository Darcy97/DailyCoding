/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:43:33
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class InvalidPerformer : IResponsePerformer
    {

        private PerformData _data;

        public void Perform (PerformData data, Action finishCallback, GameObject sender)
        {
            _data = data;
            Log.Error ("Please set action type");
            if (_data.waitDone)
                finishCallback?.Invoke ();
        }

        public PerformData GetResponseData () => _data;

        public static InvalidPerformer Default = new InvalidPerformer ();
    }
}