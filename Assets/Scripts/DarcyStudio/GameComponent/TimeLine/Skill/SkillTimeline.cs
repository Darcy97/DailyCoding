/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午4:35:00
 * Description: Description
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.WorkState;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine.Skill
{
    public partial class SkillTimeline : MonoBehaviour, ISkillPlayer, IWorkStateListener
    {

        [SerializeField] private string skillKey;

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
            _timeline.SetExtrapolationMode (DirectorWrapMode.None);
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

        private void FixedUpdate ()
        {
            if (!_isPlaying)
                return;

            if (_timeline.CurrentTime >= _timeline.Duration - Time.fixedDeltaTime - Time.fixedDeltaTime)
            {
                _isTimelinePlaying = false;
            }

            if (_isTimelinePlaying)
                return;

            if (_workingWorkerCount >= 1)
                return;

            _isPlaying = false;
            _finishAction?.Invoke (this);
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

                //
                // if (info.Type == TrackType.Enemy1)
                // {
                //     _timeline.SetBinding (info.Name, provider.Get (ObjectType.Enemy1).GetGameObject ());
                // }
            }

            // SetSourceObject (provider.Get (ObjectType.Self));
        }

        private void SetSourceObject (IObject o)
        {
            // _timeline.SetBinding (selfTrackName, o.GetGameObject ());
        }

        private bool                  _isPlaying         = false;
        private bool                  _isTimelinePlaying = false;
        private Action<SkillTimeline> _finishAction;

        public void Play (Action<ISkillPlayer> action = null)
        {
            _finishAction = action;
            _timeline.Play ();
            _isPlaying          = true;
            _isTimelinePlaying  = true;
            _workingWorkerCount = 0;
        }

        public void Stop ()
        {
            _timeline.Stop ();
        }

        public void IsFinished ()
        {
        }

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