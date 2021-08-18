/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:07:11
 * Description: 
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine
{
    public class SkillTimelineNew
    {

        private IObjectProvider _provider;
        private TimelineUnit    _timeline;

        public SkillTimelineNew (GameObject playGO, TimelineAsset timelineAsset)
        {
            _timeline = TimelineUtils.AddTimeline (playGO, timelineAsset);
            _timeline.SetPlayOnAwake (false);
        }


        public void SetObjectProvider (IObjectProvider provider)
        {
            _timeline.SetObjectProvider (provider);
        }

        public void Play ()
        {
            _timeline.Play ();
        }
    }
}