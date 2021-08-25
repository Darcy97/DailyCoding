/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午3:13:59
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.ForAction.Receiver
{
    [Serializable]
    public class PerformData
    {
        public PerformType performType;
        public string      animationKey;
        public GameObject  go;
        public float       duration = 1;
        public bool        waitDone;

        public Vector3 moveVelocity;
        public Vector3 moveAcceleration = new Vector3 (0, 2, 0);

        [NonSerialized] public IPerformer Performer;
        [NonSerialized] public ActionInfo ActionInfo;

        // public static PerformData Animation (string key, bool waitDone)
        // {
        //     return new PerformData {animationKey = key, waitDone = waitDone};
        // }

        public PerformData GetDefaultPerformData (PerformType type)
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
                    return new PerformData {performType = PerformType.ResetPosition};
                default:
                    throw new ArgumentOutOfRangeException (nameof (type), type, null);
            }

            return null;
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
    }
}