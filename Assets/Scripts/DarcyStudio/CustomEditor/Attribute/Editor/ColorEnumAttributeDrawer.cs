/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午3:14:31
 ***/

using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (ColorEnumAttribute))]
    public class ColorEnumAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                var bgColor = GUI.backgroundColor;
                var color   = GetColor (property.enumValueIndex);
                GUI.backgroundColor = color;
                EditorGUI.PropertyField (position, property, label);
                GUI.backgroundColor = bgColor;
            }
            else
            {
                EditorGUI.PropertyField (position, property, label);
            }
        }

        private Color GetColor (int index)
        {
            switch (index)
            {
                case 0:
                    return Color.cyan;
                case 1:
                    return Color.green;
                case 2:
                    return Color.yellow;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.red;
                case 5:
                    return Color.white;
                case 6:
                    return Color.magenta;
                case 7:
                {
                    ColorUtility.TryParseHtmlString ("#90EE90", out var color);
                    return color;
                }
                case 8:
                {
                    ColorUtility.TryParseHtmlString ("#32CD32", out var color);
                    return color;
                }
                case 9:
                {
                    ColorUtility.TryParseHtmlString ("#DAA520", out var color);
                    return color;
                }
                case 10:
                {
                    ColorUtility.TryParseHtmlString ("#FF8C00", out var color);
                    return color;
                }
                case 11:
                {
                    ColorUtility.TryParseHtmlString ("#FF6347", out var color);
                    return color;
                }
                case 12:
                {
                    ColorUtility.TryParseHtmlString ("#B22222", out var color);
                    return color;
                }
                case 13:
                {
                    ColorUtility.TryParseHtmlString ("#696969", out var color);
                    return color;
                }
                case 14:
                {
                    ColorUtility.TryParseHtmlString ("#1E90FF", out var color);
                    return color;
                }
                case 15:
                {
                    ColorUtility.TryParseHtmlString ("#FF1493", out var color);
                    return color;
                }

                case 16:
                {
                    ColorUtility.TryParseHtmlString ("#FF00FF", out var color);
                    return color;
                }

                case 17:
                {
                    ColorUtility.TryParseHtmlString ("#00FFFF", out var color);
                    return color;
                }
                default: return Color.gray;
            }

            return Color.gray;
        }
    }
}