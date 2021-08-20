/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月20日 星期五
 * Time: 下午6:22:12
 ***/


using System;
using System.Collections;
using UnityEngine;

namespace DarcyStudio.GameComponent.Tools
{

    internal static partial class YieldUtils
    {
        private class CoroutineUtil : MonoBehaviour
        {
            private static CoroutineUtil _instance;

            private static CoroutineUtil Instance
            {
                get
                {
                    if (_instance != null) 
                        return _instance;
                    
                    _instance = new GameObject ("YieldUtil", typeof (CoroutineUtil)).GetComponent<CoroutineUtil> ();
                    var go = _instance.gameObject;
                    go.hideFlags = HideFlags.HideAndDontSave;
                    DontDestroyOnLoad (go);

                    return _instance;
                }
            }

            public static Coroutine NStartCoroutine (IEnumerator cor)
            {
                return Instance.StartCoroutine (cor);
            }

            public static void NStopCoroutine (Coroutine cor)
            {
                Instance.StopCoroutine (cor);
            }

            private static IEnumerator DelayAction (float delay, Action action)
            {
                yield return YieldUtils.WaitForSeconds(delay);
                action?.Invoke ();
            }

            public static Coroutine DoDelayAction (float delay, Action action)
            {
                return Instance.StartCoroutine (DelayAction (delay, action));
            }

            public static void StopDelayAction (Coroutine coroutine)
            {
                Instance.StopCoroutine (coroutine);
            }

            public static Coroutine DoDelayByContext (MonoBehaviour context, float delay, Action action)
            {
                return context.StartCoroutine (DelayAction (delay, action));
            }

            public static Coroutine DoEndOfFrame (MonoBehaviour context, Action action)
            {
                return context.StartCoroutine (DelayEndOfFrame (action));
            }

            private static IEnumerator DelayEndOfFrame (Action action)
            {
                yield return YieldUtils.EndOfFrame;
                action?.Invoke ();
            }

            #region RealTime

            public static void DoDelayRealTimeAction (float dTime, System.Action callback)
            {
                Instance.StartCoroutine (DelayRealTimeAction (dTime, callback));
            }

            public static void DoRealDelayByContext (MonoBehaviour context, float dTime, System.Action callback)
            {
                context.StartCoroutine (DelayRealTimeAction (dTime, callback));
            }

            public static IEnumerator DelayRealTimeAction (float dTime, System.Action callback)
            {
                yield return YieldUtils.WaitForSecondsRealtime(dTime);
                callback ();
            }

            #endregion

        }
    }
}