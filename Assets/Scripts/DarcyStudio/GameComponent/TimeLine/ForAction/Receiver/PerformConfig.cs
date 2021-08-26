/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午3:13:59
 ***/

using System;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    [Serializable]
    public struct PerformConfig
    {
        public PerformType performType;

        public string     animationKey;
        public GameObject go;
        public float      duration;
        public bool       waitDone;

        public Vector3 moveVelocity;
        public Vector3 moveAcceleration;

        public float k0;

        // public static PerformData Animation (string key, bool waitDone)
        // {
        //     return new PerformData {animationKey = key, waitDone = waitDone};
        // }

        public PerformConfig GetDefaultPerformData (PerformType type)
        {
            switch (type)
            {
                case PerformType.Default:
                    break;
                case PerformType.Animation:
                    break;
                case PerformType.ShowGo:
                    break;
                case PerformType.Move:
                    break;
                case PerformType.ResetPosition:
                    return new PerformConfig {performType = PerformType.ResetPosition};
                default:
                    throw new ArgumentOutOfRangeException (nameof (type), type, null);
            }

            return default;
        }

    }

    [Serializable]
    public enum PerformType
    {
        None,
        Default,
        Animation,
        ShowGo,
        Move,
        ResetPosition,
        Wait,
    }
}