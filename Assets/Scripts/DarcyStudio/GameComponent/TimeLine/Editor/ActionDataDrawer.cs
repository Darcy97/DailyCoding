/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月20日 星期五
 * Time: 下午2:18:26
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [CustomPropertyDrawer (typeof (ActionData))]
    public class ActionDataDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            // EditorGUI.BeginChangeCheck ();
            // property.serializedObject.UpdateIfRequiredOrScript ();

            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color           = GUI.color;
            var backgroundColor = GUI.backgroundColor;

            var actionTypeRect  = new Rect (position.x, position.y, 70, position.height);
            var actionTypeValue = property.FindPropertyRelative (nameof (ActionData.ActionType));
            EditorGUI.PropertyField (actionTypeRect, actionTypeValue, GUIContent.none);

            position.x += 75;
            DrawPara (position, property, (ActionType) actionTypeValue.enumValueIndex);

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            GUI.backgroundColor   = backgroundColor;

            // property.serializedObject.ApplyModifiedProperties ();
            // EditorGUI.EndChangeCheck ();
            EditorGUI.EndProperty ();
        }

        private void DrawPara (Rect position, SerializedProperty property, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Default:
                    DrawAllProperty (position, property);
                    break;
                case ActionType.Custom:
                    DrawCustom (position, property);
                    break;
                case ActionType.Shoot:
                    break;
                case ActionType.Hit:
                    break;
                case ActionType.KnockFly:
                    DrawKnockFlyPara (position, property);
                    break;
                case ActionType.Idle:
                    break;
                default:
                    DrawAllProperty (position, property);
                    break;
            }
        }

        private void DrawAllProperty (Rect position, SerializedProperty property)
        {
            DrawProperty (nameof (ActionData.Para1), "Para1", ref position, 35, 30, property);
            DrawProperty (nameof (ActionData.Para2), "Para2", ref position, 35, 30, property);
            DrawProperty (nameof (ActionData.Para3), "Para3", ref position, 35, 30, property);
        }

        private void DrawKnockFlyPara (Rect position, SerializedProperty property)
        {
            DrawProperty (nameof (ActionData.Para1), "击飞力", ref position, 35, 30, property);
        }

        private void DrawCustom (Rect position, SerializedProperty property)
        {
            DrawProperty (nameof (ActionData.ActionID), "ActionKey", ref position, 55, 55, property);
        }

        private static void DrawProperty (string key, string label, ref Rect startPosition, float labelWidth,
            float                                propertyWidth,
            SerializedProperty                   property)
        {
            var labelRect = new Rect (startPosition.x, startPosition.y, labelWidth, startPosition.height);
            EditorGUI.LabelField (labelRect, label);
            startPosition.x += labelWidth;

            var propertyRect = new Rect (startPosition.x, startPosition.y, propertyWidth,
                startPosition.height);
            var propertyValue = property.FindPropertyRelative (key);
            EditorGUI.PropertyField (propertyRect, propertyValue, GUIContent.none);
            startPosition.x += propertyWidth + 5;
        }

    }
}