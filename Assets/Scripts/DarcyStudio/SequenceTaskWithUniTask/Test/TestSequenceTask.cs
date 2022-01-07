/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Friday, 07 January 2022
 * Time: 11:56:55
 ***/

using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.SequenceTaskWithUniTask.Test
{
    public class TestSequenceTask : MonoBehaviour
    {

        private Driver _driver;

        private void OnEnable ()
        {
            var tasks = new List<ISequenceTask> ();

            var factory = new LogTaskFactory ();

            tasks.Add (factory.Create (LogTaskType.Hello));
            tasks.Add (factory.Create (LogTaskType.Hello));
            tasks.Add (factory.Create (LogTaskType.Hello));
            tasks.Add (factory.Create (LogTaskType.Hi));
            tasks.Add (null);
            tasks.Add (factory.Create (LogTaskType.Hello));
            tasks.Add (factory.Create (LogTaskType.Hello));

            _driver = TaskUtils.CreateDriver (tasks.GetEnumerator (), () => Log.Info ("Finish"));
            _driver.Execute ();
        }

        private void OnDisable ()
        {
            Log.Info ($"Is executing: {_driver.IsExecuting ()}");
            _driver.Kill ();
        }
    }
}