/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午4:35:00
 * Description: Description
 ***/

using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine
{
    public class TestTimeLine : MonoBehaviour
    {
        [SerializeField] private TimelineAsset asset;

        private void OnEnable ()
        {
            var unit = TimelineUtils.AddTimeline (gameObject, asset);
            unit.SetExtrapolationMode (DirectorWrapMode.Hold);
            unit.SetBinding ("color", gameObject);

            unit.SetRequireGameObjects (end, start, target);
            unit.Play ();
        }

        [SerializeField] private GameObject start;
        [SerializeField] private GameObject end;
        [SerializeField] private GameObject target;
    }
}