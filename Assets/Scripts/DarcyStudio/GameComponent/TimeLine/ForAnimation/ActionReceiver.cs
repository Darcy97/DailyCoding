/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:17:08
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAnimation
{
    [RequireComponent (typeof (Animator))]
    public class ActionReceiver : MonoBehaviour, IActionReceiver
    {

        [SerializeField] private ActionInfo[] actionInfos;
        private                  Animator     _animator;

        private Dictionary<ActionType, ActionInfo> actionInfoDict;

        private void Start ()
        {
            _animator = GetComponent<Animator> ();
            InitActionDict ();
        }

        private void InitActionDict ()
        {
            if (actionInfos == null || actionInfos.Length < 1)
                return;
            actionInfoDict = new Dictionary<ActionType, ActionInfo> ();
            foreach (var actionInfo in actionInfos)
            {
                if (actionInfoDict.ContainsKey (actionInfo.ActionType))
                    continue;
                actionInfoDict.Add (actionInfo.ActionType, actionInfo);
            }
        }

        private ActionInfo GetActionInfo (ActionType actionType)
        {
            if (actionInfoDict == null)
                return null;
            if (actionInfoDict.ContainsKey (actionType))
                return actionInfoDict[actionType];
            return actionInfoDict.ContainsKey (ActionType.Default) ? actionInfoDict[ActionType.Default] : null;
        }

        private bool       _isPlaying;
        private ActionInfo _curActionInfo;

        public void Do (ActionType key)
        {
            var actionInfo = GetActionInfo (key);
            if (actionInfo == null)
            {
                Log.Error ("No suitable action info for >>{0}<< in >>{1}<<", key, gameObject.name);
                OnPlayEnd ();
                return;
            }

            _curActionInfo = actionInfo;
            _animator.SetTrigger (actionInfo.AnimationKey);
            _isPlaying = true;
        }

        private void LateUpdate ()
        {
            if (!_isPlaying)
                return;

            var animatorInfo = _animator.GetCurrentAnimatorStateInfo (0);

            if (!animatorInfo.IsName (_curActionInfo.AnimationKey))
            {
                _isPlaying = false;
                OnPlayEnd ();
                return;
            }

            if (!(animatorInfo.normalizedTime >= 1.0f))
                return;

            OnPlayEnd ();
            _isPlaying = false;
        }


        private void OnPlayEnd ()
        {
            //播放结束时要触发播放结束动作
            _trigger.OnPlayEnd ();
        }

        private IActionTrigger _trigger;

        public void RegisterTrigger (IActionTrigger trigger)
        {
            _trigger = trigger;
        }
    }

    [Serializable]
    public class ActionInfo
    {
        public ActionType ActionType;
        public string     AnimationKey;
    }
}