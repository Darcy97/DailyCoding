/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:44:48
 * Description: Description
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{

    public class MoveObjectPlayableBehaviour : ObjectDemandPlayableBehaviour
    {
        private GameObject _controlledGO;
        private Transform  _controlled;
        private Vector3    _startPos;
        private Vector3    _targetPos;

        private readonly AnimationCurve _curve;
        private readonly float          _delayDisappearTime;

        private float _curveTotalTime;

        private float _duration;
        private float _curTime;

        private bool _isValid;

        private bool IsValid ()
        {
            if (_curve != null && _curve.length >= 1)
                return _isValid;

            Log.Error ("Animation curve null ---> {0}", nameof (MoveObjectPlayableBehaviour));

            return false;
        }

        public MoveObjectPlayableBehaviour ()
        {
            _isValid = false;
        }

        public MoveObjectPlayableBehaviour (
            AnimationCurve curve, float delayDisappearTime)
        {
            _isValid        = true;
            _curve          = curve;
            _delayDisappearTime = delayDisappearTime;
        }

        public override void OnGraphStart (Playable playable)
        {
            base.OnGraphStart (playable);

            if (!IsValid ())
                return;

            _isWaitingKillControlled = false;

            var prefab = GetObject (DemandType.Controlled).GetGameObject ();
            if (!prefab)
            {
                _isValid = false;
                return;
            }

            var start = GetObject (DemandType.Source);
            _controlledGO = Object.Instantiate (prefab, start.GetTransform ());
            _controlled   = _controlledGO.transform;
            _startPos     = start.GetPos ();
            _targetPos    = GetObject (DemandType.Target).GetPos ();

            _curTime        = 0;
            _duration       = (float) playable.GetDuration ();
            _curveTotalTime = _curve[_curve.length - 1].time;
            HideControlled ();
        }

        public override void OnPlayableDestroy (Playable playable)
        {
            base.OnPlayableDestroy (playable);
            if (!_controlledGO)
                return;
            DestroyControlled ();
        }

        private void ShowControlled ()
        {
            _controlledGO.SetActive (true);
        }

        private void HideControlled ()
        {
            _controlledGO.SetActive (false);
        }

        public override void OnBehaviourPlay (Playable playable, FrameData info)
        {
            base.OnBehaviourPlay (playable, info);

            if (!IsValid ())
                return;

            ShowControlled ();

            _curTime += info.deltaTime;
            DoMove ();
        }

        public override void ProcessFrame (Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame (playable, info, playerData);

            if (!IsValid ())
                return;

            _curTime += info.deltaTime;
            DoMove ();
        }

        private void DoMove ()
        {
            if (!IsValid ())
                return;

            if (!IsPlaying ())
                return;

            if (_controlled == null)
                return;

            var percent = _curTime / _duration;
            if (percent > 1)
            {
                percent = 1;
                KillControlled ();
            }

            var curCurveTime = percent * _curveTotalTime;
            var value        = _curve.Evaluate (curCurveTime);
            _controlled.position = Vector3.Lerp (_startPos, _targetPos, value);
        }

        private bool _isWaitingKillControlled;

        private void KillControlled ()
        {
            if (_delayDisappearTime > 0)
            {
                _isWaitingKillControlled = true;
                YieldUtils.DelayAction (_controlledGO.GetComponent<MonoBehaviour> (),
                    () => { Object.Destroy (_controlledGO); }, _delayDisappearTime);
            }
            else
            {
                HideControlled ();
            }
        }

        private void DestroyControlled ()
        {
            if (!IsPlaying ())
            {
                Object.DestroyImmediate (_controlledGO);
            }
            else
                Object.Destroy (_controlledGO);
        }
    }
}