/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午5:41:07
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class ShowGoPerformer : IResponsePerformer
    {

        private PerformData _data;
        private GameObject  _go;
        private Action      _finishCallback;

        public void Perform (PerformData data, Action finishCallback, GameObject sender)
        {
            _data           = data;
            _finishCallback = finishCallback;

            if (data.go == null)
            {
                Log.Error ("No GO in Perform data");
                finishCallback?.Invoke ();
                return;
            }

            _go = Object.Instantiate (data.go, sender.transform);

            _go.transform.localPosition = Vector3.zero;

            // if (data.delayTime > 0)
            // {
            //     _go.SetActive (false);
            //     YieldUtils.DelayAction (sender.GetComponent<MonoBehaviour> (), OnShow,
            //         data.delayTime);
            // }
            // else
            // {
            OnShow ();
            // }
        }

        private void OnShow ()
        {
            // if (!_go.activeSelf)
            //     _go.SetActive (true);

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
            if (_data.waitDone)
                _finishCallback?.Invoke ();
        }

        public PerformData GetResponseData () => _data;
    }
}