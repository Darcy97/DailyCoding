/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 06 January 2022
 * Time: 12:12:54
 ***/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ExperienceOfSomePlugins.UniTask
{
    public class TestUniTask : MonoBehaviour, IProgress<float>
    {
        private CancellationTokenSource _cancellationResource;

        private void Awake ()
        {
            UniTaskScheduler.UnobservedTaskException += exception => { Log.Error ($"Exception: {exception.Message}"); };
            UniTaskScheduler.PropagateOperationCanceledException = true;
            UniTaskScheduler.UnobservedExceptionWriteLogType = LogType.Error;
        }

        public void Report (float value)
        {
            Log.Info (value.ToString (CultureInfo.InvariantCulture));
        }

        private async Cysharp.Threading.Tasks.UniTask WebRequest ()
        {
            var request = await UnityWebRequest.Get ("https://www.baidu.com")
                .SendWebRequest ()
                .ToUniTask (this); // pass this
            Debug.Log (request.downloadHandler.text);
        }

        public void OnClickStart ()
        {
            // StartAsyncLog ().Forget ();
            ForeachAsync ().Forget ();
        }


        [SerializeField] private Button button;

        private async UniTaskVoid TestButton ()
        {
            await button.OnClickAsync ();
            await button.OnClickAsync ();
            Log.Info ("Click twice");
        }

        private async UniTaskVoid ForeachAsync ()
        {
            _cancellationResource?.Dispose ();

            _cancellationResource = new CancellationTokenSource ();

            _cancellationResource.CancelAfterSlim (TimeSpan.FromSeconds (3));

            // 逐帧遍历
            var list = new List<int> {1, 2, 3, 4, 5}.GetEnumerator ();
            await UniTaskAsyncEnumerable.EveryUpdate ()
                .ForEachAsync (_ =>
                {
                    if (list.MoveNext ())
                    {
                        Log.Info ("Update() " + Time.frameCount);
                        Log.Info ($"Current: {list.Current}");
                    }
                    else
                    {
                        _cancellationResource.Cancel ();
                    }
                }, _cancellationResource.Token);
            list.Dispose ();
        }

        private async UniTaskVoid DoAsync ()
        {
            await WebRequest ();
            Log.Info ("Finish");
        }

        public void Cancel ()
        {
            if (_cancellationResource.Token.CanBeCanceled)
                _cancellationResource.Cancel ();
        }

        private void OnDestroy ()
        {
            _cancellationResource?.Dispose ();
        }

        private async UniTaskVoid StartAsyncLog ()
        {
            _cancellationResource?.Dispose ();
            _cancellationResource = new CancellationTokenSource ();

            var task = LogAsync ();
            // _cancellationResource.CancelAfterSlim (TimeSpan.FromSeconds (2));


            var tResult = await task;

            Log.Info ($"End: {tResult}");

            Log.Info ("End");

            // var (isCanceled, result) = await task.SuppressCancellationThrow ();
            // if (isCanceled)
            // return;

            // Log.Info ($"End: {result}");
        }

        private bool _isFinish;

        private async UniTaskVoid IsFinish ()
        {
            await Cysharp.Threading.Tasks.UniTask.WaitUntil (() => _isFinish);
        }

        private async UniTask<string> LogAsync ()
        {
            await Cysharp.Threading.Tasks.UniTask.DelayFrame (2, cancellationToken: _cancellationResource.Token);
            Log.Info ("Step one");

            await Cysharp.Threading.Tasks.UniTask.Delay (TimeSpan.FromSeconds (1),
                cancellationToken: _cancellationResource.Token);
            Log.Info ("Step two");

            await Cysharp.Threading.Tasks.UniTask.Delay (TimeSpan.FromSeconds (2),
                cancellationToken: _cancellationResource.Token);
            Log.Info ("Step three");

            return "end";
        }
    }
}