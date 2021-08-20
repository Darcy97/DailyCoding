/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月19日 星期四
 * Time: 下午6:49:18
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{
    [RequireComponent (typeof (Animator))]
    public class SuperAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake ()
        {
            _animator = GetComponent<Animator> ();
            AddPlayEndEvent ();
        }

        private void AddPlayEndEvent ()
        {
            var animationClips = _animator.runtimeAnimatorController.animationClips;
            foreach (var t in animationClips)
            {
                var @event = new AnimationEvent
                             {
                                 functionName = nameof (OnSuperAnimatorPlayEnd),
                                 time         = t.length
                             };
                t.AddEvent (@event);
            }

            _animator.Rebind ();
        }

        public void Play (string trigger, Action endCallback = null)
        {
            _playEndCallback = endCallback;
            _animator.SetTrigger (trigger);
        }

        private Action _playEndCallback;

        public void OnSuperAnimatorPlayEnd ()
        {
            _playEndCallback?.Invoke ();
            _playEndCallback = null;
        }

    }
}