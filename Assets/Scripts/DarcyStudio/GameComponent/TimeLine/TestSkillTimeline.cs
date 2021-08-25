/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午6:02:28
 * Description: 用于测试
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.Actor;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.Skill;
using DarcyStudio.GameComponent.Tools;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{

    public class TestSkillTimeline : MonoBehaviour, IObjectProvider
    {

        [SerializeField] private ActorObject self;
        [SerializeField] private ActorObject enemy1;
        [SerializeField] private ActorObject enemy2;
        [SerializeField] private ActorObject enemy3;
        [SerializeField] private ActorObject enemy4;
        [SerializeField] private ActorObject enemy5;

        [SerializeField] private SkillTimeline skillTimeline;

        private void Awake ()
        {
            InitObject (self, Direction.FaceToRight);

            InitObject (enemy1, Direction.FaceToLeft);
            InitObject (enemy2, Direction.FaceToLeft);
            InitObject (enemy3, Direction.FaceToLeft);
            InitObject (enemy4, Direction.FaceToLeft);
            InitObject (enemy5, Direction.FaceToLeft);
        }

        private void InitObject (ActorObject obj, Direction direction)
        {
            if (!obj)
                return;
            obj.SetDirection (direction);
            obj.SetStatus (ActionType.Idle);
        }

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
                {
                    return self is IObject iObject ? iObject : InvalidObject.Default;
                }
                case ObjectType.Enemy1:
                {
                    return enemy1 is IObject iObject ? iObject : InvalidObject.Default;
                }
                case ObjectType.Enemy2:
                {
                    return enemy2 is IObject iObject ? iObject : InvalidObject.Default;
                }
                case ObjectType.Enemy3:
                {
                    return enemy3 is IObject iObject ? iObject : InvalidObject.Default;
                }
                case ObjectType.Enemy4:
                {
                    return enemy4 is IObject iObject ? iObject : InvalidObject.Default;
                }
                case ObjectType.Enemy5:
                {
                    return enemy5 is IObject iObject ? iObject : InvalidObject.Default;
                }
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