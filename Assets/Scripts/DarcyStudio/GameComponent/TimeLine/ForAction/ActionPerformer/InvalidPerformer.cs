/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:43:33
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class InvalidPerformer : IResponsePerformer
    {

        private ResponseData _data;

        public void Perform (ResponseData data, Action finishCallback, GameObject sender)
        {
            _data = data;
            Log.Error ("Please set action type");
            if (_data.WaitDone)
                finishCallback?.Invoke ();
        }

        public ResponseData GetResponseData () => _data;

        public static InvalidPerformer Default = new InvalidPerformer ();
    }
}