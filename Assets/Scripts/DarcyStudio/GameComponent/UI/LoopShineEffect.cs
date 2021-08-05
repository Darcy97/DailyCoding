/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月5日 星期四
 * Time: 下午9:08:41
 * Description: 封装一下插件 自动扫光
 * 这个效果是通过 Shader 实现的 Channel 为 TexCoord1 TexCoord2 TexCoord3
 * 所以 Canvas Additional Shader Channels 别忘了勾选，不然看不到效果
 ***/

using Coffee.UIExtensions;
using UnityEngine;

namespace DarcyStudio.GameComponent.UI
{
    public class LoopShineEffect : ShinyEffectForUGUI
    {
        [SerializeField] private float loopTime = 1f;

        [Header("编辑器模式是否运行 编辑模式测试时可以勾选上")]
        [SerializeField] private bool  runInEditorMode = false;
        private                  float _time;

        private void Update ()
        {
            if (!Application.isPlaying && !runInEditorMode)
                return;

            _time += Time.deltaTime;
            if (_time > loopTime)
                _time = 0;

            location = _time / loopTime;
        }
    }
}