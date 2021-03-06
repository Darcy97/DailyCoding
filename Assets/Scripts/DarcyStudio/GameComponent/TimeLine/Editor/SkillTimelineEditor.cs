/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 上午11:05:33
 * Description:
 *
 * 支持动态旋转方向
 * 发射物体
 * 动态播放动态获取的物体的动画
 * 
 ***/

using DarcyStudio.GameComponent.TimeLine.Skill;
using UnityEditor;
using UnityEngine;
using SkillTimeline = DarcyStudio.GameComponent.TimeLine.Skill.SkillTimeline;

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

            EditorGUILayout.PropertyField (serializedObject.FindProperty ("skillKey"), new GUIContent ("技能名字"));

            // base.OnInspectorGUI ();

            EditorGUILayout.PropertyField (serializedObject.FindProperty ("playableDirector"), true);
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("timelineAsset"),    true);

            myTarget.DrawCreateTimelineButton ();

            EditorGUILayout.BeginHorizontal ();
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("trackInfos"), true);
            myTarget.DrawInitButton ();
            EditorGUILayout.EndHorizontal ();

            EditorGUILayout.Space ();

            myTarget.DrawSaveButton ();
            EditorGUILayout.Space ();

            var color = GUI.color;
            GUI.color = Color.green;
            EditorGUILayout.LabelField ($"相关文件都保存在 --- \"{SkillEditor.Folder}\" ");
            GUI.color = color;

            serializedObject.ApplyModifiedProperties ();
            EditorGUI.EndChangeCheck ();

            serializedObject.Update ();
        }
    }
}