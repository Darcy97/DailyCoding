/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:25:06
 ***/

// #define DEBUG_LOG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.SequenceTaskWithUniTask
{
    public class Driver : IDisposable
    {

        public delegate void ExecuteFinish ();

        private readonly IEnumerator<ISequenceTask> _taskEnumerator;
        private readonly ExecuteFinish              _callback;

        private bool _isExecuting;
        private bool _isOver;

        private readonly CancellationTokenSource _cancellation;

        public Driver (IEnumerator<ISequenceTask> taskEnumerator, ExecuteFinish callback)
        {
            _taskEnumerator = taskEnumerator;
            _callback       = callback;
            _cancellation   = new CancellationTokenSource ();
        }

        #region Interface

        public void Execute ()
        {
            if (_isOver)
            {
                Log.Error ("已经执行完毕 ！！！不支持多次执行");
                return;
            }

            if (_isExecuting)
            {
                Log.Error ("正在执行 ！！！不支持多次执行");
                return;
            }

            _isExecuting = true;
            StartExecute ().Forget ();
        }

        public void Kill (bool executeCallback = false)
        {
            if (_isOver)
                return;

            if (_isExecuting)
            {
                _isExecuting = false;
                StopExecute ();
            }

            End ();
            
            if(executeCallback)
                _callback?.Invoke ();
        }

        public bool IsExecuting ()
        {
            return _isExecuting;
        }

        #endregion

        private async UniTaskVoid StartExecute ()
        {
            await ExecuteOneByOne ();
            End ();
            _callback?.Invoke ();
        }

        // ReSharper disable once PossibleNullReferenceException
        private async UniTask ExecuteOneByOne ()
        {
            while (_taskEnumerator.MoveNext ())
            {
                var task = _taskEnumerator.Current;

                if (CheckNull (task))
                    continue;

                LogExecute (task);

                await task.Execute ().AttachExternalCancellation (_cancellation.Token);

                if (!task.InterruptSubsequent ())
                    continue;

                LogInterrupt (task);
                break;
            }
        }

        private void End ()
        {
            _isOver      = true;
            _isExecuting = false;
            _taskEnumerator.Dispose ();
            _cancellation.Dispose ();
        }

        private void StopExecute ()
        {
            _cancellation.Cancel ();
        }

        private static bool CheckNull (ISequenceTask task)
        {
            if (task != null)
                return false;
            Log.Warning ("Input a null task, ignore");
            return true;
        }

        [Conditional("DEBUG_LOG")]
        private static void LogExecute (ISequenceTask task)
        {
            Log.Info ($"<color=cyan>### Task System ### --- Execute task: {task.GetType ()}</color>");
        }

        [Conditional("DEBUG_LOG")]
        private static void LogInterrupt (ISequenceTask task)
        {
            Log.Info ($"<color=red>### Task System ### --- Interrupt by task: {task.GetType ()}</color>");
        }

        public void Dispose ()
        {
            if (_isExecuting)
            {
                Log.Error ("<color=red>### Task System ### --- Is executing</color>");
                return;
            }

            _taskEnumerator.Dispose ();
            _cancellation.Dispose ();
        }
    }
}