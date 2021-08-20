/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午4:35:00
 * Description: Description
 * TODO: 设置一个名字，并且提供一个按钮点击后自动将该物体生成一个 Prefab 保存到指定路径
 *
 * 受到攻击不直接播放动画了，提供一个 PlayableScript 播放对应受击方的受击动画
 * 发射物支持携带攻击效果
 ***/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.WorkState;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine.Skill
{
    public class SkillTimeline : MonoBehaviour, ISkillPlayer, IWorkStateListener
    {

        [SerializeField] private string skillKey;

        // [SerializeField] private TimelineAsset asset;

        // [SerializeField] private bool playOnAwake = false;


        [SerializeField] private TrackInfo[] trackInfos;

        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private TimelineAsset    timelineAsset;

        private IObjectProvider _provider;
        private TimelineUnit    _timeline;


        public void Init ()
        {
            _timeline?.Dispose ();
            _timeline = new TimelineUnit ();
            _timeline.Init (timelineAsset.name, playableDirector, timelineAsset);
            _timeline.SetPlayOnAwake (false);
            _timeline.SetExtrapolationMode (DirectorWrapMode.Hold);
            _timeline.SetWorkDoneListener (this);
        }

        public void Dispose ()
        {
            _timeline?.Dispose ();
        }


        // public SkillTimelineNew (GameObject playGO, TimelineAsset timelineAsset)
        // {
        //     _timeline = TimelineUtils.AddTimeline (playGO, timelineAsset);
        //     _timeline.SetPlayOnAwake (false);
        //     _timeline.RegisterFinishEvent (OnStopped);
        //     _timeline.SetExtrapolationMode (DirectorWrapMode.Hold);
        // }

        private void Update ()
        {
            if (!_isPlaying)
                return;

            if (_timeline.CurrentTime >= _timeline.Duration)
            {
                if (_workingWorkerCount < 1)
                {
                    _isPlaying = false;
                    _finishAction?.Invoke (this);
                }
            }
        }

        public void SetObjectProvider (IObjectProvider provider)
        {
            _timeline.SetObjectProvider (provider);
            foreach (var info in trackInfos)
            {
                if (info.Type == TrackType.Self)
                {
                    _timeline.SetBinding (info.Name, provider.Get (ObjectType.Self).GetGameObject ());
                }
            }

            // SetSourceObject (provider.Get (ObjectType.Self));
        }

        private void SetSourceObject (IObject o)
        {
            // _timeline.SetBinding (selfTrackName, o.GetGameObject ());
        }

        private bool                  _isPlaying = false;
        private Action<SkillTimeline> _finishAction;

        public void Play (Action<ISkillPlayer> action = null)
        {
            _finishAction = action;
            _timeline.Play ();
            _isPlaying          = true;
            _workingWorkerCount = 0;
        }

        public void Stop ()
        {
            _timeline.Stop ();
        }

        public void IsFinished ()
        {
        }


#if UNITY_EDITOR
        // public void DrawButtons ()
        // {
        //     var color = GUI.backgroundColor;
        //     GUI.backgroundColor = Color.green;
        //     EditorGUILayout.Space ();
        //     if (GUILayout.Button ("InitTracks"))
        //     {
        //         InitTrackInfos ();
        //     }
        //
        //     if (GUILayout.Button ("Save to prefab"))
        //         SaveToPrefab ();
        //
        //     GUI.backgroundColor = color;
        //     EditorGUILayout.Space ();
        // }

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

        private void InitTrackInfos ()
        {
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
            //TODO: save
            gameObject.name = skillKey;
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
#endif

        public void OnWorkDone ()
        {
            _workingWorkerCount--;
        }

        private int _workingWorkerCount = 0;

        public void StartWorking ()
        {
            _workingWorkerCount++;
        }
    }
}