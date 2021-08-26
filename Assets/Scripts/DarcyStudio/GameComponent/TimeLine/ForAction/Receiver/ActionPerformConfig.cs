/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 下午2:13:08
 ***/

using System;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    [Serializable]
    public struct ActionPerformConfig
    {
        [ColorEnum (ColorType.BackGround)] public ActionType actionType;

        public PerformConfig[] performs;

        public ActionBaseConfig config;

        public ActionType ActionType () => actionType;

        public bool WaitDone ()
        {
            return config.WaitDone;
        }

        public PerformConfig[] GetPerforms ()
        {
            return performs;
        }

        public ActionType GetNextActionType ()
        {
            return config.SpecifyNextAction ? config.NextAction : Sender.ActionType.None;
        }

        public static ActionType GetNextActionType (ActionPerformConfig preConfig)
        {
            var result = Sender.ActionType.None;
            switch (preConfig.ActionType ())
            {
                case Sender.ActionType.None:
                    break;
                case Sender.ActionType.Custom:
                    break;
                case Sender.ActionType.Idle:
                    break;
                case Sender.ActionType.Back:
                    result = Sender.ActionType.Idle;
                    break;
                case Sender.ActionType.Fall:
                    result = Sender.ActionType.GetUp;
                    break;
                case Sender.ActionType.KnockFly:
                    result = Sender.ActionType.Fall;
                    break;
                case Sender.ActionType.Floating:
                    result = Sender.ActionType.Fall;
                    break;
                case Sender.ActionType.Default:
                    break;
                case Sender.ActionType.GetUp:
                    result = Sender.ActionType.Idle;
                    break;
                default:
                    result = Sender.ActionType.None;
                    break;
            }

            return result;
        }
    }

    [Serializable]
    public struct ActionBaseConfig
    {
        public                                    bool       WaitDone;
        public                                    bool       SpecifyNextAction;
        [ColorEnum (ColorType.BackGround)] public ActionType NextAction;
    }
}