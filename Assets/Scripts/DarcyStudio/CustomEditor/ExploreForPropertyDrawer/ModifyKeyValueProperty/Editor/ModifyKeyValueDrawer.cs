/***
 * Created by Darcy
 * Github: https://github.com/Darcy97
 * Date: Monday, 28 June 2021
 * Time: 21:15:03
 * Description: ModifyKeyValueDrawer
 ***/

using UnityEditor;
using UnityEngine;

namespace DarcyStudio.CustomEditor.ExploreForPropertyDrawer.ModifyKeyValueProperty.Editor
{

    [CustomPropertyDrawer (typeof (ModifyKeyValue))]
    public class ModifyKeyValueDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //绘制 Type 字段
            var typeRect  = new Rect (position.x, position.y, 100, position.height);
            var typeValue = property.FindPropertyRelative ("ModifyValueType");
            EditorGUI.PropertyField (typeRect, typeValue, GUIContent.none);

            //绘制 Key 字段
            var preKeyRect = new Rect (position.x + 120, position.y, 30, position.height);
            EditorGUI.LabelField (preKeyRect, "Key");
            var keyRect  = new Rect (position.x + 150, position.y, 100, position.height);
            var keyValue = property.FindPropertyRelative ("Key");
            EditorGUI.PropertyField (keyRect, keyValue, GUIContent.none);

            var preValueRect = new Rect (position.x + 270, position.y, 35, position.height);
            EditorGUI.LabelField (preValueRect, "Value");


            var valueRect = new Rect (position.x + 305, position.y, 100, position.height);
            var valueType = (ModifyValueType) typeValue.enumValueIndex;

            // 根据 ValueType 来选择绘制什么 UI 到 Inspector
            switch (valueType)
            {
                case ModifyValueType.TypeInt:
                    var intV = property.FindPropertyRelative ("IntValue");
                    EditorGUI.PropertyField (valueRect, intV, GUIContent.none);
                    break;
                case ModifyValueType.TypeColor:
                    var colorV = property.FindPropertyRelative ("ColorValue");
                    EditorGUI.PropertyField (valueRect, colorV, GUIContent.none);
                    break;
                case ModifyValueType.TypeFloat:
                    var floatV = property.FindPropertyRelative ("FloatValue");
                    EditorGUI.PropertyField (valueRect, floatV, GUIContent.none);
                    break;
                case ModifyValueType.Default:
                    break;
                default:
                    Debug.LogError ("Un handle type: " + valueType);
                    break;
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty ();
        }
    }
}