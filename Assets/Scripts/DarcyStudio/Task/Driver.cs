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
            YieldUtils.StopCoroutine (_executeCoroutine);
        }

        private IEnumerator ExecuteOneByOne ()
        {
            while (_enumerator.MoveNext ())
            {
                var task = _enumerator.Current;

                if (task == null)
                    continue;

                Log.Info ($"<color=cyan>### Task System ### --- Execute task: {task.GetType ()}</color>");

                task.Execute ();

                while (task.IsBlock () && !task.IsFinish ())
                {
                    yield return YieldUtils.WaitForEndOfFrame;
                }

                if (!task.InterruptSubsequent ())
                    continue;

                Log.Info ($"<color=red>### Task System ### --- Interrupt by task: {task.GetType ()}</color>");
                break;
            }

            _enumerator.Dispose ();
            _callBack?.Invoke ();
        }

        public void Dispose ()
        {
            Stop ();
            _enumerator.Dispose ();
        }
    }
}