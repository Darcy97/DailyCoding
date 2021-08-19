/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午4:58:02
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{

    [CustomPropertyDrawer (typeof (ObjectDemandInfo))]
    public class ObjectDemandInfoDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color = GUI.color;

            var labelColor = color;
            //绘制 DemandType 字段
            GUI.color = labelColor;
            var trackNameTitleRect = new Rect (position.x, position.y, 30, position.height);
            EditorGUI.LabelField (trackNameTitleRect, "用于:");
            GUI.color = color;

            var demandTypeRect  = new Rect (position.x + 30, position.y, 70, position.height);
            var demandTypeValue = property.FindPropertyRelative ("DemandType");

            var alert = string.Empty;
            switch ((DemandType) demandTypeValue.enumValueIndex)
            {
                case DemandType.Source:
                    GUI.backgroundColor = Color.green;
                    alert               = "发射方";
                    break;
                case DemandType.Target:
                    GUI.backgroundColor = Color.red;
                    alert               = "受击方";
                    break;
                case DemandType.Controlled:
                    GUI.backgroundColor = Color.cyan;
                    alert               = "被控制";
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }

            EditorGUI.PropertyField (demandTypeRect, demandTypeValue, GUIContent.none);
            var alertRect = new Rect (position.x + 100, position.y, 40, position.height);
            EditorGUI.LabelField (alertRect, alert);

            //绘制 ObjectType 字段
            GUI.color = labelColor;
            var trackTypeTitleRect = new Rect (position.x + 150, position.y, 40, position.height);
            EditorGUI.LabelField (trackTypeTitleRect, "从哪来:");

            GUI.color = color;

            var objectTypeRect  = new Rect (position.x + 190, position.y, 60, position.height);
            var objectTypeValue = property.FindPropertyRelative ("ObjectType");


            var backgroudColor = GUI.backgroundColor;
            var needSpeciy     = false;
            switch ((ObjectType) objectTypeValue.enumValueIndex)
            {
                case ObjectType.Self:
                    GUI.backgroundColor = Color.gray;
                    break;
                case ObjectType.Enemy1:
                case ObjectType.Enemy2:
                case ObjectType.Enemy3:
                case ObjectType.Enemy4:
                case ObjectType.Enemy5:
                    GUI.backgroundColor = Color.red;
                    break;
                case ObjectType.Specify:
                    GUI.backgroundColor = Color.cyan;
                    needSpeciy          = true;
                    break;
                case ObjectType.Teammate1:
                case ObjectType.Teammate2:
                case ObjectType.Teammate3:
                case ObjectType.Teammate4:
                    GUI.backgroundColor = Color.yellow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException ();
            }

            EditorGUI.PropertyField (objectTypeRect, objectTypeValue, GUIContent.none);

            if (needSpeciy)
            {
                var objectRect  = new Rect (position.x + 260, position.y, 140, position.height);
                var objectValue = property.FindPropertyRelative ("_gameObject");
                // EditorGUILayout.ObjectField (objectValue, GUIContent.none);
                EditorGUI.ObjectField (objectRect, objectValue, GUIContent.none);
            }

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            GUI.backgroundColor   = backgroudColor;
            EditorGUI.EndProperty ();
        }

        // public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        // {
        //     // EditorGUI.BeginProperty (position, label, property);
        //     EditorGUILayout.BeginVertical ();
        //
        //     var color = GUI.color;
        //
        //     var demandTypeValue = property.FindPropertyRelative ("DemandType");
        //     switch ((DemandType) demandTypeValue.enumValueIndex)
        //     {
        //         case DemandType.Source:
        //             GUI.backgroundColor = Color.green;
        //             break;
        //         case DemandType.Target:
        //             GUI.backgroundColor = Color.red;
        //             break;
        //         case DemandType.Controlled:
        //             GUI.backgroundColor = Color.cyan;
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException ();
        //     }
        //
        //     EditorGUILayout.PropertyField (demandTypeValue, new GUIContent ("DemandType(用于什么)"));
        //
        //     //绘制 ObjectType 字段
        //     // GUI.color = Color.cyan;
        //     // var trackTypeTitleRect = new Rect (position.x + 150, position.y, 30, position.height);
        //     // EditorGUI.LabelField (trackTypeTitleRect, "ObjectType:");
        //
        //     GUI.color = color;
        //
        //     // var objectTypeRect  = new Rect (position.x + 180, position.y, 60, position.height);
        //     var objectTypeValue = property.FindPropertyRelative ("ObjectType");
        //
        //
        //     var backgroudColor = GUI.backgroundColor;
        //     var needSpeciy     = false;
        //     switch ((ObjectType) objectTypeValue.enumValueIndex)
        //     {
        //         case ObjectType.Self:
        //             GUI.backgroundColor = Color.green;
        //             break;
        //         case ObjectType.Enemy1:
        //         case ObjectType.Enemy2:
        //         case ObjectType.Enemy3:
        //         case ObjectType.Enemy4:
        //         case ObjectType.Enemy5:
        //             GUI.backgroundColor = Color.red;
        //             break;
        //         case ObjectType.Specify:
        //             GUI.backgroundColor = Color.cyan;
        //             needSpeciy          = true;
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException ();
        //     }
        //
        //     EditorGUILayout.PropertyField (objectTypeValue, new GUIContent ("ObjectType(从哪里来)"));
        //
        //     if (needSpeciy)
        //     {
        //         // var objectRect  = new Rect (position.x + 240, position.y, 60, position.height);
        //         var objectValue = property.FindPropertyRelative ("_gameObject");
        //         EditorGUILayout.PropertyField (objectValue);
        //     }
        //
        //     // EditorGUI.indentLevel = indent;
        //     GUI.color           = color;
        //     GUI.backgroundColor = backgroudColor;
        //     EditorGUILayout.EndVertical ();
        //     // EditorGUI.EndProperty ();
        // }
    }
}