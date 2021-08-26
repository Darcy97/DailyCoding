/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午2:53:23
 ***/

using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (ColorAttribute))]
    public class ColorAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var at    = (ColorAttribute) attribute;
            var color = GUI.backgroundColor;
            if (!ColorUtility.TryParseHtmlString (at.Color, out var pColor))
            {
                pColor = color;
            }

            GUI.backgroundColor = pColor;
            EditorGUI.PropertyField (position, property, label);
            GUI.backgroundColor = color;
        }
    }
}