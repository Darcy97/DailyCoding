/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:17:08
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    public class ActionReceiver : MonoBehaviour, IActionReceiver
    {

        [SerializeField] private ActionPerformConfig[] actionConfigs;
        private                  SuperAnimator         _animator;

        private Dictionary<ActionType, ActionPerformConfig> _actionInfoDict;
        private Dictionary<string, ActionPerformConfig>     _customActionInfoDict;

        private void Start ()
        {
            // _animator = GetComponent<SuperAnimator> ();
            InitActionConfigDict ();
        }

        private void InitActionConfigDict ()
        {
            if (actionConfigs == null || actionConfigs.Length < 1)
                return;
            _actionInfoDict = new Dictionary<ActionType, ActionPerformConfig> ();

            foreach (var actionPerformConfig in actionConfigs)
            {
                if (_actionInfoDict.ContainsKey (actionPerformConfig.GetActionType ()))
                {
                    Log.Error ("Dont allow multi action with same type: {0} in \"{1}\"",
                        actionPerformConfig.GetActionType (), name);
                    continue;
                }

                if (actionPerformConfig.GetActionType () == ActionType.Custom)
                {
                    AddCustomActionInfo (actionPerformConfig);
                    continue;
                }

                _actionInfoDict.Add (actionPerformConfig.actionTypeInfo.ActionType, actionPerformConfig);
            }
        }

        private void AddCustomActionInfo (ActionPerformConfig config)
        {
            if (_customActionInfoDict == null)
                _customActionInfoDict = new Dictionary<string, ActionPerformConfig> ();

            var actionID = config.GetActionID ();
            if (_customActionInfoDict.ContainsKey (actionID))
            {
                Log.Error ("Dont allow same Action ID --> {0} in \"{1}\"", actionID, name);
                return;
            }

            _customActionInfoDict.Add (actionID, config);
        }

        public void Do (ActionData actionData, Action finishCallback = null)
        {
            var responseConfig = GetActionInfo (actionData);
            if (responseConfig == null)
            {
                Log.Error ("No suitable action info for ->{0}<-  in  ->{1}<-", actionData.ActionType, gameObject.name);
                finishCallback?.Invoke ();
                return;
            }

            DoActionByConfig (responseConfig, finishCallback);
        }

        private ActionPerformConfig GetActionInfo (ActionData actionData)
        {
            var actionType = actionData.ActionType;
            if (_actionInfoDict == null)
                return null;
            if (_actionInfoDict.ContainsKey (actionType))
                return _actionInfoDict[actionType];

            if (actionType == ActionType.Custom && _customActionInfoDict.ContainsKey (actionData.ActionID))
                return _customActionInfoDict[actionData.ActionID];

            return _actionInfoDict.ContainsKey (ActionType.Default) ? _actionInfoDict[ActionType.Default] : null;
        }

        private void DoActionByConfig (ActionPerformConfig performConfig, Action finishCallback)
        {
            var responses = performConfig.performs;
            var waitDone  = false;
            foreach (var response in responses)
            {
                DoActionByResponse (response, finishCallback, ref waitDone);
            }

            if (waitDone)
                return;
            finishCallback?.Invoke ();
        }

        private void DoActionByResponse (PerformData data, Action finishCallback, ref bool waitDone)
        {
            if (data.waitDone)
                waitDone = true;

            var performer = data.Performer;
            if (performer == null)
            {
                performer      = GetResponsePerformer (data.performType);
                data.Performer = performer;
            }

            performer.Perform (data, finishCallback, gameObject);
        }

        private static IResponsePerformer GetResponsePerformer (PerformType type)
        {
            switch (type)
            {
                case PerformType.Default:
                    return InvalidPerformer.Default;
                case PerformType.Animation:
                    return new AnimationPerformer ();
                case PerformType.ShowGo:
                    return new ShowGoPerformer ();
                default:
                    return InvalidPerformer.Default;
            }
        }
    }

    [Serializable]
    public class ActionPerformConfig
    {
        public ActionTypeInfo actionTypeInfo;

        public PerformData[] performs;

        public ActionType GetActionType ()
        {
            return actionTypeInfo?.ActionType ?? ActionType.Default;
        }

        public string GetActionID ()
        {
            return actionTypeInfo?.ActionID ?? string.Empty;
        }

    }

    [Serializable]
    public class ActionTypeInfo
    {
        public ActionType ActionType;
        public string     ActionID;
    }
}