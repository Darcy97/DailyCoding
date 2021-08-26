/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午12:10:36
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.State;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer
{
    public class ActionPlayer
    {

        private readonly AttackActionConfigData                      _attackActionConfigData;
        private readonly Dictionary<ActionType, ActionPerformConfig> _performConfigs;
        private readonly IActionStatusOwner                          _statusOwner;
        private readonly IObject                                     _sender;

        private Action _endCallback;


        public ActionPlayer (AttackActionConfigData     configData,
            Dictionary<ActionType, ActionPerformConfig> performConfigs,
            IActionStatusOwner                          statusOwner, Action endCallback, IObject sender)
        {
            _attackActionConfigData = configData;
            _performConfigs         = performConfigs;
            _statusOwner            = statusOwner;
            _endCallback            = endCallback;
            _sender                 = sender;
        }

        private bool               _isPlaying;
        private AttackActionConfig _attackActionConfig;

        public void Play ()
        {
            _isPlaying = true;

            _attackActionConfig = _attackActionConfigData.GetActionInfoByPreviousAction (_statusOwner.GetStatus ());
            if (_attackActionConfig.Equals (default (AttackActionConfig)))
            {
                Log.Error ("No suitable action info for previous action ->{0}<-  in {1}",
                    _statusOwner.GetStatus (), _sender.GetTransform ().GetPath ());
                _endCallback?.Invoke ();
                _endCallback = null;
                return;
            }

            var selfActionConfig = GetSelfActionConfig (_attackActionConfig.afterActionType);
            if (selfActionConfig.Equals (default (ActionPerformConfig)))
            {
                Log.Error ("No suitable action info for ->{0}<-  in  ->{1}<-", _attackActionConfig.afterActionType,
                    _sender.GetTransform ().GetPath ());
                EndCallback ();
                return;
            }

            Execute (selfActionConfig, true);
        }

        private ActionPerformConfig GetSelfActionConfig (ActionType actionType)
        {
            if (_performConfigs == null)
                return default;
            if (_performConfigs.ContainsKey (actionType))
                return _performConfigs[actionType];

            return _performConfigs.ContainsKey (ActionType.Default) ? _performConfigs[ActionType.Default] : default;
        }

        private void OnExecuteEnd (IExecutor executor)
        {
            if (!_isPlaying)
                return;

            var config         = executor.GetConfig ();
            var nextActionType = config.GetNextActionType ();
            var next           = GetSelfActionConfig (nextActionType);
            if (next.Equals (default (ActionPerformConfig)))
            {
                EndCallback ();
                return;
            }

            Execute (next, true);
        }

        private IExecutor _curExecutor;

        private void Execute (ActionPerformConfig config, bool canBreak)
        {
            _curExecutor = GetExecutor (_attackActionConfig);
            _curExecutor.Execute (config, OnExecuteEnd, _sender, canBreak);

            _statusOwner.SetStatus (config.ActionType ());

            if (config.WaitDone ())
                return;
            EndCallback ();
        }

        private void EndCallback ()
        {
            _endCallback?.Invoke ();
            _endCallback = null;
        }

        private IExecutor GetExecutor (AttackActionConfig attackConfig)
        {
            return new Executor (attackConfig);
        }

        public void Stop ()
        {
            _isPlaying = false;
            EndCallback ();
        }


    }
}