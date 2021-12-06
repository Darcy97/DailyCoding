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

        private Dictionary<ActionType, ActionPerformConfig> _performConfigsDict;

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
            _performConfigsDict = new Dictionary<ActionType, ActionPerformConfig> (actionConfigs.Length);

            foreach (var actionPerformConfig in actionConfigs)
            {
                if (_performConfigsDict.ContainsKey (actionPerformConfig.ActionType ()))
                {
                    Log.Error ($"Dont allow multi action with same type: {actionPerformConfig.ActionType ()} in \"{transform.GetPath ()}\"");
                    continue;
                }

                _performConfigsDict.Add (actionPerformConfig.ActionType (), actionPerformConfig);
            }
        }

        // private Action _finishCallback;

        private Dictionary<int, Action> _finishCallbacks = new Dictionary<int, Action> ();

        private int          _executeTag = 0;
        private ActionPlayer _player;

        public void Do (AttackActionConfigData attackActionConfigData, Action finishCallback = null)
        {
            _player?.Stop ();

            _player = new ActionPlayer (attackActionConfigData, _performConfigsDict, _statusOwner, finishCallback,
                GetComponent<IObject> ());
            _player.Play ();
            // _executeTag++;
            // // EndCallback ();
            // _finishCallbacks.Add (_executeTag, finishCallback);
            //
            // var actionInfo = attackActionConfigData.GetActionInfoByPreviousAction (_statusOwner.GetStatus ());
            // if (actionInfo.Equals (default))
            // {
            //     Log.Error ("No suitable action info for previous action ->{0}<-  in  ->{1}<-",
            //         _statusOwner.GetStatus (),
            //         transform.GetPath ());
            //     finishCallback?.Invoke ();
            //     return;
            // }
            //
            // var actionConfig = GetActionConfig (actionInfo.afterActionType);
            // if (actionConfig.Equals (default))
            // {
            //     Log.Error ("No suitable action info for ->{0}<-  in  ->{1}<-", actionInfo.afterActionType,
            //         transform.GetPath ());
            //     finishCallback?.Invoke ();
            //     return;
            // }
            //
            // Execute (actionConfig, true, _executeTag);

            // DoActionByConfig (actionConfig, actionInfo);
        }

        // private IExecutor GetExecutor ()
        // {
        //     // return new Executor ();
        //     return null;
        // }
        //
        // private ActionPerformConfig GetActionConfig (ActionType actionType)
        // {
        //     if (_actionInfoDict == null)
        //         return default;
        //     if (_actionInfoDict.ContainsKey (actionType))
        //         return _actionInfoDict[actionType];
        //
        //     return _actionInfoDict.ContainsKey (ActionType.Default) ? _actionInfoDict[ActionType.Default] : default;
        // }
        //
        // private void OnExecuteEnd (IExecutor executor)
        // {
        //     var config         = executor.GetConfig ();
        //     var nextActionType = config.GetNextActionType ();
        //     var next           = GetActionConfig (nextActionType);
        //     if (next.Equals (default))
        //     {
        //         EndCallback (executor.Tag ());
        //         return;
        //     }
        //
        //     Execute (next, executor.Tag () == _executeTag, executor.Tag ());
        // }
        //
        // private void Execute (ActionPerformConfig config, bool canBreak, int executeTag)
        // {
        //     var executor = GetExecutor ();
        //     executor.Execute (config, OnExecuteEnd, GetComponent<IObject> (), canBreak);
        //
        //     _statusOwner.SetStatus (config.ActionType ());
        //
        //     if (config.WaitDone ())
        //         return;
        //     EndCallback (executeTag);
        // }
        //
        // private void EndCallback (int tag)
        // {
        //     if (_finishCallbacks.ContainsKey (tag))
        //     {
        //         _finishCallbacks[tag]?.Invoke ();
        //         _finishCallbacks.Remove (tag);
        //     }
        // }
    }


    [Serializable]
    public class ActionTypeInfo
    {
        public ActionType ActionType;
        public string     ActionID;
    }
}