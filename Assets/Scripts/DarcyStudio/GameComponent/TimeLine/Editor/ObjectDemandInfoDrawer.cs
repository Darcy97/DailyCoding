/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午4:58:02
 ***/

using System;
using DarcyStudio.CustomEditor.Attribute.Editor;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{

    [CustomPropertyDrawer (typeof (ObjectDemandInfo))]
    public class ObjectDemandInfoDrawer : SuperPropertyDrawer
    {
        protected override void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck ();
            property.serializedObject.UpdateIfRequiredOrScript ();

            var backgroudColor = GUI.backgroundColor;

            var trackNameTitleRect = new Rect (position.x, position.y, 30, position.height);
            EditorGUI.LabelField (trackNameTitleRect, "用于:");

            var demandTypeRect  = new Rect (position.x + 30, position.y, 70, position.height);
            var demandTypeValue = property.FindPropertyRelative (nameof (ObjectDemandInfo.DemandType));

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
            var alertRect = new Rect (position.x + 100, position.y, 35, position.height);
            EditorGUI.LabelField (alertRect, alert);

            //绘制 ObjectType 字段
            var trackTypeTitleRect = new Rect (position.x + 140, position.y, 40, position.height);
            EditorGUI.LabelField (trackTypeTitleRect, "从哪来:");

            var objectTypeRect  = new Rect (position.x + 180, position.y, 75, position.height);
            var objectTypeValue = property.FindPropertyRelative (nameof (ObjectDemandInfo.ObjectType));

            var needSpeciy = false;
            switch ((ObjectType) objectTypeValue.enumValueIndex)
            {
                case ObjectType.Self:
                    GUI.backgroundColor = Color.green;
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

            position.x += 260;
            if (needSpeciy)
            {
                var objectRect  = new Rect (position.x, position.y, 140, position.height);
                var objectValue = property.FindPropertyRelative (ObjectDemandInfo.GameObjectParaName ());
                EditorGUI.ObjectField (objectRect, objectValue, GUIContent.none);
                position.x += 145;
            }
            else
            {
                GUI.backgroundColor = backgroudColor;
                DrawProperty (nameof (ObjectDemandInfo.BoneKey), "BoneKey", ref position, 50, 70, property);
            }

            property.serializedObject.ApplyModifiedProperties ();
            EditorGUI.EndChangeCheck ();
        }

        protected override int GetHeight (SerializedProperty property) => 1;
    }
}