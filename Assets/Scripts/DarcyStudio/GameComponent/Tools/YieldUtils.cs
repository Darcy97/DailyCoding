/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月20日 星期五
 * Time: 下午6:22:12
 ***/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarcyStudio.GameComponent.Tools
{
    internal static partial class YieldUtils
    {
        public static WaitForFixedUpdate WaitForFixedUpdate => FixedUpdate;

        public static WaitForEndOfFrame WaitForEndOfFrame => EndOfFrame;

        public static WaitForSeconds WaitForSeconds (float seconds)
        {
            if (!_waitSecondsCache.TryGetValue (seconds, out var wfs))
            {
                if (seconds > 5.0f)
                {
                    Log.Info ($"wait for too long ??? time : {seconds}");
                }

                _waitSecondsCache.Add (seconds, wfs = new WaitForSeconds (seconds));
            }

            return wfs;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime (float seconds)
        {
            if (!_waitSecondsRealTimeCache.TryGetValue (seconds, out var wfs))
            {
                if (seconds > 5.0f)
                {
                    Log.Info ($"wait for real time too long ??? time : {seconds}");
                }

                _waitSecondsRealTimeCache.Add (seconds, wfs = new WaitForSecondsRealtime (seconds));
            }

            return wfs;
        }

        /// <summary>
        /// 使用协程的方式做延时
        /// 最好使用带context这个
        /// 方便管理，生命周期可控
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="delay">delay time</param>
        /// <param name="action">action</param>
        /// <returns>协程句柄</returns>
        public static Coroutine DelayAction (MonoBehaviour context, Action action, float delay)
        {
            if (action != null)
                return CoroutineUtil.DoDelayByContext (context, delay, action);
            Log.Error ("YieldUtils -> DelayAction Para null ...... Context: " +
                       (context == null ? "null" : context.name));
            return null;
        }

        public static void StopDelayAction (MonoBehaviour context, Coroutine coroutine)
        {
            if (coroutine == null)
            {
                Log.Error ("YieldUtils -> StopDelay: coroutine null Context: " + context.name);
                return;
            }

            context.StopCoroutine (coroutine);
        }

        public static Coroutine DoEndOfFrame (MonoBehaviour context, Action action)
        {
            if (action != null)
                return CoroutineUtil.DoEndOfFrame (context, action);
            Log.Error ("YieldUtils -> DelayAction Para null ...... Context: " +
                       (context == null ? "null" : context.name));
            return null;
        }

        /// <summary>
        /// ⚠️Mono脚本最好不要用该方法，除非你清楚你在做什么
        /// ⚠️注意确保生命周期可控
        /// 非Mono脚本可以调用该方法来启动协程
        /// 调用StopDelay来终止
        /// </summary>
        /// <param name="delay">delay time</param>
        /// <param name="action">action</param>
        /// <returns>协程句柄</returns>
        public static Coroutine DelayActionWithOutContext (Action action, float delay)
        {
            return CoroutineUtil.DoDelayAction (delay, action);
        }

        /// <summary>
        /// 终止由⬇️
        /// DelayActionWithOutContext
        /// 启动的协程 
        /// </summary>
        /// <param name="coroutine">协程句柄</param>
        public static void StopDelayActionWithOutContext (Coroutine coroutine)
        {
            CoroutineUtil.StopDelayAction (coroutine);
        }

        public static Coroutine StartCoroutine (IEnumerator cor)
        {
            return CoroutineUtil.NStartCoroutine (cor);
        }

        public static void StopCoroutine (Coroutine cor)
        {
            CoroutineUtil.NStopCoroutine (cor);
        }

        // 内部缓存变量
        private static readonly WaitForEndOfFrame  EndOfFrame  = new WaitForEndOfFrame ();
        private static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate ();

        private static readonly Dictionary<float, WaitForSeconds> _waitSecondsCache =
            new Dictionary<float, WaitForSeconds> (256, new FloatComparer ());

        private static readonly Dictionary<float, WaitForSecondsRealtime> _waitSecondsRealTimeCache =
            new Dictionary<float, WaitForSecondsRealtime> (256, new FloatComparer ());
    }

    internal class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals (float x, float y)
        {
            var dis = x - y;
            dis = dis > 0 ? dis : -dis;

            // 因为我们fps=60,所以小于0.015s这个精度已经就意义不大了，1/0.015 > 60 
            // 或者简单来讲，对于相对时间，wait 0.014s和wait 0s，都会是在下一帧处理
            return dis < 0.015f;
        }

        int IEqualityComparer<float>.GetHashCode (float obj)
        {
            return obj.GetHashCode ();
        }
    }
}