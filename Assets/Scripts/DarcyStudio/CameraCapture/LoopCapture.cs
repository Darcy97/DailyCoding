/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年7月8日 星期四
 * Time: 下午3:38:02
 * Description: LoopCapture
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.CaptureScreen
{
    public class LoopCapture : MonoBehaviour
    {
        [SerializeField] private List<AnimationClip> _animations;
        [SerializeField] private List<GameObject>    _models;
        [SerializeField] private float               _intervalMax = 10f;


        [SerializeField] private CameraCapture _cameraCapture;

        [SerializeField] private Transform _startPosition;


        private bool _run = false;

        public void Run ()
        {
            _run = true;
            LoadNewActor ();
            _curAnimationClip     = _animations[curAnimationClipIndex];
            _curAnimationClipName = _curAnimationClip.name;
            PlayAnimationClip (_curAnimationClip, _curAnimator, ref _playableGraph);
        }


        private int _numModel;
        private int _numAnimationClip;

        private void Start ()
        {
            _numModel         = _models.Count;
            _numAnimationClip = _animations.Count;
        }

        private float interval = 0;


        private PlayableGraph _playableGraph;

        private int _curModelIndex        = 0;
        private int curAnimationClipIndex = 0;

        private GameObject    _curModel;
        private AnimationClip _curAnimationClip;
        private Animator      _curAnimator;

        private string _curModelName;
        private string _curAnimationClipName;

        private void Update ()
        {
            if (!_run)
                return;

            // if (!_playableGraph.IsPlaying ())
            //     _playableGraph.Play ();
            //
            // if(_playableGraph.IsDone ())
            //     _playableGraph.Play ();

            interval += Time.deltaTime;
            if (interval >= _intervalMax)
            {
                interval = 0;
                _cameraCapture.ResetFrameCount ();
                if (_playableGraph.IsValid ())
                    _playableGraph.Stop ();

                curAnimationClipIndex++;
                if (curAnimationClipIndex >= _numAnimationClip)
                {
                    curAnimationClipIndex = 0;
                    _curModelIndex++;

                    if (_curModelIndex >= _numModel)
                    {
                        _run = false;
                        Debug.Log ("END");
                        return;
                    }

                    LoadNewActor ();
                }


                _curAnimationClip     = _animations[curAnimationClipIndex];
                _curAnimationClipName = _curAnimationClip.name;
                PlayAnimationClip (_curAnimationClip, _curAnimator, ref _playableGraph);
            }

            _cameraCapture.Capture (_curModelName, _curAnimationClipName, _curModelIndex, curAnimationClipIndex);
        }

        private void LoadNewActor ()
        {
            if (_curModel)
                Destroy (_curModel);
            _curModel                         = Instantiate (_models[_curModelIndex], _startPosition);
            _curModel.transform.localPosition = Vector3.zero;
            _curModelName                     = _models[_curModelIndex].name;
            _curAnimator                      = _curModel.GetComponent<Animator> ();
            if (_curAnimator == null)
            {
                _curAnimator = _curModel.AddComponent<Animator> ();
            }
        }

        private void PlayAnimationClip (AnimationClip clip, Animator animator, ref PlayableGraph playableGraph)
        {
            if (animator == null)
            {
                return;
            }

            _curModel.transform.localPosition = Vector3.zero;
            if (playableGraph.IsValid ()) playableGraph.Destroy ();
            AnimationPlayableUtilities.PlayClip (animator, clip, out playableGraph);

            Debug.Log (
                $"StartPlay Model: {_curModelName}_{_curModelIndex:D04} -- Animation: {_curAnimationClipName}_{curAnimationClipIndex:D04}");
        }
    }


}