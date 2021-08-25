/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 下午2:13:08
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
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
}