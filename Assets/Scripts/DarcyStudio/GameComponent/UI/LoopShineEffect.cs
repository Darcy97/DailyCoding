/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 11 August 2021
 * Time: 20:43:54
 * Description: 自动循环扫光脚本
 * 这个效果是通过 Shader 实现的 Channel 为 TexCoord1 TexCoord2 TexCoord3
 * 所以 Canvas Additional Shader Channels 别忘了勾选，不然看不到效果
 ***/

using Coffee.UIExtensions;
using DarcyStudio.CustomEditor.Attribute.CustomPropertyAttribute;
using DarcyStudio.GameComponent.Attribute.SortingLayerDrawerAttribute;
using UnityEngine;

namespace DarcyStudio.GameComponent.UI
{
    [RequireComponent (typeof (ShinyEffectForUGUI))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class LoopShineEffect : MonoBehaviour
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {

        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private bool           loop;

#if UNITY_EDITOR
        [Header ("编辑器模式是否运行 编辑模式测试时可以勾选上")] [SerializeField]
        private bool runInEditorMode = false;
#endif


        [SerializeField] [DisableEdit] private float totalTime = 1f;

        [SerializeField] [DisableEdit] private ShinyEffectForUGUI shinyEffect;

        private float _time;

        private void OnEnable ()
        {
            if (loop)
                return;
            _time = 0;
        }

        private void Update ()
        {
            if (!loop && _time > totalTime)
                return;

            if (!CanRun ())
                return;

            if (_time > totalTime)
                _time = 0;

            var value = animationCurve.Evaluate (_time);
            SetShinyLocation (value);

            _time += Time.deltaTime;
        }

        private bool CanRun ()
        {
#if UNITY_EDITOR
            return Application.isPlaying || runInEditorMode;
#endif
            return true;
        }

        private void SetShinyLocation (float location)
        {
            shinyEffect.location = location;
        }

        #region For editor

#if UNITY_EDITOR

        public void OnBeforeSerialize ()
        {
            CalculateCurveTime ();
            SetShinyEffect ();
        }

        private void CalculateCurveTime ()
        {
            if (animationCurve == null || animationCurve.length < 1)
                return;
            totalTime = animationCurve[animationCurve.length - 1].time;
        }

        private void SetShinyEffect ()
        {
            if (shinyEffect)
                return;
            shinyEffect = GetComponent<ShinyEffectForUGUI> ();
        }

        public void OnAfterDeserialize ()
        {
        }
#endif

        #endregion

        public void Restart ()
        {
            _time = 0;
        }
    }
}