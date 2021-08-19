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

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    [RequireComponent (typeof (SuperAnimator))]
    public class ActionReceiver : MonoBehaviour, IActionReceiver
    {

        [SerializeField] private ActionConfig[] actionConfigs;
        private                  SuperAnimator  _animator;

        private Dictionary<ActionType, ActionConfig> actionInfoDict;

        private void Start ()
        {
            _animator = GetComponent<SuperAnimator> ();
            InitActionConfigDict ();
        }

        private void InitActionConfigDict ()
        {
            if (actionConfigs == null || actionConfigs.Length < 1)
                return;
            actionInfoDict = new Dictionary<ActionType, ActionConfig> ();
            foreach (var actionInfo in actionConfigs)
            {
                if (actionInfoDict.ContainsKey (actionInfo.ActionType))
                    continue;
                actionInfoDict.Add (actionInfo.ActionType, actionInfo);
            }
        }

        private ActionConfig GetActionInfo (ActionType actionType)
        {
            if (actionInfoDict == null)
                return null;
            if (actionInfoDict.ContainsKey (actionType))
                return actionInfoDict[actionType];
            return actionInfoDict.ContainsKey (ActionType.Default) ? actionInfoDict[ActionType.Default] : null;
        }


        public void Do (ActionType key, Action finishCallback = null)
        {
            var actionInfo = GetActionInfo (key);
            if (actionInfo == null)
            {
                Log.Error ("No suitable action info for >>{0}<< in >>{1}<<", key, gameObject.name);
                finishCallback?.Invoke ();
                return;
            }

            if (actionInfo.TriggerType == TriggerType.Animation)
                _animator.SetTrigger (actionInfo.AnimationKey, finishCallback);
        }
    }

    [Serializable]
    public class ActionConfig
    {
        public ActionType  ActionType;
        public TriggerType TriggerType;
        public string      AnimationKey;
    }

    public enum TriggerType
    {
        Default,
        Animation
    }
}