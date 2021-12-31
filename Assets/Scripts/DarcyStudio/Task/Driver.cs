/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 14:51:44
 * Description: 任务驱动器 驱动任务执行
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

        public Driver (IEnumerator<ITask> enumerator, ExecuteFinish callBack)
        {
            _enumerator = enumerator;
            _callBack   = callBack;
        }

        private Coroutine _executeCoroutine;

        public void Execute ()
        {
            _executeCoroutine = YieldUtils.StartCoroutine (ExecuteOneByOne ());
        }

        public void Stop ()
        {
            if (_executeCoroutine == null)
                return;

            YieldUtils.StopCoroutine (_executeCoroutine);
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

            _enumerator.Dispose ();
            _callBack?.Invoke ();
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
            Stop ();
            _enumerator.Dispose ();
        }
    }
}