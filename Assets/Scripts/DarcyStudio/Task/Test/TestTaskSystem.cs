/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:47:43
 ***/

using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.Task.Test
{
    public class TestTaskSystem : MonoBehaviour
    {
        private void OnEnable ()
        {
            var factory = new LogTaskFactory ();
            var tasks = new List<ITask>
                        {
                            factory.Create (LogTaskType.LogHi),
                            factory.Create (LogTaskType.Default),
                            factory.Create (LogTaskType.LogHello),
                            factory.Create (LogTaskType.Wait3Seconds),
                            factory.Create (LogTaskType.LogGoodbye)
                        };

            var driver = TaskUtils.CreateDriver (tasks.GetEnumerator (), OnDriverExecuteEnd);
            driver.Execute ();
        }

        private static void OnDriverExecuteEnd ()
        {
            Log.Info ("Driver execute end");
        }
    }
}