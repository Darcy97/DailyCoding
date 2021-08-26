/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月19日 星期四
 * Time: 下午8:51:47
 * 
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Sender
{
    [Serializable]
    public class AttackActionConfigData
    {

        public List<AttackActionConfig> actionPairs;

        public AttackActionConfig GetActionInfoByPreviousAction (ActionType actionType)
        {
            if (actionPairs == null || actionPairs.Count < 1)
                return default;

            foreach (var actionInfo in actionPairs)
            {
                if ((actionInfo.previousActionType & ActionType.Any) == ActionType.Any)
                    return actionInfo;

                if ((actionInfo.previousActionType & actionType) == actionType)
                    return actionInfo;
            }

            // Log.Error ("No fit action info for ---> {0}", actionType.ToString ());
            return default;
        }
    }

    [Serializable]
    public struct AttackActionConfig
    {
        [EnumList] public ActionType previousActionType;

        public ActionType afterActionType;
        public float      k0;
        public float      k1;
        public float      k2;
        public float      k3;

        public float waitTime;

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
        Next1,
        Next2,
        Next3,
        Next4,
        Next5,
        Next6
    }
}