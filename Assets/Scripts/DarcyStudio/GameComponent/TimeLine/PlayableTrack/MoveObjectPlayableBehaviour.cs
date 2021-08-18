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
        private Transform _controlled;
        private Vector3   _startPos;
        private Vector3   _targetPos;

        private readonly AnimationCurve _curve;

        private float _curveTotalTime;

        private float _duration;
        private float _curTime;

        private readonly bool _isValid;

        private bool IsValid ()
        {
            if (_curve.length >= 1)
                return _isValid;

            Log.Error ("Animation curve null ---> {0}", nameof (MoveObjectPlayableBehaviour));

            return false;
        }

        private bool IsPlaying ()
        {
#if UNITY_EDITOR
            return Application.isPlaying;
#endif
            return true;
        }

        public MoveObjectPlayableBehaviour ()
        {
            _isValid = false;
        }

        public MoveObjectPlayableBehaviour (
            AnimationCurve curve)
        {
            _isValid = true;
            _curve   = curve;
        }

        public override void OnGraphStart (Playable playable)
        {
            base.OnGraphStart (playable);

            if (!IsValid ())
                return;

            _controlled = GetObject (DemandType.Controlled).GetTransform ();
            _startPos   = GetObject (DemandType.Source).GetPos ();
            _targetPos  = GetObject (DemandType.Target).GetPos ();

            _curTime        = 0;
            _duration       = (float) playable.GetDuration ();
            _curveTotalTime = _curve[_curve.length - 1].time;
        }

        public override void OnGraphStop (Playable playable)
        {
            base.OnGraphStop (playable);

            if (!IsValid ())
                return;

            _curTime = _duration;
            DoMove ();
        }

        public override void OnBehaviourPlay (Playable playable, FrameData info)
        {
            base.OnBehaviourPlay (playable, info);

            if (!IsValid ())
                return;

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
                percent = 1;

            var curCurveTime = percent * _curveTotalTime;
            var value        = _curve.Evaluate (curCurveTime);
            _controlled.position = Vector3.Lerp (_startPos, _targetPos, value);
        }
    }
}