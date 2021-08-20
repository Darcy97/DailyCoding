/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:41:07
 ***/

using System;
using UnityEngine;
using Object = UnityEngine.Object;
using YieldUtils = DarcyStudio.GameComponent.Tools.YieldUtils;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ShowGoPerformer : IResponsePerformer
    {

        private ResponseData _data;

        public void Perform (ResponseData data, Action finishCallback, GameObject sender)
        {
            _data = data;
            var go = Object.Instantiate (data.GO, sender.transform);
            go.transform.localPosition = Vector3.zero;
            YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (), () =>
            {
                Object.Destroy (go);
                if (data.WaitDone)
                    finishCallback?.Invoke ();
            }, data.showTime);
        }

        public ResponseData GetResponseData () => _data;
    }
}