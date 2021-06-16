/***
 * Created by Darcy
 * Date: Wednesday, 16 June 2021
 * Time: 20:30:47
 * Description: 使一个滑动列表即可以滑动又可以响应点击
 ***/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarcyStudio.GameComponent.UI
{
    [DisallowMultipleComponent]
    [RequireComponent (typeof (OnClickHelper))]
    [AddComponentMenu ("UGUI Extension/Scroll Rect Can Click")]
    public class ScrollRectCanClick : ScrollRect, IPointerClickHandler
    {

        protected override void Awake ()
        {
            base.Awake ();
            _mClickHelper = GetComponent<OnClickHelper> ();
        }

        private OnClickHelper _mClickHelper;

        private bool _beingDragged;

        public override void OnDrag (PointerEventData eventData)
        {
            base.OnDrag (eventData);
            _beingDragged = true;
        }

        public override void OnEndDrag (PointerEventData eventData)
        {
            base.OnEndDrag (eventData);
            _beingDragged = false;
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            if (!_beingDragged)
                _mClickHelper.OnClick ();
        }
    }
}