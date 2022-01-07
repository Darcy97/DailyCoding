/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:25:06
 ***/

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.SequenceTaskWithUniTask
{
    public class Driver : IDisposable
    {

        public delegate void ExecuteFinish ();

        private readonly IEnumerator<ISequenceTask> _taskEnumerator;
        private readonly ExecuteFinish              _callBack;

        private bool _isExecuting;
        private bool _isOver;

        private CancellationTokenSource _cancellation;

        public Driver (IEnumerator<ISequenceTask> taskEnumerator, ExecuteFinish callBack)
        {
            _taskEnumerator   = taskEnumerator;
            _callBack     = callBack;
            _cancellation = new CancellationTokenSource ();
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

        public void Kill ()
        {
            if (_isExecuting)
            {
                _isExecuting = false;
                StopExecute ();
            }

            End ();
        }

        public bool IsExecuting ()
        {
            return _isExecuting;
        }

        #endregion

        private async UniTaskVoid StartExecute ()
        {
            await ExecuteOneByOne ();
            _callBack?.Invoke ();
            End ();
        }

        private async UniTask ExecuteOneByOne ()
        {
            while (_taskEnumerator.MoveNext ())
            {
                var task = _taskEnumerator.Current;

                if (task == null)
                    continue;

                LogExecute (task.GetType ());

                await task.Execute ().AttachExternalCancellation (_cancellation.Token);

                if (!task.InterruptSubsequent ())
                    continue;

                LogInterrupt (task.GetType ());
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

        private static void LogExecute (Type taskType)
        {
            Log.Info ($"<color=cyan>### Task System ### --- Execute task: {taskType}</color>");
        }

        private static void LogInterrupt (Type taskType)
        {
            Log.Info ($"<color=red>### Task System ### --- Interrupt by task: {taskType}</color>");
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