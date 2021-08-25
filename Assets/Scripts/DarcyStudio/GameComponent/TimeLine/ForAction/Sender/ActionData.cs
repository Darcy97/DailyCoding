/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月19日 星期四
 * Time: 下午8:51:47
 * 
 ***/

using System;
using System.Collections.Generic;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Sender
{
    [Serializable]
    public class ActionData
    {

        public List<ActionInfo> actionPairs;

        public ActionInfo GetActionInfoByPreviousAction (ActionType actionType)
        {
            foreach (var actionInfo in actionPairs)
            {
                if (actionInfo.previousActionType == ActionType.Any)
                    return actionInfo;
                if (actionInfo.previousActionType == actionType)
                    return actionInfo;
            }

            // Log.Error ("No fit action info for ---> {0}", actionType.ToString ());
            return null;
        }
    }

    [Serializable]
    public class ActionInfo
    {
        public ActionType previousActionType;
        public ActionType afterActionType;
        public float      k0;
        public float      k1;
        public float      k2;
        public float      k3;

        public static ActionInfo Default = new ActionInfo ();
    }

    public enum ActionType
    {
        Any,
        None,
        Default,
        Custom,
        Idle,
        Back, //击退
        Fall,
        KnockFly, //击飞
        Floating, //浮空
        GetUp,
        ResetToOrigin, //归位
    }
}