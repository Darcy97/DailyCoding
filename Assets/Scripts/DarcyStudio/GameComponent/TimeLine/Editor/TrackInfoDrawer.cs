/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 上午11:38:48
 * Description:
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.Skill;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{

    [CustomPropertyDrawer (typeof (TrackInfo))]
    public class TrackInfoDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color = GUI.color;

            //绘制 TrackName 字段
            GUI.color = Color.cyan;
            var trackNameTitleRect = new Rect (position.x, position.y, 40, position.height);
            EditorGUI.LabelField (trackNameTitleRect, "Name:");
            GUI.color = color;

            GUI.enabled = false;
            var trackNameRect  = new Rect (position.x + 40, position.y, 100, position.height);
            var trackNameValue = property.FindPropertyRelative ("Name");
            EditorGUI.PropertyField (trackNameRect, trackNameValue, GUIContent.none);
            GUI.enabled = true;

            //绘制 TrackType 字段
            GUI.color = Color.cyan;
            var trackTypeTitleRect = new Rect (position.x + 150, position.y, 30, position.height);
            EditorGUI.LabelField (trackTypeTitleRect, "Type:");
            GUI.color = color;

            var trackTypeRect  = new Rect (position.x + 180, position.y, 60, position.height);
            var trackTypeValue = property.FindPropertyRelative ("Type");

            var backgroudColor = GUI.backgroundColor;
            switch ((TrackType) trackTypeValue.enumValueIndex)
            {
                case TrackType.Default:
                    GUI.backgroundColor = backgroudColor;
                    break;
                case TrackType.Enemy:
                    GUI.backgroundColor = Color.red;
                    break;
                case TrackType.Self:
                    GUI.backgroundColor = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }

            EditorGUI.PropertyField (trackTypeRect, trackTypeValue, GUIContent.none);

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            EditorGUI.EndProperty ();
        }
    }
}