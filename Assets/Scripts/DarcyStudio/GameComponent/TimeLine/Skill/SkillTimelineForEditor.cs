/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月26日 星期四
 * Time: 下午9:00:27
 ***/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine.Skill
{
    public partial class SkillTimeline : ISerializationCallbackReceiver
    {
        public void OnBeforeSerialize ()
        {
            gameObject.name = skillKey;
        }

        public void OnAfterDeserialize ()
        {
        }

        public void DrawCreateTimelineButton ()
        {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button ("Create Timeline Asset"))
            {
                CreateTimelineAsset ();
            }

            GUI.backgroundColor = color;
        }

        private void CreateTimelineAsset ()
        {
            if (string.IsNullOrEmpty (skillKey))
            {
                EditorUtility.DisplayDialog ("Warning", "请先输入技能名字", "知道了");
                return;
            }

            gameObject.name = skillKey;

            var dictPath = Path.Combine (Application.dataPath, SkillEditor.Folder, skillKey);

            if (!Directory.Exists (dictPath))
            {
                Directory.CreateDirectory (dictPath);
            }

            var path = Path.Combine (dictPath, "timeline.playable");

            if (File.Exists (path))
            {
                if (EditorUtility.DisplayDialog ("Warning", $"技能-{skillKey} 文件夹中已经存在对应 Timeline 文件 是否覆盖？", "是", "否"))
                {
                    File.Delete (path);
                    AssetDatabase.Refresh ();
                    CreateAndBindTimelineAsset (Path.Combine ("Assets", SkillEditor.Folder, skillKey,
                        "timeline.playable"));
                }
            }
            else
            {
                CreateAndBindTimelineAsset (Path.Combine ("Assets", SkillEditor.Folder, skillKey, "timeline.playable"));
                // CreateAndBindTimelineAsset (path);
            }
        }

        private void CreateAndBindTimelineAsset (string path)
        {
            var asset = SkillEditor.CreateTimelineAsset (path, out var message);
            if (!string.IsNullOrEmpty (message) || asset == null)
            {
                EditorUtility.DisplayDialog ("失败", $"创建 Timeline 文件失败 ⚠️ \n{message}", "知道了～");
                playableDirector.playableAsset = null;
                timelineAsset                  = null;
                return;
            }

            playableDirector.playableAsset = asset;
            timelineAsset                  = asset;
            EditorUtility.DisplayDialog ("成功", "生成 Timeline 文件 成功", "知道了～");
        }

        public void DrawInitButton ()
        {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button ("InitTracks"))
            {
                InitTrackInfos ();
            }

            GUI.backgroundColor = color;
        }

        public void DrawSaveButton ()
        {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button ("Save to prefab", GUILayout.Width (100)))
                SaveToPrefab ();
            GUI.backgroundColor = color;
        }

        public void InitBinding ()
        {
            playableDirector = GetComponentInChildren<PlayableDirector> ();
        }

        private void InitTrackInfos ()
        {
            InitBinding ();

            var director = playableDirector;

            if (director.playableAsset == null)
            {
                Debug.LogError ("No TimelineAsset");
                trackInfos = new TrackInfo[0];
                return;
            }

            var outputs          = director.playableAsset.outputs;
            var playableBindings = outputs as PlayableBinding[] ?? outputs.ToArray ();

            var preInfos = trackInfos.ToList ();
            trackInfos = new TrackInfo[playableBindings.Length];

            var i = 0;
            foreach (var o in playableBindings)
            {
                var trackName = o.streamName;
                var preInfo   = preInfos.Find (a => a.Name == trackName);
                if (preInfo != null)
                    trackInfos[i] = preInfo;
                else
                    trackInfos[i] = new TrackInfo (trackName, TrackType.Default);
                i++;
            }
        }

        private void SaveToPrefab ()
        {
            InitTrackInfos ();
            //TODO: save
            gameObject.name = skillKey;
            var dictPath = Path.Combine (Application.dataPath, SkillEditor.Folder, skillKey);

            if (!Directory.Exists (dictPath))
            {
                Directory.CreateDirectory (dictPath);
            }

            var path = Path.Combine (dictPath, skillKey + ".prefab");

            if (File.Exists (path))
            {
                if (EditorUtility.DisplayDialog ("保存 Prefab", "指定路径已经存在对应 Prefab 是否覆盖", "是", "否"))
                {
                    Save (path);
                }

                return;
            }

            Save (path);
        }

        private void Save (string path)
        {
            try
            {
                PrefabUtility.UnpackPrefabInstance (gameObject, PrefabUnpackMode.Completely,
                    InteractionMode.AutomatedAction);
                PrefabUtility.SaveAsPrefabAssetAndConnect (gameObject, path, InteractionMode.AutomatedAction,
                    out var success);
                if (success)
                {
                    EditorUtility.DisplayDialog ("保存成功", "保存成功！", "OK");
                    AssetDatabase.Refresh ();
                }
                else
                {
                    EditorUtility.DisplayDialog ("保存失败", "保存失败！", "OK");
                }
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog ("保存失败", e.Message + "\n \n请找开发同学", "OK");
            }
        }

        public bool IsValid (out string info)
        {
            info = string.Empty;
            CheckValid (out info);
            return string.IsNullOrEmpty (info);
        }

        private readonly Dictionary<string, TrackInfo> _dict          = new Dictionary<string, TrackInfo> ();
        private readonly StringBuilder                 _stringBuilder = new StringBuilder ();

        private void CheckValid (out string info)
        {
            info = string.Empty;

            _dict.Clear ();
            _stringBuilder.Clear ();

            if (trackInfos == null || trackInfos.Length < 1)
            {
                info = "No track info";
                return;
            }

            foreach (var trackInfo in trackInfos)
            {
                if (_dict.ContainsKey (trackInfo.Name))
                {
                    // Debug.LogError (
                    // $"Two track with the same name are not allowed ---->  Track name: <color=red>\"{trackInfo.Name}\"</color>");
                    // Debug.LogError ($"不允许有两个同名轨道 ----> 轨道名: <color=red>\"{trackInfo.Name}\"</color>");
                    _stringBuilder.Append (" \"").Append (trackInfo.Name).Append ("\" ");
                    continue;
                }

                _dict.Add (trackInfo.Name, trackInfo);
            }

            if (_stringBuilder.Length > 0)
            {
                info = "Two track with the same name are not allowed ----> Track name:" + _stringBuilder +
                       "\n不允许有两个同名轨道 ----> 轨道名:"                                        + _stringBuilder;
            }
        }
    }


    public static class SkillEditor
    {
        public const string Folder = "PveSkill";

        [MenuItem ("GameObject/Skill Editor/Create", priority = 0)]
        public static void CreateSkillEditor ()
        {
            var obj           = AddObject ("SkillTimeLine", false, true);
            var skillTimeline = obj.AddComponent<SkillTimeline> ();
            var playable      = AddObject ("Timeline", false, true, obj);
            playable.AddComponent<PlayableDirector> ();
            skillTimeline.InitBinding ();
            Selection.activeGameObject = obj;
        }

        public static TimelineAsset CreateTimelineAsset (string path, out string message)
        {
            var asset = ScriptableObject.CreateInstance<TimelineAsset> ();
            if (asset == null)
            {
                message = "Create instance fail";
                return null;
            }

            asset.CreateTrack<AnimationTrack> (null, "Self");
            try
            {
                AssetDatabase.CreateAsset (asset, path);
                AssetDatabase.SaveAssets ();
                message = string.Empty;
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return asset;
        }

        static GameObject AddObject (string objName, bool addParentName, bool autoScale = false,
            GameObject                      specifyParent = null)
        {
            var parent = specifyParent ? specifyParent : Selection.activeGameObject;

            GameObject obj = new GameObject (addParentName && parent ? objName + " (" + parent.name + ")" : objName);
            if (parent)
            {
                obj.transform.parent = parent.transform;
            }

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            if (autoScale && parent)
            {
                var scl = parent.transform.lossyScale;
                obj.transform.localScale = new Vector3 (1.0f / scl.x, 1.0f / scl.y, 1.0f / scl.z);
            }
            else
                obj.transform.localScale = Vector3.one;

            return obj;
        }
    }
}

#endif