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

        private void Awake ()
        {
            _transform = transform;
        }

        public void Move (Vector3 v, Vector3 a)
        {
            MoveVelocity     = v;
            MoveAcceleration = a;
        }

        public void Stop ()
        {
            MoveVelocity     = Vector3.zero;
            MoveAcceleration = Vector3.zero;
        }

        private void FixedUpdate ()
        {
            if (MoveVelocity.x >= float.Epsilon)
                MoveVelocity.x -= MoveAcceleration.x * aRate;
            if (AlmostZero (MoveVelocity.x, MoveAcceleration.x * aRate))
            {
                MoveVelocity.x     = 0;
                MoveAcceleration.x = 0;
            }

            if (MoveVelocity.y >= float.Epsilon)
                MoveVelocity.y -= MoveAcceleration.y * aRate;
            if (AlmostZero (MoveVelocity.y, MoveAcceleration.y * aRate))
            {
                MoveVelocity.y = 0;
            }

            if (MoveVelocity.z >= float.Epsilon)
                MoveVelocity.z -= MoveAcceleration.z * aRate;
            if (AlmostZero (MoveVelocity.z, MoveAcceleration.z * aRate))
            {
                MoveVelocity.z     = 0;
                MoveAcceleration.z = 0;
            }

            _transform.position += MoveVelocity * vRate;
        }

        private bool AlmostZero (float value, float precision)
        {
            if (value < 0)
                value = -value;
            return value < precision;
        }

    }
}