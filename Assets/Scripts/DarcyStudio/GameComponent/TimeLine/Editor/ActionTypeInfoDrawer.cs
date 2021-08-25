/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午6:02:06
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [CustomPropertyDrawer (typeof (ActionTypeInfo))]
    public class ActionTypeInfoDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color           = GUI.color;
            var backgroundColor = GUI.backgroundColor;

            var actionType = DrawProperty (nameof (ActionTypeInfo.ActionType), string.Empty, ref position, 0, 80,
                property);

            if ((ActionType) actionType.enumValueIndex == ActionType.Custom)
                DrawProperty (nameof (ActionTypeInfo.ActionID), "Action Key", ref position, 60, 60, property);

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            GUI.backgroundColor   = backgroundColor;

            EditorGUI.EndProperty ();
        }

        private static SerializedProperty DrawProperty (string key, string label, ref Rect startPosition,
            float                                              labelWidth,
            float                                              propertyWidth,
            SerializedProperty                                 property,
            bool                                               propertyFront = false)
        {
            var propertyValue = property.FindPropertyRelative (key);

            if (string.IsNullOrEmpty (label))
                DrawProperty (ref startPosition, propertyValue, propertyWidth);
            else
            {
                if (propertyFront)
                {
                    DrawProperty (ref startPosition, propertyValue, propertyWidth);
                    DrawLabel (ref startPosition, label, labelWidth);
                }
                else
                {
                    DrawLabel (ref startPosition, label, labelWidth);
                    DrawProperty (ref startPosition, propertyValue, propertyWidth);
                }
            }


            startPosition.x += 5;
            return propertyValue;
        }

        private static void DrawLabel (ref Rect startPosition, string text, float width)
        {
            var labelRect = new Rect (startPosition.x, startPosition.y, width, startPosition.height);
            EditorGUI.LabelField (labelRect, text);
            startPosition.x += width;
        }

        private static void DrawProperty (ref Rect startPosition, SerializedProperty property, float width)
        {
            var propertyRect = new Rect (startPosition.x, startPosition.y, width,
                startPosition.height);
            EditorGUI.PropertyField (propertyRect, property, GUIContent.none);
            startPosition.x += width;
        }
    }
}