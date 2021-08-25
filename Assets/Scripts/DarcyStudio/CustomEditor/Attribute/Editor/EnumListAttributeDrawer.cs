/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月25日 星期三
 * Time: 下午5:32:16
 ***/

using UnityEngine;
using UnityEditor;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;

namespace DarcyStudio.CustomEditor.Attribute.Editor
{

    [CustomPropertyDrawer (typeof (EnumListAttribute))]
    public class EnumListAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.MaskField (position, label, property.intValue, property.enumNames);
        }
    }
}