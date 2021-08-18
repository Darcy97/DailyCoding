/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:02:28
 * Description: 用于测试
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Timeline;

namespace DarcyStudio.GameComponent.TimeLine
{

    public class TestSkillTimeline : MonoBehaviour, IObjectProvider
    {
        public List<TargetObjectInfo> infos;

        [SerializeField] private TimelineAsset skillTimelineAsset;

        private SkillTimelineNew _skillTimeline;

        public void Play ()
        {
            _skillTimeline = new SkillTimelineNew (gameObject, skillTimelineAsset);
            _skillTimeline.SetObjectProvider (this);
            _skillTimeline.Play ();
            // _skillTimeline.
        }

        public IObject Get (ObjectType objectType)
        {
            var findInfo = infos?.Find (info => info.ObjectType == objectType);
            return findInfo?.GetObject () ?? InvalidObject.Default;
        }

        public void DrawButtons ()
        {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button ("Play"))
                Play ();

            GUI.backgroundColor = color;
        }
    }

    [Serializable]
    public class TargetObjectInfo
    {
        public                   ObjectType ObjectType;
        [SerializeField] private GameObject go;

        public IObject GetObject ()
        {
            return go ? go.GetComponent<IObject> () : InvalidObject.Default;
        }


    }
}