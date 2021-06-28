/***
 * Created by Darcy
 * Date: Saturday, 26 June 2021
 * Time: 16:00:52
 * Description: Description
 ***/

using System;
using DarcyStudio.GameComponent.Attribute.SelectObjectAttribute;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

namespace DarcyStudio.GameComponent.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (DragObjectPath))]
    public class DragObjectPathDrawer : PropertyDrawer
    {

        private const string ObjectSelectorUpdated = "ObjectSelectorUpdated";

        private string _prefabName;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (!(attribute is DragObjectPath drag))
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
                _prefabName          = result;
            }
            
            var prefab = GetDragObject (position, Event.current, drag.SearchType);
            if (prefab)
            {
                //在这里给他赋值就是不行 必须在外面赋值 可能是由于 unity 的时序逻辑导致的
                // property.stringValue = prefab.name;
                var tPath  = AssetDatabase.GetAssetPath (prefab);
                var result = tPath.Substring (0, tPath.IndexOf ('.'));
                _prefabName = result;
            }

            if (!string.IsNullOrEmpty (_prefabName) && property.stringValue != _prefabName)
            {
                property.stringValue = _prefabName;
                EditorUtility.SetDirty (property.serializedObject.targetObject);
            }


            EditorGUI.EndProperty ();
        }

        private static Object GetDragObject (Rect rect, Event @event, SearchType searchType)
        {
            switch (searchType)
            {
                case SearchType.Prefab:
                    return GetDragObject<GameObject> (rect, @event);
                case SearchType.AudioClip:
                    return GetDragObject<AudioClip> (rect, @event);
                case SearchType.Texture:
                    return GetDragObject<Texture> (rect, @event);
                case SearchType.Sprite:
                    return GetDragObject<Sprite> (rect, @event);
                case SearchType.Animator:
                    return GetDragObject<Animator> (rect, @event);
                case SearchType.Font:
                    return GetDragObject<Font> (rect, @event);
                case SearchType.Material:
                    return GetDragObject<Material> (rect, @event);
                case SearchType.Shader:
                    return GetDragObject<Shader> (rect, @event);
                case SearchType.VideoClip:
                    return GetDragObject<VideoClip> (rect, @event);
                default:
                    throw new ArgumentOutOfRangeException (nameof (searchType), searchType, null);
            }
        }

        /// <summary>
        /// 检查是否拖拽到指定区域
        /// </summary>
        /// <typeparam name="T">指定类型资源</typeparam>
        /// <param name="rect">监测区域</param>
        /// <param name="event">事件</param>i
        /// <returns></returns>
        private static T GetDragObject<T> (Rect rect, Event @event) where T : Object
        {
            if (!rect.Contains (@event.mousePosition))
                return null;
            if (DragAndDrop.objectReferences.Length <= 0)
                return null;
            
            T @object = default;
            
            if (DragAndDrop.objectReferences[0].GetType () == typeof (T))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;

                if (@event.type != EventType.DragExited && @event.type != EventType.DragUpdated)
                    return null;

                DragAndDrop.AcceptDrag ();
                GUI.changed = true;
                @object     = (T) DragAndDrop.objectReferences[0];
                Event.current.Use ();
            }
            else
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.None;
            }

            return @object;
        }
    }
}