/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月24日 星期二
 * Time: 下午7:03:27
 ***/

using System;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction
{

    public class MoveControl : MonoBehaviour
    {

        [SerializeField] private Vector3 MoveVelocity;
        [SerializeField] private Vector3 MoveAcceleration;

        private float vRate = 0.016f;
        private float aRate = 0.016f;

        private Transform _transform;
        private Vector3   _originPosition;

        private bool _isMoving;
        private bool _isMoveToOrigin;

        private IDirectionOwner _directionOwner;

        public bool IsMoving => _isMoving;

        private void Awake ()
        {
            _directionOwner = GetComponent<IDirectionOwner> ();
            _transform      = transform;
            _originPosition = _transform.localPosition;
        }

        private Action _moveEndCallback;

        public void Move (Vector3 v, Vector3 a, Action moveEndCallback)
        {
            if (_isMoving)
            {
                Log.Error ("is moving");
                moveEndCallback?.Invoke ();
                return;
            }

            _moveEndCallback = moveEndCallback;

            MoveVelocity     = v;
            MoveAcceleration = a;


            if (_directionOwner.GetDirection () == Direction.FaceToLeft)
            {
                MoveVelocity.z     = -MoveVelocity.z;
                MoveAcceleration.z = -MoveAcceleration.z;
            }

            _isMoving       = true;
            _isMoveToOrigin = false;
        }

        public void MoveToOriginPosition (float time, Action moveEndCallback)
        {
            if (time <= 0)
            {
                time = 0.1f;
            }

            if (_isMoving)
            {
                Log.Error ("is moving");
                moveEndCallback?.Invoke ();
                return;
            }

            _moveEndCallback = moveEndCallback;

            var distance = _originPosition - _transform.localPosition;
            var v        = distance / time;
            MoveVelocity     = v;
            MoveAcceleration = Vector3.zero;
            _isMoving        = true;
            _isMoveToOrigin  = true;
        }

        public void Stop ()
        {
            if (_isMoving)
                OnMoveEnd ();

            _isMoving        = false;
            _isMoveToOrigin  = false;
            _moveEndCallback = null;

            MoveVelocity     = Vector3.zero;
            MoveAcceleration = Vector3.zero;
        }

        private void FixedUpdate ()
        {
            if (!_isMoving)
                return;

            var move = false;

            if (_isMoveToOrigin)
            {
                move = CheckIsMovingToOrigin ();
            }
            else
            {
                move = CalculateVelocityByAcceleration ();
            }

            _isMoving = move;

            if (move)
                _transform.localPosition += MoveVelocity * vRate;
            else
                OnMoveEnd ();
        }

        private bool CheckIsMovingToOrigin ()
        {
            if (AlmostZero (_transform.localPosition.x - _originPosition.x, MoveVelocity.x * vRate))
                MoveVelocity.x = 0;

            if (AlmostZero (_transform.localPosition.y - _originPosition.y, MoveVelocity.y * vRate))
                MoveVelocity.y = 0;

            if (AlmostZero (_transform.localPosition.z - _originPosition.z, MoveVelocity.z * vRate))
                MoveVelocity.z = 0;

            return !AlmostZero (MoveVelocity.x,    float.Epsilon)
                   || !AlmostZero (MoveVelocity.y, float.Epsilon)
                   || !AlmostZero (MoveVelocity.z, float.Epsilon);
        }

        private bool CalculateVelocityByAcceleration ()
        {
            var moving = false;
            if (!AlmostZero (MoveVelocity.x, float.Epsilon))
            {
                moving = true;

                MoveVelocity.x -= MoveAcceleration.x * aRate;
                if (AlmostZero (MoveVelocity.x, MoveAcceleration.x * aRate))
                {
                    MoveVelocity.x     = 0;
                    MoveAcceleration.x = 0;
                }
            }

            if (!AlmostZero (MoveVelocity.y, float.Epsilon))
            {
                moving = true;

                MoveVelocity.y -= MoveAcceleration.y * aRate;
                if (AlmostZero (MoveVelocity.y, MoveAcceleration.y * aRate))
                {
                    MoveVelocity.y = 0;
                }
            }

            if (!AlmostZero (MoveVelocity.z, float.Epsilon))
            {
                moving = true;

                MoveVelocity.z -= MoveAcceleration.z * aRate;
                if (AlmostZero (MoveVelocity.z, MoveAcceleration.z * aRate))
                {
                    MoveVelocity.z     = 0;
                    MoveAcceleration.z = 0;
                }
            }

            return moving;
        }

        private void OnMoveEnd ()
        {
            _moveEndCallback?.Invoke ();
            _moveEndCallback = null;
        }

        private bool AlmostZero (float value, float precision)
        {
            if (value < 0)
                value = -value;
            if (precision < 0)
                precision = -precision;
            return value <= precision;
        }
    }
}