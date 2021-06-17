/***
 * Created by Darcy
 * Date: Thursday, 17 June 2021
 * Time: 21:47:44
 * Description: https://forum.unity.com/threads/nested-scrollrect.268551/
 * 借鉴大佬的做法完成该脚本
 * 处理 ScrollView 嵌套的问题
 * 当一个只可以竖着滑动的滚动列表里面放了一个只可以横向滑动的列表时，当触摸区域在横向列表时，会阻断 UI 事件，无法滑动上一层的竖向列表
 * 使用该脚本可以解决这个问题，原理是判断手指 横向和竖向滑动幅度，如果方向符合最上层（UI 表现上 上面的）的滚动列表，则当前列表处理事件，反之将事件继续传递，直到符合条件
 * 未测试多层嵌套，有时间试一下
 * 这里这个 DoForParents 非常有意思 值得学习
 * Edit by darcy 
 ***/

using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace DarcyStudio.GameComponent.UI
{
    public class SuperScrollRect : ScrollRect
    {

        private bool routeToParent;


        /// <summary>
        /// Do for parents
        /// </summary>
        /// <param name="action"></param>
        /// <param name="onlyForFirstParent">是否只传递给第一个符合条件的 parent </param>
        /// <typeparam name="T"></typeparam>
        private void DoForParents<T> (Action<T> action, bool onlyForFirstParent = false) where T : IEventSystemHandler
        {
            var parent = transform.parent;
            while (parent != null)
            {
                foreach (var component in parent.GetComponents<Component> ())
                {
                    if (!(component is T))
                        continue;
                    action ((T) (IEventSystemHandler) component);

                    // //理论上只传递给第一个符合条件的parent就足够了
                    // if (onlyForFirstParent)
                    //     return;
                }

                parent = parent.parent;
            }
        }

        /// <summary>
        /// Always route initialize potential drag event to parents
        /// </summary>
        public override void OnInitializePotentialDrag (PointerEventData eventData)
        {
            DoForParents<IInitializePotentialDragHandler> (parent => { parent.OnInitializePotentialDrag (eventData); },
                true);
            base.OnInitializePotentialDrag (eventData);
        }

        /// <summary>
        /// Drag event
        /// </summary>
        public override void OnDrag (PointerEventData eventData)
        {
            if (routeToParent)
                DoForParents<IDragHandler> (parent => { parent.OnDrag (eventData); }, true);
            else
                base.OnDrag (eventData);
        }

        /// <summary>
        /// Begin drag event
        /// </summary>
        public override void OnBeginDrag (PointerEventData eventData)
        {
            if (!horizontal && Math.Abs (eventData.delta.x) > Math.Abs (eventData.delta.y))
                routeToParent = true;
            else if (!vertical && Math.Abs (eventData.delta.x) < Math.Abs (eventData.delta.y))
                routeToParent = true;
            else
                routeToParent = false;

            if (routeToParent)
                DoForParents<IBeginDragHandler> (parent => { parent.OnBeginDrag (eventData); }, true);
            else
                base.OnBeginDrag (eventData);
        }

        /// <summary>
        /// End drag event
        /// </summary>
        public override void OnEndDrag (PointerEventData eventData)
        {
            if (routeToParent)
                DoForParents<IEndDragHandler> (parent => { parent.OnEndDrag (eventData); }, true);
            else
                base.OnEndDrag (eventData);
            routeToParent = false;
        }
    }
}