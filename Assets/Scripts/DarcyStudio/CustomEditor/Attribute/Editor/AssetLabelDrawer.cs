/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 30 November 2021
 * Time: 16:23:38
 ***/

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using DarcyStudio.GameComponent.Attribute.Editor;
using DarcyStudio.GameComponent.StringHelper;
using UnityEditor;
using UnityEngine;

namespace DarcyStudio.CustomEditor.Attribute.Editor
{
    [CustomPropertyDrawer (typeof (AssetLabelAttribute))]
    public class AssetLabelDrawer : PropertyDrawer
    {
        private const string None = "<None>";

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var at       = (AssetLabelAttribute) attribute;
            var minScore = at.MinScore;
            var maxCount = at.MaxCount;

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField (position, "ERROR:", "May only apply to type string");
                return;
            }

            position = EditorGUI.PrefixLabel (position, label);
            var value = property.stringValue;
            if (string.IsNullOrEmpty (value))
                value = None;
            if (GUI.Button (position, value, EditorStyles.popup))
            {
                Selector (property, minScore, maxCount);
            }
        }

        private static void Selector (SerializedProperty property, int score, int maxCount)
        {
            var layers = GetLabelNames (score, maxCount);

            var menu = new GenericMenu ();

            var isNone = string.IsNullOrEmpty (property.stringValue);
            menu.AddItem (new GUIContent (None), isNone, HandleSelect, new DrawerValuePair (None, property));

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
            pair.Property.stringValue = pair.StringValue.Equals (None) ? "" : pair.StringValue;

            pair.Property.serializedObject.ApplyModifiedProperties ();
        }
        
        private static IEnumerable<string> GetLabelNames (int minScore, int maxCount)
        {
            var internalAssetDatabaseType = typeof (AssetDatabase);
            var getAllLabelsMethod =
                internalAssetDatabaseType.GetMethod ("GetAllLabels",
                    BindingFlags.Static | BindingFlags.NonPublic);
            var labelDict = (Dictionary<string, float>) getAllLabelsMethod.Invoke (null, new object[0]);

            var result = SearchLabels (labelDict, minScore, maxCount);

            return result;
        }

        private static IEnumerable<string> SearchLabels (Dictionary<string, float> searchTarget, float minScore,
            float                                                                  maxCount)
        {
            var result      = new List<string> ();
            var filterScore = searchTarget.Select (item => item.Value).Prepend (minScore).Max ();

            while (result.Count < 15 && filterScore > minScore)
            {
                result      =  SearchLabels (result, filterScore, searchTarget, maxCount);
                filterScore -= 1;
            }

            result.Sort();
            return result;
        }

        private static List<string> SearchLabels (List<string> result,       float filterScore,
            Dictionary<string, float>                          searchTarget, float maxCount)
        {
            foreach (var item in searchTarget)
            {
                if (item.Value < filterScore)
                    continue;

                var label = item.Key.UpperFirst ();

                if (result.Contains (label))
                    continue;

                result.Add (label);

                Debug.Log ($"Label: {item.Key} --- Score: {item.Value}");

                if (result.Count >= maxCount)
                    break;
            }

            return result;
        }
    }
}