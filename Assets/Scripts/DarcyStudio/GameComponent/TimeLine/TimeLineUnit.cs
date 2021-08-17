/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:39:14
 * Description: Description
 ***/

using System;
using DarcyStudio.GameComponent.Tools;

namespace DarcyStudio.GameComponent.TimeLine
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Timeline;
    using UnityEngine.Playables;

    public class TimelineUnit
    {
        private string           _name;
        private PlayableDirector _director;
        private PlayableAsset    _asset;

        private Dictionary<string, PlayableBinding> _bindings;
        // private Dictionary<string, Dictionary<string, PlayableAsset>> _clips;

        private List<PlayableAsset> _playableAssets;

        public void SetExtrapolationMode (DirectorWrapMode mode)
        {
            _director.extrapolationMode = mode;
        }

        public void Init (string name, PlayableDirector director, PlayableAsset asset)
        {
            director.playableAsset = asset;
            _name                  = name;
            _director              = director;
            _asset                 = asset;

            _bindings       = new Dictionary<string, PlayableBinding> ();
            _playableAssets = new List<PlayableAsset> ();
            foreach (var o in asset.outputs)
            {
                var trackName = o.streamName;
                _bindings.Add (trackName, o);

                var track = o.sourceObject as TrackAsset;

                if (track == null)
                    continue;

                var clipList = track.GetClips ();
                foreach (var timeLineClip in clipList)
                {
                    //     if (!_clips.ContainsKey (trackName))
                    //     {
                    //         _clips[trackName] = new Dictionary<string, PlayableAsset> ();
                    //     }
                    //
                    //     var clipInfo = _clips[trackName];
                    //     if (!clipInfo.ContainsKey (timeLineClip.displayName))
                    //     {
                    //         clipInfo.Add (timeLineClip.displayName, timeLineClip.asset as PlayableAsset);
                    //     }
                    _playableAssets.Add (timeLineClip.asset as PlayableAsset);
                }
            }
        }

        public void SetBinding (string trackName, Object o)
        {
            if (!_bindings.ContainsKey (trackName))
            {
                Log.Error ("No track: {0}", trackName);
                return;
            }

            var binding = _bindings[trackName];
            _director.SetGenericBinding (binding.sourceObject, o);

            var go = o as GameObject;
            if (go == null)
                return;
            var animator = go.GetComponent<Animator> ();
            if (animator == null)
                go.AddComponent<Animator> ();
        }

        public T GetTrack<T> (string trackName) where T : TrackAsset
        {
            return _bindings[trackName].sourceObject as T;
        }

        // public T GetClip<T> (string trackName, string clipName) where T : PlayableAsset
        // {
        //     if (_clips.ContainsKey (trackName))
        //     {
        //         var track = _clips[trackName];
        //         if (track.ContainsKey (clipName))
        //         {
        //             return track[clipName] as T;
        //         }
        //         else
        //         {
        //             Debug.LogError ("GetClip Error, Track does not contain clip, trackName: " + trackName +
        //                             ", clipName: "                                            + clipName);
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogError ("GetClip Error, Track does not contain clip, trackName: " + trackName + ", clipName: " +
        //                         clipName);
        //     }
        //
        //     return null;
        // }

        // public void SetRequireGameObjects (GameObject related, GameObject source, params GameObject[] target)
        // {
        //     ForeachPlayableAssets (p =>
        //     {
        //         if (p is INeedTarget needTarget)
        //         {
        //             var index = needTarget.GetTargetIndex ();
        //             if (target.Length > index)
        //                 needTarget.SetTarget (target[index]);
        //             else
        //             {
        //                 Log.Error ("Index out of range: {0}", index);
        //             }
        //         }
        //
        //         if (p is INeedSource needSource)
        //             needSource.SetSource (source);
        //
        //         if (p is INeedRelated needRelated)
        //             needRelated.SetRelated (related);
        //     });
        // }
        
        public void SetRequireGameObjects (GameObject related, GameObject source, GameObject[] target)
        {
            ForeachPlayableAssets (p =>
            {
                if (p is IRequireTarget needTarget)
                {
                    var index = needTarget.GetTargetIndex ();
                    if (target.Length > index)
                        needTarget.SetTarget (target[index]);
                    else
                    {
                        Log.Error ("Index out of range: {0}", index);
                    }
                }

                if (p is IRequireSource needSource)
                    needSource.SetSource (source);

                if (p is IRequireControlledObject needRelated)
                    needRelated.SetControlled (related);
            });
        }

        private void ForeachPlayableAssets (Action<PlayableAsset> action)
        {
            foreach (var o in _asset.outputs)
            {
                var track = o.sourceObject as TrackAsset;

                if (track == null)
                    continue;

                var clipList = track.GetClips ();
                foreach (var timeLineClip in clipList)
                {
                    var playableAsset = timeLineClip.asset as PlayableAsset;
                    action (playableAsset);
                }
            }
        }

        public void Play ()
        {
            _director.Play ();
        }
    }
}