/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:41:07
 * TODO：处理创建的物体是否跟随角色移动
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ShowGoPerformer : IPerformer
    {

        private PerformData        _data;
        private GameObject         _go;
        private Action<IPerformer> _finishCallback;

        public void Perform (PerformData data, Action<IPerformer> finishCallback, GameObject sender, bool canBreak)
        {
            _data           = data;
            _finishCallback = finishCallback;

            if (data.go == null)
            {
                Log.Error ("No GO in Perform data");
                finishCallback?.Invoke (this);
                return;
            }

            _go = Object.Instantiate (data.go, sender.transform);

            _go.transform.localPosition = Vector3.zero;

            OnShow ();
        }

        private void OnShow ()
        {
            if (_data.duration > 0)
                YieldUtils.DelayActionWithOutContext (OnDisappear, _data.duration);
            else
            {
                Log.Error ("Make sure you set the duration in perform data");
                OnDisappear ();
            }
        }

        private void OnDisappear ()
        {
            Object.Destroy (_go);
            _finishCallback?.Invoke (this);
        }

        public PerformData GetPerformData () => _data;
    }
}