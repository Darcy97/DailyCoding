/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:39:14
 * Description: Description
 ***/

using System;
using System.Linq;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.WorkState;
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
        // private List<TimelineClip>  _timelineClips;

        private Action<TimelineUnit> _playFinished;

        public PlayableDirector Director => _director;

        public void SetExtrapolationMode (DirectorWrapMode mode)
        {
            _director.extrapolationMode = mode;
        }

        public void SetPlayOnAwake (bool playOnAwake)
        {
            _director.playOnAwake = playOnAwake;
        }

        public double CurrentTime => _director.time;
        public double Duration    => _director.duration;

        // public void RegisterFinishEvent (Action<TimelineUnit> action)
        // {
        // _playFinished = action;
        // }

        public void Init (string name, PlayableDirector director, TimelineAsset asset)
        {
            _director               = director;
            _asset                  = asset;
            _director.playableAsset = asset;
            _name                   = name;


            InitBindingsAndPlayableAssets ();
            // InitEvent ();
        }

        public void SetWorkDoneListener (IWorkStateListener listener)
        {
            ForeachPlayableAssets (p =>
            {
                if (p is IRequireWaitDone r && r.Require ())
                {
                    r.SetWaitDoneListener (listener);
                }
            });
        }

        public void Dispose ()
        {
            if (_director.state == PlayState.Playing)
                _director.Stop ();
            _director.stopped -= OnDirectorStop;
        }

        // private void InitEvent ()
        // {
        //     _director.stopped += OnDirectorStop;
        // }

        private void OnDirectorStop (PlayableDirector director)
        {
            Log.Info ("On stop");
            if (_playFinished == null)
                return;
            if (director.time > 0)
            {
                _playFinished?.Invoke (this);
            }
        }

        private void InitBindingsAndPlayableAssets ()
        {
            var outPuts = _asset.outputs;
            _bindings       = new Dictionary<string, PlayableBinding> ();
            _playableAssets = new List<PlayableAsset> ();
            // _timelineClips  = new List<TimelineClip> (_asset.outputs.Count ());
            foreach (var o in outPuts)
            {
                var trackName = o.streamName;
                _bindings.Add (trackName, o);

                var track = o.sourceObject as TrackAsset;

                if (track == null)
                    continue;

                var clipList = track.GetClips ();
                // var timeLineClips = clipList as TimelineClip[] ?? clipList.ToArray ();
                // _timelineClips.AddRange (timeLineClips);
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

        public void SetObjectProvider (IObjectProvider provider)
        {
            ForeachPlayableAssets (p =>
            {
                if (p is IObjectDemander demander)
                {
                    demander.SetProvider (provider);
                }
            });
        }

        public void SetAnimationExtrapolation ()
        {
            // ForeachTimelineClips ((t) =>
            // {
            //     if (t.animationClip != null)
            //     {
            //         
            //     }
            // });
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

        // public void SetRequireGameObjects (GameObject related, GameObject source, GameObject[] target)
        // {
        //     ForeachPlayableAssets (p =>
        //     {
        //         if (p is IRequireTarget needTarget)
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
        //         if (p is IRequireSource needSource)
        //             needSource.SetSource (source);
        //
        //         if (p is IRequireControlledObject needRelated)
        //             needRelated.SetControlled (related);
        //     });
        // }

        private void ForeachPlayableAssets (Action<PlayableAsset> action)
        {
            foreach (var playableAsset in _playableAssets)
            {
                if (playableAsset)
                    action (playableAsset);
            }

            // foreach (var o in _asset.outputs)
            // {
            //     var track = o.sourceObject as TrackAsset;
            //
            //     if (track == null)
            //         continue;
            //
            //     var clipList = track.GetClips ();
            //     foreach (var timeLineClip in clipList)
            //     {
            //         var playableAsset = timeLineClip.asset as PlayableAsset;
            //         action (playableAsset);
            //     }
            // }
        }

        private void ForeachTimelineClips (Action<TimelineClip> action)
        {
            // foreach (var timelineClip in _timelineClips)
            // {
            // if (timelineClip != null)
            // action (timelineClip);
            // }
        }

        public void Play ()
        {
            _director.Play ();
        }

        public void Stop ()
        {
            _director.Stop ();
        }
    }
}