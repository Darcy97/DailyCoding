/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 16:36:33
 * Description: 用来处理当上层 UI 响应点击事件后，不阻挡该点击事件，能再往下面传递一层
 * 该代码借鉴了网上一些大佬的实现，但是实际业务中发现网上的一些实现方式都不完美，都有漏洞会造成死循环，所以完善了一下，目前使用来说没碰到什么问题
 ***/

using System.Collections.Generic;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DarcyStudio.GameComponent.UI
{


    [DisallowMultipleComponent]
    [AddComponentMenu ("UGUI Extension/Click Through")]
    public class ClickThrough : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private bool passClick = true;

        private void PassEvent<T> (PointerEventData data, ExecuteEvents.EventFunction<T> function)
            where T : IEventSystemHandler
        {
            Log.Info ("Depend Obj: {0}", gameObject.name);
            var results = new List<RaycastResult> ();

            //查了一下源码和api 获取到的这个 List -> results
            //是按照射线检测顺序排序过的
            EventSystem.current.RaycastAll (data, results);

            if (results.Count < 1)
                return;

            var source = data.pointerCurrentRaycast.gameObject;

            var lowerThanCurrent = false;

            foreach (var result in results)
            {
                Log.Info ("ResultGO: {0}", result.gameObject.name);

                #region 层级检测

                // 这部分检测是为了只把事件透传给层级低于该对象的对象
                if (result.gameObject == gameObject)
                {
                    lowerThanCurrent = true;
                    continue;
                }

                if (!lowerThanCurrent)
                    continue;

                #endregion

                //避免事件传递产生死循环，理论上这个判断永远为 false
                if (result.gameObject == source)
                    continue;

                if (ExecuteEvents.Execute (result.gameObject, data, function))
                {
#if UNITY_EDITOR
                    Log.Info ("Pass to: " + result.gameObject.name + " Depend obj: " + gameObject.name);
#endif
                    break;
                }
            }

            //若射线检测列表里没有该脚本所在的 gameObject
            //则会走到这
            //说明射线检测时该go已经关闭，那么直接将事件传递给第一个对象
            if (!lowerThanCurrent)
            {
                ExecuteEvents.Execute (results[0].gameObject, data, function);
            }

            results.Clear ();
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            if (!passClick)
                return;
            PassEvent (eventData, ExecuteEvents.pointerClickHandler);
        }
    }
}