/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 上午11:05:33
 * Description:
 ***/

using UnityEditor;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.Editor
{
    [UnityEditor.CustomEditor (typeof (SkillTimeline))]
    public class SkillTimelineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            EditorGUI.BeginChangeCheck ();
            serializedObject.UpdateIfRequiredOrScript ();

            var myTarget = (SkillTimeline) target;

            if (!myTarget.IsValid (out var info))
                EditorGUILayout.HelpBox (info, MessageType.Error);

            // base.OnInspectorGUI ();

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("trackInfos"), true);
            myTarget.DrawInitButton ();
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.Space ();

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("skillKey"));
            myTarget.DrawSaveButton ();
            EditorGUILayout.EndHorizontal ();
            EditorGUILayout.Space ();

            serializedObject.ApplyModifiedProperties ();
            EditorGUI.EndChangeCheck ();

            serializedObject.Update ();
        }
    }
}