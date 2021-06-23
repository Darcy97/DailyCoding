/***
 * Created by Darcy
 * Date: Wednesday, 23 June 2021
 * Time: 16:09:43
 * Description: DisableEditDrawer
 ***/

using DarcyStudio.GameComponent.Attribute.SortingLayerDrawerAttribute;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (DisableEditAttribute))]
    public class DisableEditDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            EditorGUI.PropertyField (position, property);

            GUI.enabled = true;
        }
    }
}