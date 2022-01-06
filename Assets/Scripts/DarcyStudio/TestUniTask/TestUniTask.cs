/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 06 January 2022
 * Time: 12:12:54
 ***/

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.TestUniTask
{
    public class TestUniTask : MonoBehaviour
    {
        private CancellationTokenSource _cancellationResource;

        private void Awake ()
        {
            _cancellationResource = new CancellationTokenSource ();
        }

        public void OnClickStart ()
        {
            StartAsync ();
        }

        public void Cancel ()
        {
            if (_cancellationResource.Token.CanBeCanceled)
                _cancellationResource.Cancel ();
        }

        private void OnDestroy ()
        {
            _cancellationResource.Dispose ();
        }

        private async void StartAsync ()
        {
            var task = DoAsync ();

            var (isCanceled, result) = await task.SuppressCancellationThrow ();
            if (isCanceled)
                return;

            Log.Info ($"End: {result}");
        }

        private async UniTask<string> DoAsync ()
        {
            await UniTask.DelayFrame (2, cancellationToken: _cancellationResource.Token);
            Log.Info ("Step one");

            await UniTask.Delay (TimeSpan.FromSeconds (2), cancellationToken: _cancellationResource.Token);
            Log.Info ("Step two");

            await UniTask.Delay (TimeSpan.FromSeconds (2), cancellationToken: _cancellationResource.Token);
            Log.Info ("Step three");

            return "end";
        }
    }
}