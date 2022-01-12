/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Monday, 10 January 2022
 * Time: 15:22:34
 ***/

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DarcyStudio.GameComponent.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace DarcyStudio.SequenceTaskWithUniTask
{
    public static class UnitTest
    {

        [MenuItem ("UnitTest/SequenceTask")]
        public static void Test ()
        {
            DoTest ().Forget ();
        }

        private static async UniTaskVoid DoTest ()
        {
            await DoTestBaseAndCancel ();
            await DoTestInterrupt ();
            await DoTestBaseAndCancelWithFinishCallback ();
            Log.Info ("Test finish");
        }

        private static async UniTask DoTestBaseAndCancel ()
        {
            var factory = new Factory ();
            var number  = new Number ();
            var tasks = new List<ISequenceTask>
                        {
                            factory.Create (TaskType.Add, number), // 1
                            factory.Create (TaskType.Sub, number), // 2
                            factory.Create (TaskType.Add, number), // 3
                            factory.Create (TaskType.Add, number), // 4
                            factory.Create (TaskType.Add, number)  // 5
                        };
            var driver = TaskUtils.CreateDriver (tasks.GetEnumerator (), () => { number.Value = 111; });

            number.Value = 0;

            driver.Execute ();

            // 1

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 2

            Assert.AreEqual (number.Value, 0, $"expect: 0 but: {number.Value}");

            Assert.IsTrue (driver.IsExecuting (), "driver.IsExecuting ()");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 3

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            driver.Kill ();

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 4

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            Assert.IsFalse (driver.IsExecuting (), "driver.IsExecuting ()");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 5

            Assert.IsFalse (driver.IsExecuting ());

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            Log.Info ("Test cancel finish: 没有报错就代表成功，这样还是不太好，有时间改一改");
        }


        private static async UniTask DoTestBaseAndCancelWithFinishCallback ()
        {
            var factory = new Factory ();
            var number  = new Number ();
            var tasks = new List<ISequenceTask>
                        {
                            factory.Create (TaskType.Add, number), // 1
                            factory.Create (TaskType.Sub, number), // 2
                            factory.Create (TaskType.Add, number), // 3
                            factory.Create (TaskType.Add, number), // 4
                            factory.Create (TaskType.Add, number)  // 5
                        };
            var driver = TaskUtils.CreateDriver (tasks.GetEnumerator (), () => { number.Value = 111; });

            number.Value = 0;

            driver.Execute ();

            // 1

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 2

            Assert.AreEqual (number.Value, 0, $"expect: 0 but: {number.Value}");

            Assert.IsTrue (driver.IsExecuting (), "driver.IsExecuting ()");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 3

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            driver.Kill (true);

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 4

            Assert.AreEqual (number.Value, 111, $"expect: 1 but: {number.Value}");

            Assert.IsFalse (driver.IsExecuting (), "driver.IsExecuting ()");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 5

            Assert.IsFalse (driver.IsExecuting ());

            Assert.AreEqual (number.Value, 111, $"expect: 1 but: {number.Value}");

            Log.Info ("Test cancel with callback finish ...: 没有报错就代表成功，这样还是不太好，有时间改一改");
        }


        private static async UniTask DoTestInterrupt ()
        {
            var factory = new Factory ();
            var number  = new Number ();
            var tasks = new List<ISequenceTask>
                        {
                            factory.Create (TaskType.Add,              number), // 1
                            factory.Create (TaskType.Sub,              number), // 2
                            factory.Create (TaskType.AddThenInterrupt, number), // 3
                            factory.Create (TaskType.Add,              number), // 4
                            factory.Create (TaskType.Add,              number)  // 5
                        };
            var driver = TaskUtils.CreateDriver (tasks.GetEnumerator (),
                () => { Log.Info ("Test interrupt finish: 没有报错就代表成功，这样还是不太好，有时间改一改"); });

            number.Value = 0;

            driver.Execute ();

            // 1

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 2

            Assert.IsTrue (driver.IsExecuting ());

            Assert.AreEqual (number.Value, 0, $"expect: 0 but: {number.Value}");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 3

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            Assert.IsTrue (driver.IsExecuting ());

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 4

            Assert.IsFalse (driver.IsExecuting ());

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");

            await UniTask.NextFrame (PlayerLoopTiming.PostLateUpdate);
            // 5

            Assert.AreEqual (number.Value, 1, $"expect: 1 but: {number.Value}");
        }
    }

    public class Number
    {
        public int Value;
    }

    public class AddOneTask : ISequenceTask
    {

        private readonly Number _number;

        public AddOneTask (Number number)
        {
            _number = number;
        }

        public async UniTask Execute ()
        {
            _number.Value += 1;
            await UniTask.NextFrame ();
        }

        public bool InterruptSubsequent () => false;
    }

    public class SubOneTask : ISequenceTask
    {

        private readonly Number _number;

        public SubOneTask (Number number)
        {
            _number = number;
        }

        public async UniTask Execute ()
        {
            _number.Value -= 1;
            await UniTask.NextFrame ();
        }

        public bool InterruptSubsequent () => false;
    }

    public class AddOneThenInterruptTask : ISequenceTask
    {
        private readonly Number _number;

        public AddOneThenInterruptTask (Number number)
        {
            _number = number;
        }

        public async UniTask Execute ()
        {
            _number.Value += 1;
            await UniTask.NextFrame ();
        }

        public bool InterruptSubsequent () => true;
    }

    public enum TaskType
    {
        Add,
        Sub,
        AddThenInterrupt
    }

    public class Factory : ITaskFactory<TaskType, Number>
    {

        public ISequenceTask Create (TaskType type, Number number)
        {
            switch (type)
            {
                case TaskType.Add:
                    return new AddOneTask (number);
                case TaskType.Sub:
                    return new SubOneTask (number);
                case TaskType.AddThenInterrupt:
                    return new AddOneThenInterruptTask (number);
                default:
                    throw new ArgumentOutOfRangeException (nameof (type), type, null);
            }
        }
    }
}