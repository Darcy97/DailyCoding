/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:17:08
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.State;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class ActionReceiver : MonoBehaviour, IActionReceiver, IActionStatusOwner
    {

        [SerializeField] private ActionPerformConfig[] actionConfigs;
        private                  SuperAnimator         _animator;

        private Dictionary<ActionType, ActionPerformConfig> _actionInfoDict;
        // private Dictionary<string, ActionPerformConfig>     _customActionInfoDict;

        private void Start ()
        {
            //TODO: 临时测试 这个IActionStatusOwner 应该有角色控制器实现
            SetStatus (ActionType.Idle);
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

            var actionInfo = actionData.GetActionInfoByPreviousAction (this.GetStatus ());

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

            next.SetActionInfo (null);
            Execute (next);
        }

        private void Execute (ActionPerformConfig config)
        {
            var executor = GetExecutor ();
            executor.Execute (config, OnExecuteEnd, GetComponent<IObject> ());

            SetStatus (config.actionType);

            if (config.waitDone)
                return;
            EndCallback ();
        }

        private void EndCallback ()
        {
            _finishCallback?.Invoke ();
            _finishCallback = null;
        }

        private ActionType curAction;
        public  ActionType GetStatus () => curAction;

        public void SetStatus (ActionType status)
        {
            curAction = status;
        }
    }

    [Serializable]
    public class ActionPerformConfig
    {

        public bool waitDone;

        public ActionType actionType;

        public PerformData[] performs;


        [SerializeField] private bool       specifyNextAction;
        [SerializeField] private ActionType nextAction = ActionType.None;

        public ActionType GetActionType ()
        {
            return actionType;
        }

        public PerformData[] GetPerforms ()
        {
            return performs;
        }

        public ActionPerformConfig GetNextAction ()
        {
            return GetDefaultNextAction (this);
        }

        public ActionType GetNextActionType ()
        {
            return specifyNextAction ? nextAction : GetNextActionType (this);
        }

        private ActionInfo _actionInfo; //从攻击方传来的

        public void SetActionInfo (ActionInfo actionInfo)
        {
            _actionInfo = actionInfo;
        }

        public ActionInfo GetActionInfo () => _actionInfo;


        public static ActionPerformConfig GetDefaultNextAction (ActionPerformConfig preConfig)
        {
            //TODO 这里写死一个默认的规则
            switch (preConfig.actionType)
            {
                case ActionType.Default:
                    break;
                case ActionType.Custom:
                    break;
                case ActionType.Idle:
                    break;
                case ActionType.Back:
                    return new ActionPerformConfig ()
                           {
                               waitDone   = false,
                               actionType = ActionType.Idle,
                               performs   = new[] {PerformData.Animation ("idle", false)}
                           };

                    break;
                case ActionType.Fall:
                    break;
                case ActionType.KnockFly:
                    break;
                case ActionType.Floating:
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }

            return null;
        }

        public static ActionType GetNextActionType (ActionPerformConfig preConfig)
        {
            var result = ActionType.None;
            switch (preConfig.actionType)
            {
                case ActionType.None:
                    break;
                case ActionType.Custom:
                    break;
                case ActionType.Idle:
                    break;
                case ActionType.Back:
                    result = ActionType.Idle;
                    break;
                case ActionType.Fall:
                    result = ActionType.GetUp;
                    break;
                case ActionType.KnockFly:
                    result = ActionType.Fall;
                    break;
                case ActionType.Floating:
                    result = ActionType.Fall;
                    break;
                case ActionType.Default:
                    break;
                case ActionType.GetUp:
                    result = ActionType.Idle;
                    break;
                default:
                    result = ActionType.None;
                    break;
            }

            return result;
        }
    }


    [Serializable]
    public class ActionTypeInfo
    {
        public ActionType ActionType;
        public string     ActionID;
    }
}