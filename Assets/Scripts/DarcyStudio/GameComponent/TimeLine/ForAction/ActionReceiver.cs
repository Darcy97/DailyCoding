/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午9:17:08
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.ForAction.ActionPerformer;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    public class ActionReceiver : MonoBehaviour, IActionReceiver
    {

        [SerializeField] private ActionResponseConfig[] actionConfigs;
        private                  SuperAnimator          _animator;

        private Dictionary<ActionType, ActionResponseConfig> actionInfoDict;

        private void Start ()
        {
            // _animator = GetComponent<SuperAnimator> ();
            InitActionConfigDict ();
        }

        private void InitActionConfigDict ()
        {
            if (actionConfigs == null || actionConfigs.Length < 1)
                return;
            actionInfoDict = new Dictionary<ActionType, ActionResponseConfig> ();
            foreach (var actionInfo in actionConfigs)
            {
                if (actionInfoDict.ContainsKey (actionInfo.ActionType))
                    continue;
                actionInfoDict.Add (actionInfo.ActionType, actionInfo);
            }
        }

        private ActionResponseConfig GetActionInfo (ActionType actionType)
        {
            if (actionInfoDict == null)
                return null;
            if (actionInfoDict.ContainsKey (actionType))
                return actionInfoDict[actionType];
            return actionInfoDict.ContainsKey (ActionType.Default) ? actionInfoDict[ActionType.Default] : null;
        }


        public void Do (ActionData actionData, Action finishCallback = null)
        {
            var responseConfig = GetActionInfo (actionData.ActionType);
            if (responseConfig == null)
            {
                Log.Error ("No suitable action info for ->{0}<-  in  ->{1}<-", actionData.ActionType, gameObject.name);
                finishCallback?.Invoke ();
                return;
            }

            DoActionByConfig (responseConfig, finishCallback);
        }

        private void DoActionByConfig (ActionResponseConfig responseConfig, Action finishCallback)
        {
            var responses = responseConfig.responses;
            var waitDone  = false;
            foreach (var response in responses)
            {
                DoActionByResponse (response, finishCallback, ref waitDone);
            }

            if (waitDone)
                return;
            finishCallback?.Invoke ();
        }

        private void DoActionByResponse (ResponseData data, Action finishCallback, ref bool waitDone)
        {
            if (data.WaitDone)
                waitDone = true;

            var performer = data.Performer;
            if (performer == null)
            {
                performer      = GetResponsePerformer (data.ResponseType);
                data.Performer = performer;
            }

            performer.Perform (data, finishCallback, gameObject);
        }

        private static IResponsePerformer GetResponsePerformer (ResponseType type)
        {
            switch (type)
            {
                case ResponseType.Default:
                    return InvalidPerformer.Default;
                case ResponseType.Animation:
                    return new AnimationPerformer ();
                case ResponseType.ShowGo:
                    return new ShowGoPerformer ();
                default:
                    return InvalidPerformer.Default;
            }
        }
    }

    [Serializable]
    public class ActionResponseConfig
    {
        public ActionType ActionType;

        public ResponseData[] responses;

    }

    [Serializable]
    public class ResponseData
    {
        public ResponseType ResponseType;
        public string       AnimationKey;
        public GameObject   GO;
        public float        DelayTime = 0;
        public float        ShowTime  = 1;
        public bool         WaitDone;

        [NonSerialized] public IResponsePerformer Performer;
    }

    public enum ResponseType
    {
        Default,
        Animation,
        ShowGo,
    }
}