/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月23日 星期一
 * Time: 下午3:13:59
 ***/

using System;
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

        [NonSerialized] public IPerformer Performer;
        [NonSerialized] public ActionInfo ActionInfo;

        public static PerformData Animation (string key, bool waitDone)
        {
            return new PerformData {animationKey = key, waitDone = waitDone};
        }
    }

    [Serializable]
    public enum PerformType
    {
        Default,
        Animation,
        ShowGo,
    }
}