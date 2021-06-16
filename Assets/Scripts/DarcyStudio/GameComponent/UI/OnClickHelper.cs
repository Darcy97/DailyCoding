/***
 * Created by Darcy
 * Date: Wednesday, 16 June 2021
 * Time: 20:27:05
 * Description:
 * 由于继承自 ScrollRect 或其他有自定义 Inspector 的类
 * Inspector 无法展示其他序列化成员，
 * 所以实现该类，目的是为了在 Inspector 绑定点击事件
 ***/

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DarcyStudio.GameComponent.UI
{
    
    [AddComponentMenu ("UGUI Extension/On Click Helper")]
    public class OnClickHelper : MonoBehaviour
    {
        [FormerlySerializedAs ("onClick")] [SerializeField]
        private ScrollRectClickedEvent m_OnClick = new ScrollRectClickedEvent ();

        /// <summary>
        /// UnityEvent that is triggered when the onclick is call.
        /// </summary>
        public ScrollRectClickedEvent onClick
        {
            get => m_OnClick;
            set => m_OnClick = value;
        }

        public void OnClick ()
        {
            m_OnClick.Invoke ();
        }

        [Serializable]
        public class ScrollRectClickedEvent : UnityEvent
        {
        }
    }

}