/***
 * Created by Darcy
 * Date: Tuesday, 08 June 2021
 * Time: 14:25:55
 * Description: Turntable
 * 具体实现逻辑：
 * 首先配置的贝塞尔曲线终点必须是整数 (之后考虑优化下 去除该限制条件)
 * 终点 value 值为多少就是转多少圈
 * 要求曲线中必须包含匀速阶段（之后考虑优化一下若没有匀速阶段则在中加添加一段）
 * 该段匀速曲线用来做假随机，即停在指定的位置
 * 延长该匀速阶段使停止时刚好可以停在指定的位置
 * 延长匀速阶段也就是将其后面的点都像右上方移动（方向为该匀速阶段的 K 值)
 * 这样就可以刚好停止在指定位置了
 *
 * TODO: 优化实现方式以去除上面的两个限制条件
 ***/


#define TURNTABLE_DEBUG


using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DarcyStudio.GameComponent.Turntable
{
    public class Turntable : MonoBehaviour
    {

        #region External

        /// <summary>
        /// 初始化转盘 也可通过 Inspector 勾选 AutoInit 自动初始化
        /// </summary>
        /// <param name="elementCount">转盘上元素的总数量</param>
        public void InitWheelData (int elementCount) => Internal_InitWheelData (elementCount);

        /// <summary>
        /// 重置转盘到初始状态
        /// </summary>
        public void ResetStatus () => Internal_ResetStatusToOrigin ();

        /// <summary>
        /// 注册转动阶段事件
        /// </summary>
        /// <param name="onSpinStart">开始转的回调</param>
        /// <param name="onSpinMiddle">开始匀速转的回调 即配置中 StartFrameIndex 所指的时刻</param>
        /// <param name="onSpinBeforeEnd">匀速转结束的回调 即配置中 EndFrameIndex 所指的时刻</param>
        public void RegisterSpinStateEvent (Action onSpinStart, Action onSpinMiddle, Action onSpinBeforeEnd) =>
            Internal_RegisterSpinStateEvent (onSpinStart, onSpinMiddle, onSpinBeforeEnd);

        /// <summary>
        /// 注册转动结束事件
        /// </summary>
        /// <param name="action">转动彻底结束的回调</param>
        public void RegisterSpinFinishEvent (Action action) => Internal_RegisterSpinFinishEvent (action);

        /// <summary>
        /// 注册所有事件
        /// </summary>
        /// <param name="onSpinStart">开始转的回调</param>
        /// <param name="onSpinMiddle">开始匀速转的回调 即配置中 StartFrameIndex 所指的时刻</param>
        /// <param name="onSpinBeforeEnd">匀速转结束的回调 即配置中 EndFrameIndex 所指的时刻</param>
        /// <param name="onSpinFinish">转动彻底结束的回调</param>
        public void RegisterSpinEvent (Action onSpinStart, Action onSpinMiddle, Action onSpinBeforeEnd,
            Action onSpinFinish) => Internal_RegisterSpinEvent (onSpinStart, onSpinMiddle, onSpinBeforeEnd,
            onSpinFinish);

        /// <summary>
        /// 开始转
        /// </summary>
        /// <param name="targetIndex">转动停止时 指针所指的元素（指针在最上方时)</param>
        /// <returns></returns>
        public bool StartSpin (int targetIndex) => Internal_StartSpin (targetIndex);

        /// <summary>
        /// 停止转
        /// </summary>
        public void Stop () => Internal_Stop ();

        /// <summary>
        /// 是否在旋转
        /// </summary>
        public bool IsSpinning => isSpinning;

        #endregion

        #region Internal

        [Header ("横轴为时间，曲线曲率为转速 --  曲线终点的 value 值必须为整数，且必须包含匀速阶段，终点的 value 为多少就是转多少圈")] [SerializeField]
        private AnimationCurve animationCurve;

        [Header ("Curve曲线匀速阶段的起始帧索引")] [SerializeField]
        private int startFrameIndex;

        [Header ("Curve曲线匀速阶段的末尾帧索引")] [SerializeField]
        private int endFrameIndex;

        [Header ("True -> 顺时针 ---- False -> 逆时针")] [SerializeField]
        private bool clockwise = true; //是否顺时针转

        [Header ("勾选该选项且设置好 WheelElementCount，会自动初始化，等同于调用 \"InitWheelData()\" ")] [SerializeField]
        private bool autoInit = true;

        [Header ("转盘元素总数")] [SerializeField] private int wheelElementCount = 1;

        private       float anglePerItem;
        private const float RoundAngle = 360;

        private AnimationCurve _finalAnimationCurve;

        private int   endIndex; //停止转动时 转盘上的 0 位置 所在的位置
        private int   preAwardItemIndex;
        private bool  isSpinning;
        private float offsetAngle; //转动的最终角度
        private float preAudioTriggerAngle;

        private Action _spinStart;
        private Action _spinMiddle;
        private Action _spinBeforeEnd;
        private Action _onSpinFinish;

        private Coroutine _spinCoroutine;

        private float curAngleZ = 0;

        private void Internal_ResetStatusToOrigin ()
        {
            isSpinning                 = false;
            endIndex                   = 0;
            preAwardItemIndex          = 0;
            transform.localEulerAngles = Vector3.zero;
            curAngleZ                  = 0;
            offsetAngle                = 0;
            preAudioTriggerAngle       = anglePerItem / 2;
        }

        private void Awake ()
        {
            if (!autoInit)
                return;
            InitData ();
        }

        private void Reset ()
        {
            animationCurve    = AnimationCurve.Constant (0, 10, 2);
            startFrameIndex   = 0;
            endFrameIndex     = 0;
            clockwise         = true;
            autoInit          = true;
            wheelElementCount = 1;
        }

        private void Internal_InitWheelData (int elementCount)
        {
            Internal_ResetStatusToOrigin ();
            wheelElementCount = elementCount;
            InitData ();
        }

        private void Internal_RegisterSpinStateEvent (Action onSpinStart, Action onSpinMiddle, Action onSpinBeforeEnd)
        {
            _spinStart     = onSpinStart;
            _spinMiddle    = onSpinMiddle;
            _spinBeforeEnd = onSpinBeforeEnd;
        }

        private void Internal_RegisterSpinFinishEvent (Action action)
        {
            _onSpinFinish = action;
        }

        private void Internal_RegisterSpinEvent (Action onSpinStart, Action onSpinMiddle, Action onSpinBeforeEnd,
            Action                                      onSpinFinish)
        {
            Internal_RegisterSpinStateEvent (onSpinStart, onSpinMiddle, onSpinBeforeEnd);
            Internal_RegisterSpinFinishEvent (onSpinFinish);
        }

        private void InitData ()
        {
            anglePerItem         = RoundAngle   / wheelElementCount;
            preAudioTriggerAngle = anglePerItem / 2;
            curAngleZ            = transform.eulerAngles.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rewardIndex">停止转动时指针所指的元素的 Index 即停在最上方的元素</param>
        private bool PrepareBeforeRun (int rewardIndex)
        {
            if (rewardIndex >= wheelElementCount)
            {
                LogError ("End index out of range");
                return false;
            }

            endIndex = wheelElementCount - rewardIndex;

            int offsetItem;
            if (endIndex >= preAwardItemIndex)
            {
                offsetItem = endIndex - preAwardItemIndex;
            }
            else
            {
                offsetItem = endIndex + wheelElementCount - preAwardItemIndex;
            }

            offsetAngle = (clockwise ? -1 : 1) * offsetItem * anglePerItem;
            return CreateCurveForSpin ();
        }

        /// <summary>
        /// 根据配置的 Curve 和设置的停止位置 计算旋转所需的 Curve
        /// </summary>
        /// <returns></returns>
        private bool CreateCurveForSpin ()
        {
            if (animationCurve == null)
            {
                LogError ("Curve config null");
                return false;
            }

            if (animationCurve.length     == 0               ||
                animationCurve.length + 2 <= startFrameIndex ||
                animationCurve.length + 1 <= endFrameIndex)
            {
                LogError ("Config error");
                return false;
            }

            #region 生成最终旋转时所用的 Curve

            _finalAnimationCurve = new AnimationCurve ();

            // 1. 匀速阶段之前的部分
            for (var i = 0; i <= startFrameIndex; i++)
            {
                _finalAnimationCurve.AddKey (animationCurve.keys[i]);
            }

            // 2. 根据指定的中奖位置
            //    延长匀速阶段，同时后移后面的所有点
            //    重新生成后面的点添加到 Curve 中
            var startKey = animationCurve.keys[startFrameIndex];
            var endKey   = animationCurve.keys[endFrameIndex];
            var k        = (endKey.value - startKey.value) / (endKey.time - startKey.time); //计算原始速度

            var offsetValue = Mathf.Abs (offsetAngle) / RoundAngle;

            var offsetTime = offsetValue / k;
            for (var i = endFrameIndex; i < animationCurve.keys.Length; i++)
            {
                var keyFrame = animationCurve[i];
                keyFrame.time  += offsetTime;
                keyFrame.value += offsetValue;
                _finalAnimationCurve.AddKey (keyFrame);
            }

            #endregion

            LogCurveInfo ();

            return true;
        }

        [Conditional ("TURNTABLE_DEBUG")]
        private void LogCurveInfo ()
        {
            var index = 0;
            foreach (var item in animationCurve.keys)
            {
                LogError ($"Index: {index}, Time: {item.time}, Value: {item.value}");
                index++;
            }

            LogError ("____________________ Line _____________________");

            index = 0;
            foreach (var item in _finalAnimationCurve.keys)
            {
                LogError ($"Index: {index}, Time: {item.time}, Value: {item.value}");
                index++;
            }
        }

        private bool Internal_StartSpin (int targetIndex)
        {
            var prepare = PrepareBeforeRun (targetIndex);
            if (!prepare)
            {
                LogError ("Prepare fail");
                return false;
            }

            WheelSpin ();
            return true;
        }

        private void WheelSpin ()
        {
            if (isSpinning)
            {
                LogError ("Is spinning");
                return;
            }

            _spinCoroutine = StartCoroutine (SpinWheel ());
        }

        private void Internal_Stop ()
        {
            if (_spinCoroutine == null)
                return;
            StopCoroutine (_spinCoroutine);
            _spinCoroutine = null;
        }

        private IEnumerator SpinWheel ()
        {
            isSpinning = true;
            var timer         = 0.0f;
            var startAngle    = curAngleZ; //transform.eulerAngles.z;
            var endFrameTime  = _finalAnimationCurve[_finalAnimationCurve.length - 1].time;
            var middleTime    = _finalAnimationCurve[startFrameIndex].time;
            var beforeEndTime = _finalAnimationCurve[endFrameIndex].time;

            var isPlayFirstAudio  = true;
            var isPlayMiddleAudio = true;
            var isPlayLastAudio   = true;
            var clockwise         = this.clockwise ? -1 : 1;
            while (timer < endFrameTime)
            {
                var angle = clockwise * RoundAngle * _finalAnimationCurve.Evaluate (timer);
                if (Mathf.Abs (angle) >= preAudioTriggerAngle)
                {
                    //播放每一个转盘上的item转动到指针上面的音效
                    PlayWheelItemTriggerAudio ();
                    preAudioTriggerAngle += anglePerItem;
                }

                curAngleZ = angle + startAngle;
                DoRotate (curAngleZ);

                if (isPlayFirstAudio)
                {
                    OnStartSpin ();
                    isPlayFirstAudio = false;
                }

                if (isPlayMiddleAudio && timer >= middleTime)
                {
                    OnMiddleSpin ();
                    isPlayMiddleAudio = false;
                }

                if (isPlayLastAudio && timer >= beforeEndTime)
                {
                    OnBeforeEndSpin ();
                    isPlayLastAudio = false;
                }

                timer += Time.deltaTime;
                yield return 0;
            }

            var maxAngle = clockwise * RoundAngle * _finalAnimationCurve[_finalAnimationCurve.keys.Length - 1].value;
            curAngleZ = maxAngle + startAngle;
            SetEndState (curAngleZ);
            isSpinning        = false;
            preAwardItemIndex = endIndex;
            OnSpinEnd ();
        }

        private void SetEndState (float endAngle)
        {
            transform.eulerAngles = new Vector3 (0.0f, 0.0f, endAngle);
        }

        private void DoRotate (float endAngle)
        {
            transform.eulerAngles = new Vector3 (0.0f, 0.0f, endAngle);
        }

        private void OnStartSpin ()
        {
            LogInfo ("StartSpin");
            _spinStart?.Invoke ();
        }

        private void OnMiddleSpin ()
        {
            LogInfo ("MiddleSpin");
            _spinMiddle?.Invoke ();
        }

        private void OnBeforeEndSpin ()
        {
            LogInfo ("BeforeEnd");
            _spinBeforeEnd?.Invoke ();
        }

        private void OnSpinEnd ()
        {
            LogInfo ("SpinEnd");
            _onSpinFinish?.Invoke ();
        }

        /// <summary>
        /// 轮子每转动一个 Element 的角度 该方法会被调用一次
        /// 需要播放转动音效可以在这里处理
        /// </summary>
        private static void PlayWheelItemTriggerAudio ()
        {
            LogInfo ("TriggerAudio");
        }

        private const string LogFormat = "CurveWheelController ---> {0}";

        private static void LogInfo (string info)
        {
#if TURNTABLE_DEBUG
            Debug.Log (string.Format (LogFormat, info));
#endif
        }

        private static void LogError (string error)
        {
#if TURNTABLE_DEBUG
            Debug.LogError (string.Format (LogFormat, error));
#endif
        }

        #endregion

    }
}