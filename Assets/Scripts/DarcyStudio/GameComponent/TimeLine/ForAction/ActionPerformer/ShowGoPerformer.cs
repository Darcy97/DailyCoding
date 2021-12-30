/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:41:07
 * TODO：处理创建的物体是否跟随角色移动
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ShowGoPerformer : IPerformer
    {

        private PerformConfig      _config;
        private GameObject         _go;
        private Action<IPerformer> _finishCallback;

        public void Perform (PerformConfig config,         AttackActionConfig attackActionConfig,
            Action<IPerformer>             finishCallback, GameObject         sender, bool canBreak)
        {
            _config         = config;
            _finishCallback = finishCallback;

            if (config.go == null)
            {
                Log.Error ("No GO in Perform data");
                finishCallback?.Invoke (this);
                return;
            }

            _go = Object.Instantiate (config.go, sender.transform);

            _go.transform.localPosition = Vector3.zero;

            OnShow ();
        }

        private void OnShow ()
        {
            if (_config.duration > 0)
                YieldUtils.DelayActionWithOutContext (_config.duration, OnDisappear);
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

        public PerformConfig GetPerformData () => _config;
    }
}