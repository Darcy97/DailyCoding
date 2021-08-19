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
using DarcyStudio.GameComponent.TimeLine.Skill;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{

    public class TestSkillTimeline : MonoBehaviour, IObjectProvider
    {

        [SerializeField] private GameObject self;
        [SerializeField] private GameObject enemy1;
        [SerializeField] private GameObject enemy2;
        [SerializeField] private GameObject enemy3;
        [SerializeField] private GameObject enemy4;
        [SerializeField] private GameObject enemy5;

        [SerializeField] private SkillTimeline skillTimeline;

        public void Play ()
        {
            skillTimeline.Init ();
            skillTimeline.SetObjectProvider (this);
            skillTimeline.Play (OnPlayFinish);
            // _skillTimeline.
        }

        private void OnPlayFinish (ISkillPlayer skillTimelineNew)
        {
            Log.Info ("Play finish");
            skillTimeline.Stop ();
        }

        public IObject Get (ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Specify:
                    return InvalidObject.Default;
                case ObjectType.Self:
                    return self ? self.GetComponent<IObject> () : InvalidObject.Default;
                case ObjectType.Enemy1:
                    return enemy1 ? enemy1.GetComponent<IObject> () : InvalidObject.Default;
                case ObjectType.Enemy2:
                    return enemy2 ? enemy2.GetComponent<IObject> () : InvalidObject.Default;
                case ObjectType.Enemy3:
                    return enemy3 ? enemy3.GetComponent<IObject> () : InvalidObject.Default;
                case ObjectType.Enemy4:
                    return enemy4 ? enemy4.GetComponent<IObject> () : InvalidObject.Default;
                case ObjectType.Enemy5:
                    return enemy5 ? enemy5.GetComponent<IObject> () : InvalidObject.Default;
                default:
                    return InvalidObject.Default;
            }
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

    // [Serializable]
    // public class TargetObjectInfo
    // {
    //     public                   ObjectType ObjectType;
    //     [SerializeField] private GameObject go;
    //
    //     public IObject GetObject ()
    //     {
    //         return go ? go.GetComponent<IObject> () : InvalidObject.Default;
    //     }
    //
    //
    // }
}