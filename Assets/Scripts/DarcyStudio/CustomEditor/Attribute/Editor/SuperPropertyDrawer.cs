/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午4:10:41
 ***/

using UnityEditor;
using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.Editor
{
    public abstract class SuperPropertyDrawer : PropertyDrawer
    {
        private float _startX;

        private const float LineSpace = 2;

        protected float StartX => _startX;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);
            position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
            _startX  = position.x;

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var color           = GUI.color;
            var backgroundColor = GUI.backgroundColor;

            position.height =  (position.height - LineSpace * 2) / GetLineCount (property) - LineSpace;
            position.y      += LineSpace;

            SuperOnGUI (position, property, label);

            EditorGUI.indentLevel = indent;
            GUI.color             = color;
            GUI.backgroundColor   = backgroundColor;

            EditorGUI.EndProperty ();
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var lineCount = GetLineCount (property);

            return base.GetPropertyHeight (property, label) * lineCount + (lineCount) * LineSpace + LineSpace * 2;
        }

        protected abstract void SuperOnGUI (Rect position, SerializedProperty property, GUIContent label);

        protected abstract int GetLineCount (SerializedProperty property);

        protected static SerializedProperty DrawProperty (string key, string label, ref Rect startPosition,
            float                                                labelWidth,
            float                                                propertyWidth,
            SerializedProperty                                   property,
            bool                                                 propertyFront = false)
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

        protected void NewLine (ref Rect position)
        {
            position.x =  _startX;
            position.y += position.height + LineSpace;
        }

    }
}