/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午4:35:48
 * Description: Description
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine
{
    public static class TimelineUtils
    {
        public static TimelineUnit AddTimeline (GameObject target, TimelineAsset asset)
        {
            var unit     = new TimelineUnit ();
            var director = target.GetComponent<PlayableDirector> ();
            if (null == director)
                director = target.AddComponent<PlayableDirector> ();
            director.Stop ();
            unit.Init (asset.name, director, asset);
            return unit;
        }
    }
}