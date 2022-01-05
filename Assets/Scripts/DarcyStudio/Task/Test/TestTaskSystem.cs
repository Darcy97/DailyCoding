/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Thursday, 30 December 2021
 * Time: 15:47:43
 ***/

using System.Collections.Generic;
using System.Text;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace DarcyStudio.Task.Test
{
    public class TestTaskSystem : MonoBehaviour
    {
        [SerializeField] private Text logContent;
        [SerializeField] private Text taskContent;

        private          Logger                            _logger;
        private          Driver                            _driver;
        private          ITaskFactory<LogTaskType, Logger> _taskFactory;
        private readonly List<ITask>                       _tasks = new List<ITask> ();

        public void AddInvalid ()
        {
            AddTask (LogTaskType.Invalid);
        }

        public void AddLogHello ()
        {
            AddTask (LogTaskType.LogHello);
        }

        public void AddLogHi ()
        {
            AddTask (LogTaskType.LogHi);
        }

        public void AddGoodBye ()
        {
            AddTask (LogTaskType.LogGoodbye);
        }

        public void AddWait3Seconds ()
        {
            AddTask (LogTaskType.Wait3Seconds);
        }

        public void AddInterrupt ()
        {
            AddTask (LogTaskType.Interrupt);
        }

        public void ResetTask ()
        {
            _tasks.Clear ();
            UpdateTaskPreview ();
        }

        private void AddTask (LogTaskType type)
        {
            var task = _taskFactory.Create (type, _logger);
            _tasks.Add (task);
            UpdateTaskPreview ();
        }

        private void UpdateTaskPreview ()
        {
            if (_tasks.Count < 1)
            {
                taskContent.text = "No Tasks ...";
                return;
            }

            var strBuilder = new StringBuilder ();
            foreach (var item in _tasks)
            {
                if (strBuilder.Length >= 1)
                    strBuilder.AppendLine ();
                strBuilder.Append (SubTypeString (item.GetType ().ToString ()));
            }

            taskContent.text = strBuilder.ToString ();
        }

        private string SubTypeString (string str)
        {
            if (!str.Contains ("."))
                return str.Replace ("Task", string.Empty);
            var index = str.LastIndexOf ('.');
            return str.Substring (index + 1).Replace ("Task", string.Empty);
        }

        private void Start ()
        {
            Log.Info ("Init");

            _logger = new Logger (logContent);
            _logger.AddLog ("Ready ...");
            UpdateTaskPreview ();
            _taskFactory = new LogTaskFactory ();
        }

        public void ResetLog ()
        {
            _logger.Clear ();
            _logger.AddLog ("Ready ...");
        }

        public void InitDriver ()
        {
            var enumerator = _tasks.GetEnumerator ();
            // enumerator.Dispose ();
            _driver = TaskUtils.CreateDriver (enumerator, OnDriverExecuteEnd, false);
        }

        public void StartExecuting ()
        {
            Log.Info ("Start");
            _driver.Execute ();
        }

        public void PauseExecuting ()
        {
            Log.Info ("Pause");
            _driver.Pause ();
        }

        public void ResumeExecuting ()
        {
            Log.Info ("Resume");
            _driver.Resume ();
        }

        public void Kill ()
        {
            Log.Info ("Kill");
            _driver.Kill ();
        }

        public void Restart ()
        {
            Log.Info ("Restart");

            if (!_driver.IsExecuting ())
                ResetLog ();

            _driver.Restart ();
        }

        private void OnDriverExecuteEnd ()
        {
            _logger.AddLog ("End");
        }
    }
}