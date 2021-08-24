/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午2:33:29
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class Executor : IExecutor
    {
        private ActionPerformConfig _config;
        private Action<IExecutor>   _finishCallback;
        private IObject             _sender;
        private int                 _waitDoneCount;

        public void Execute (ActionPerformConfig config, Action<IExecutor> finishCallback, IObject sender)
        {
            if (_waitDoneCount > 0)
            {
                Log.Error ("Logic error");
                return;
            }

            _config         = config;
            _finishCallback = finishCallback;
            _sender         = sender;

            var performDatas = config.GetPerforms ();

            _waitDoneCount = 0;

            foreach (var performData in performDatas)
            {
                if (performData.waitDone)
                    _waitDoneCount++;
            }

            foreach (var performData in performDatas)
            {
                performData.ActionInfo = config.GetActionInfo ();
                ExecuteByPerformData (performData);
            }

            if (_waitDoneCount > 0)
                return;

            EndCallback ();
        }

        public ActionPerformConfig GetConfig () => _config;

        private void ExecuteByPerformData (PerformData data)
        {
            var performer = data.Performer;
            if (performer == null)
            {
                performer      = GetPerformer (data.performType);
                data.Performer = performer;
            }

            performer.Perform (data, OnPerformEnd, _sender.GetGameObject ());
        }

        private void OnPerformEnd (IPerformer performer)
        {
            var performData = performer.GetPerformData ();
            if (performData.waitDone)
            {
                _waitDoneCount--;
            }

            if (_waitDoneCount > 0)
                return;

            EndCallback ();
        }

        private void EndCallback ()
        {
            _finishCallback?.Invoke (this);
            _finishCallback = null;
        }

        private static IPerformer GetPerformer (PerformType type)
        {
            switch (type)
            {
                case PerformType.Default:
                    return InvalidPerformer.Default;
                case PerformType.Animation:
                    return new AnimationPerformer ();
                case PerformType.ShowGo:
                    return new ShowGoPerformer ();
                case PerformType.Move:
                    return new MovePerformer ();
                default:
                    return InvalidPerformer.Default;
            }
        }
    }
}