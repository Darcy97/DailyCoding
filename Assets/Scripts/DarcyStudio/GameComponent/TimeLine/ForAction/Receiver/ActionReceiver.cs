/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:17:08
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.State;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class ActionReceiver : MonoBehaviour, IActionReceiver
    {

        [SerializeField] private ActionPerformConfig[] actionConfigs;
        private                  SuperAnimator         _animator;

        private Dictionary<ActionType, ActionPerformConfig> _actionInfoDict;

        private IActionStatusOwner _statusOwner;
        // private Dictionary<string, ActionPerformConfig>     _customActionInfoDict;

        private void Start ()
        {
            _statusOwner = GetComponent<IActionStatusOwner> ();
            InitActionConfigDict ();
        }

        private void InitActionConfigDict ()
        {
            if (actionConfigs == null || actionConfigs.Length < 1)
                return;
            _actionInfoDict = new Dictionary<ActionType, ActionPerformConfig> (actionConfigs.Length);

            foreach (var actionPerformConfig in actionConfigs)
            {
                if (_actionInfoDict.ContainsKey (actionPerformConfig.GetActionType ()))
                {
                    Log.Error ("Dont allow multi action with same type: {0} in \"{1}\"",
                        actionPerformConfig.GetActionType (), transform.GetPath ());
                    continue;
                }

                _actionInfoDict.Add (actionPerformConfig.GetActionType (), actionPerformConfig);
            }
        }

        private Action _finishCallback;

        public void Do (ActionData actionData, Action finishCallback = null)
        {
            _finishCallback = finishCallback;

            var actionInfo = actionData.GetActionInfoByPreviousAction (_statusOwner.GetStatus ());
            if (actionInfo == null)
            {
                Log.Error ("No suitable action info for previous action ->{0}<-  in  ->{1}<-",
                    _statusOwner.GetStatus (),
                    transform.GetPath ());
            }

            var actionConfig = GetActionConfig (actionInfo.afterActionType);
            if (actionConfig == null)
            {
                Log.Error ("No suitable action info for ->{0}<-  in  ->{1}<-", actionInfo.afterActionType,
                    transform.GetPath ());
                finishCallback?.Invoke ();
                return;
            }

            actionConfig.SetActionInfo (actionInfo);
            Execute (actionConfig);

            // DoActionByConfig (actionConfig, actionInfo);
        }

        private IExecutor GetExecutor ()
        {
            return new Executor ();
        }

        private ActionPerformConfig GetActionConfig (ActionType actionType)
        {
            if (_actionInfoDict == null)
                return null;
            if (_actionInfoDict.ContainsKey (actionType))
                return _actionInfoDict[actionType];

            return _actionInfoDict.ContainsKey (ActionType.Default) ? _actionInfoDict[ActionType.Default] : null;
        }

        private void OnExecuteEnd (IExecutor executor)
        {
            var config         = executor.GetConfig ();
            var nextActionType = config.GetNextActionType ();
            var next           = GetActionConfig (nextActionType);
            if (next == null)
            {
                EndCallback ();
                return;
            }

            next.SetActionInfo (ActionInfo.Default);
            Execute (next);
        }

        private void Execute (ActionPerformConfig config)
        {
            var executor = GetExecutor ();
            executor.Execute (config, OnExecuteEnd, GetComponent<IObject> ());

            _statusOwner.SetStatus (config.actionType);

            if (config.waitDone)
                return;
            EndCallback ();
        }

        private void EndCallback ()
        {
            _finishCallback?.Invoke ();
            _finishCallback = null;
        }
    }


    [Serializable]
    public class ActionTypeInfo
    {
        public ActionType ActionType;
        public string     ActionID;
    }
}