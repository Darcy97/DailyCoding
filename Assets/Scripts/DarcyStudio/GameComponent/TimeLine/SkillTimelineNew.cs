/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:07:11
 * Description: 
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine
{
    public class SkillTimelineNew : MonoBehaviour
    {

        [SerializeField] private PlayableDirector playableDirector;
        [SerializeField] private TimelineAsset    timelineAsset;
        [SerializeField] private string           selfTrackName;

        private IObjectProvider _provider;
        private TimelineUnit    _timeline;


        public void Init ()
        {
            _timeline?.Dispose ();
            _timeline = new TimelineUnit ();
            _timeline.Init (timelineAsset.name, playableDirector, timelineAsset);
            _timeline.SetPlayOnAwake (false);
            _timeline.SetExtrapolationMode (DirectorWrapMode.Hold);
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
                _isPlaying = false;
                _finishAction?.Invoke (this);
            }
        }

        public void Dispose ()
        {
            _timeline.Dispose ();
        }

        public void SetObjectProvider (IObjectProvider provider)
        {
            _timeline.SetObjectProvider (provider);
            SetSourceObject (provider.Get (ObjectType.Self));
        }

        private void SetSourceObject (IObject o)
        {
            _timeline.SetBinding (selfTrackName, o.GetGameObject ());
        }

        private bool                     _isPlaying = false;
        private Action<SkillTimelineNew> _finishAction;

        public void Play (Action<SkillTimelineNew> action = null)
        {
            _finishAction = action;
            _timeline.Play ();
            _isPlaying = true;
        }

    }
}