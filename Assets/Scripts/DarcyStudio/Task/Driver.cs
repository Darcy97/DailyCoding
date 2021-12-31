/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 14:51:44
 * Description: 任务驱动器
 ***/

using System;
using System.Collections;
using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.Task
{
    public class Driver : IDisposable
    {

        public delegate void ExecuteFinish ();

        private readonly IEnumerator<ITask> _enumerator;
        private readonly ExecuteFinish      _callBack;
        private readonly bool               _disposable;

        private Coroutine _executeCoroutine;
        private bool      _isExecuting;

        /// <param name="callBack"></param>
        /// <param name="disposable">一次性的, 为 true 运行一次自动释放, 不可反复运行, 为 false 时需要自己手动释放</param>
        /// <param name="enumerator">任务迭代器, Driver 未执行完请勿手动释放, 默认情况下 Driver 运行完毕会自动释放</param>
        public Driver (IEnumerator<ITask> enumerator, ExecuteFinish callBack, bool disposable = true)
        {
            _enumerator = enumerator;
            _callBack   = callBack;
            _disposable = disposable;
        }

        #region Interface
        
        public void Execute ()
        {
            if (_isExecuting)
                return;

            _isExecuting = true;

            StartExecute ();
        }

        public void Restart ()
        {
            if (_isExecuting)
                StopExecute ();

            _isExecuting = true;
            _enumerator.Reset ();
            StartExecute ();
        }

        public void Pause ()
        {
            if (!_isExecuting)
                return;

            _isExecuting = false;

            StopExecute ();
        }

        public void Resume ()
        {
            if (_isExecuting)
                return;

            _isExecuting = true;

            StartExecute ();
        }

        public void Kill ()
        {
            if (_isExecuting)
            {
                _isExecuting = false;
                StopExecute ();
            }

            _enumerator.Dispose ();
        }

        public bool IsExecuting ()
        {
            return _isExecuting;
        }
        
        #endregion

        private void StartExecute ()
        {
            _executeCoroutine = YieldUtils.StartCoroutine (ExecuteOneByOne ());
        }

        private IEnumerator ExecuteOneByOne ()
        {
            while (_enumerator.MoveNext ())
            {
                var task = _enumerator.Current;

                if (task == null)
                    continue;

                LogExecute (task.GetType ());

                task.Execute ();

                while (task.IsBlock () && !task.IsFinish ())
                {
                    yield return YieldUtils.WaitForEndOfFrame;
                }

                if (!task.InterruptSubsequent ())
                    continue;

                LogInterrupt (task.GetType ());
                break;
            }

            if (_disposable)
                _enumerator.Dispose ();
            _callBack?.Invoke ();
            _isExecuting = false;
        }

        private void StopExecute ()
        {
            if (_executeCoroutine == null)
                return;

            YieldUtils.StopCoroutine (_executeCoroutine);
            _executeCoroutine = null;
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
                StopExecute ();

            _enumerator.Dispose ();
        }
    }
}