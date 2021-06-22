/***
 * Created by Darcy
 * Date: Friday, 18 June 2021
 * Time: 18:12:34
 * Description: Description
 ***/

using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DarcyStudio.GameComponent.SortingLayerDrawerAttribute.Editor
{
    [CustomPropertyDrawer (typeof (SortingLayerAttribute))]
    public class SortingLayerDrawer : PropertyDrawer
    {

        const string NONE = "<None>";

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField (position, "ERROR:", "May only apply to type string");
                return;
            }

            position = EditorGUI.PrefixLabel (position, label);
            var value = property.stringValue;
            if (string.IsNullOrEmpty (value))
                value = NONE;
            if (GUI.Button (position, value, EditorStyles.popup))
            {
                Selector (property);
            }
        }

        private static void Selector (SerializedProperty property)
        {
            var layers = GetSortingLayerNames ();

            var menu = new GenericMenu ();

            var isNone = string.IsNullOrEmpty (property.stringValue);
            menu.AddItem (new GUIContent (NONE), isNone, HandleSelect, new DrawerValuePair (NONE, property));

            foreach (var name in layers)
            {
                menu.AddItem (new GUIContent (name), name == property.stringValue, HandleSelect,
                    new DrawerValuePair (name, property));
            }

            menu.ShowAsContext ();
        }

        private static void HandleSelect (object val)
        {
            var pair = (DrawerValuePair) val;
            pair.Property.stringValue = pair.StringValue.Equals (NONE) ? "" : pair.StringValue;

            pair.Property.serializedObject.ApplyModifiedProperties ();
        }

        // Get the sorting layer names
        private static IEnumerable<string> GetSortingLayerNames ()
        {
            var internalEditorUtilityType = typeof (InternalEditorUtility);
            var sortingLayersProperty =
                internalEditorUtilityType.GetProperty ("sortingLayerNames",
                    BindingFlags.Static | BindingFlags.NonPublic);
            return (string[]) sortingLayersProperty.GetValue (null, new object[0]);
        }
    }
}