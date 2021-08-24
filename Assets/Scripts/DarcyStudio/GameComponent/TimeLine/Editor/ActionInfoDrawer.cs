/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午4:06:48
 ***/

using System;
using DarcyStudio.CustomEditor.Attribute.Editor;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.Tools;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{

    [CustomPropertyDrawer (typeof (ActionInfo))]
    public class ActionInfoDrawer : SuperPropertyDrawer
    {

        protected override void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            DrawProperty (nameof (ActionInfo.previousActionType), "Previous", ref position, 45, 70, property);
            position.x += 5;
            var afterAction = DrawProperty (nameof (ActionInfo.afterActionType), "After", ref position, 30, 70,
                property);

            NewLine (ref position);

            switch ((ActionType) afterAction.enumValueIndex)
            {
                case ActionType.None:
                    break;
                case ActionType.Default:
                    break;
                case ActionType.Custom:
                    break;
                case ActionType.Idle:
                    break;
                case ActionType.Back:
                    DrawProperty (nameof (ActionInfo.k0), "X初速度", ref position, 45, 30, property);
                    NewLine (ref position);
                    DrawProperty (nameof (ActionInfo.k1), "X加速度", ref position, 45, 30, property);
                    break;
                case ActionType.Fall:
                    break;
                case ActionType.KnockFly:
                    //TODO
                    break;
                case ActionType.Floating:
                    //TODO
                    break;
                case ActionType.GetUp:
                    break;
                default:
                    Log.Error ("Un handle condition");
                    break;
            }
        }

        protected override int GetLineCount (SerializedProperty property)
        {
            switch ((ActionType) property.FindPropertyRelative (nameof (ActionInfo.afterActionType)).enumValueIndex)
            {
                case ActionType.None:
                case ActionType.Default:
                case ActionType.Custom:
                case ActionType.Idle:
                    return 1;
                case ActionType.Back:
                    return 3;
                case ActionType.Fall:
                    break;
                case ActionType.KnockFly:
                    return 2;
                case ActionType.Floating:
                    return 2;
                case ActionType.GetUp:
                    return 1;
                default:
                    return 1;
            }

            return 1;
        }
    }
}