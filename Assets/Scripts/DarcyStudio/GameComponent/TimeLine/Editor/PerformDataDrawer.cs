/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午3:14:51
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [CustomPropertyDrawer (typeof (PerformData))]
    public class PerformDataDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            var startX = position.x;
            position.height = position.height / 2;
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color           = GUI.color;
            var backgroundColor = GUI.backgroundColor;

            var performType = DrawProperty (nameof (PerformData.performType), string.Empty, ref position, 0, 80,
                property);

            DrawPara (ref position, property, (PerformType) performType.enumValueIndex);

            //绘制第二行
            position.y += position.height;
            position.x =  startX;

            DrawProperty (nameof (PerformData.waitDone), "WaitDone(是否等待结束)", ref position, 130, 20, property,
                true);

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            GUI.backgroundColor   = backgroundColor;

            EditorGUI.EndProperty ();
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var p = property.FindPropertyRelative (nameof (PerformData.performType));
            switch ((PerformType) p.enumValueIndex)
            {
                case PerformType.Default:
                    return base.GetPropertyHeight (property, label) * 2;
                case PerformType.Animation:
                    return base.GetPropertyHeight (property, label) * 2;
                case PerformType.ShowGo:
                    return base.GetPropertyHeight (property, label) * 2;
                default:
                    return base.GetPropertyHeight (property, label) * 2;
            }
        }


        private void DrawPara (ref Rect position, SerializedProperty property, PerformType actionType)
        {
            switch (actionType)
            {
                case PerformType.Default:
                    break;
                case PerformType.Animation:
                    DrawProperty (nameof (PerformData.animationKey), "Key", ref position, 25, 100, property);
                    break;
                case PerformType.ShowGo:
                    DrawProperty (nameof (PerformData.go),       string.Empty, ref position, 0,  130, property);
                    DrawProperty (nameof (PerformData.duration), "Duration",   ref position, 50, 30,  property);
                    break;
                default:
                    break;
            }
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

        // private static SerializedProperty DrawProperty (string key, ref Rect startPosition, float width,
        //     SerializedProperty                                 property)
        // {
        //     var propertyRect = new Rect (startPosition.x, startPosition.y, width,
        //         startPosition.height);
        //     var propertyValue = property.FindPropertyRelative (key);
        //     EditorGUI.PropertyField (propertyRect, propertyValue);
        //     return propertyValue;
        // }
    }
}