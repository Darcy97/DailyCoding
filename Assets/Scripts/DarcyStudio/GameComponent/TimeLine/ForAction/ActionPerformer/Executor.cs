/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午2:33:29
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class Executor : IExecutor
    {
        private ActionPerformConfig _config;
        private Action<IExecutor>   _finishCallback;
        private IObject             _sender;
        private int                 _waitDoneCount;

        private int _tag;

        private AttackActionConfig _attackActionConfig;

        private bool _isPlaying;

        public Executor (AttackActionConfig attackConfig)
        {
            _attackActionConfig = attackConfig;
        }

        public void SetTag (int tag)
        {
            _tag = tag;
        }

        public int Tag () => _tag;

        public void Execute (ActionPerformConfig config, Action<IExecutor> finishCallback, IObject sender,
            bool                                 canBreak)
        {
            _isPlaying = true;

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
                ExecuteByPerformData (performData, canBreak);
            }

            if (_waitDoneCount > 0)
                return;

            EndCallback ();
        }

        public void Stop ()
        {
            _isPlaying = false;
        }

        public ActionPerformConfig GetConfig () => _config;

        private void ExecuteByPerformData (PerformConfig config, bool canBreak)
        {
            var performer = GetPerformer (config.performType);
            performer.Perform (config, _attackActionConfig, OnPerformEnd, _sender.GetGameObject (), canBreak);
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
            YieldUtils.DoEndOfFrame (_sender.GetComponent<MonoBehaviour> (), () =>
            {
                _finishCallback?.Invoke (this);
                _finishCallback = null;
            });
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
                case PerformType.ResetPosition:
                    return new ResetPositionPerformer ();
                case PerformType.Wait:
                    return new WaitPerformer ();
                default:
                    return InvalidPerformer.Default;
            }
        }
    }
}