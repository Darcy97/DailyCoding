/***
 * Created by Darcy
 * Date: Saturday, 26 June 2021
 * Time: 16:00:52
 * Description: Description
 ***/

using DarcyStudio.GameComponent.Attribute.SelectObjectAttribute;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (SelectObjectPath))]
    public class SelectObjectPathDrawer : PropertyDrawer
    {

        private const string ObjectSelectorUpdated = "ObjectSelectorUpdated";

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (!(attribute is SelectObjectPath drag))
                return;

            var btnRect = new Rect (position);
            position.width -= 60;
            btnRect.x      += btnRect.width - 60;
            btnRect.width  =  60;
            EditorGUI.BeginProperty (position, label, property);
            EditorGUI.PropertyField (position, property, true);
            if (GUI.Button (btnRect, "select"))
            {
                if (drag.SearchType == SearchType.Prefab)
                {
                    EditorGUIUtility.ShowObjectPicker<GameObject> (null, false, drag.SearchFilter, 100);
                }
                else
                    EditorGUIUtility.ShowObjectPicker<Object> (null, false, drag.SearchFilter, 100);
            }

            var commandName = Event.current.commandName;
            if (commandName == ObjectSelectorUpdated && EditorGUIUtility.GetObjectPickerControlID () == 100)
            {
                var picker = EditorGUIUtility.GetObjectPickerObject ();
                var tPath  = AssetDatabase.GetAssetPath (picker);
                var result = tPath.Substring (0, tPath.IndexOf ('.'));
                property.stringValue = result;
            }

            EditorGUI.EndProperty ();
        }
    }
}