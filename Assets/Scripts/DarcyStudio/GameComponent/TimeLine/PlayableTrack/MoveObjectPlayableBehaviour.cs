/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:44:48
 * Description: Description
 ***/

using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{

    public class MoveObjectPlayableBehaviour : PlayableBehaviour
    {
        private readonly Transform      _target;
        private readonly Vector3        _targetPos;
        private readonly AnimationCurve _curve;
        private readonly Vector3        _startPos;

        private float _curveTotalTime;

        private float _duration;
        private float _curTime;

        public MoveObjectPlayableBehaviour ()
        {
        }

        public MoveObjectPlayableBehaviour (Transform target, Vector3 sourcePos, Vector3 targetPos,
            AnimationCurve                            curve)
        {
            _target    = target;
            _startPos  = sourcePos;
            _targetPos = targetPos;
            _curve     = curve;
        }

        public override void OnPlayableCreate (Playable playable)
        {
            base.OnPlayableCreate (playable);
            playable.SetDuration (2);
        }

        public override void OnGraphStart (Playable playable)
        {
            base.OnGraphStart (playable);
            _curTime        = 0;
            _duration       = (float) playable.GetDuration ();
            _curveTotalTime = _curve[_curve.length - 1].time;
        }

        public override void OnGraphStop (Playable playable)
        {
            base.OnGraphStop (playable);
            _curTime = _duration;
            DoMove ();
        }

        public override void OnBehaviourPlay (Playable playable, FrameData info)
        {
            base.OnBehaviourPlay (playable, info);
            _curTime += info.deltaTime;
            DoMove ();
        }

        public override void ProcessFrame (Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame (playable, info, playerData);
            _curTime += info.deltaTime;
            DoMove ();
        }

        private void DoMove ()
        {
            if (_target == null)
                return;

            var percent = _curTime / _duration;
            if (percent > 1)
                percent = 1;

            var curCurveTime = percent * _curveTotalTime;
            var value        = _curve.Evaluate (curCurveTime);
            _target.position = Vector3.Lerp (_startPos, _targetPos, value);
        }
    }
}