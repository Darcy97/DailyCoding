/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:23:31
 * Description: 
 ***/

using System;
using System.Collections.Generic;
using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.WorkState;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class TriggerActionPlayableAsset : ObjectDemandPlayableAsset, IRequireWaitDone
    {
        // [SerializeField] private List<ActionInfo> actionPairs;
        
        [SerializeField] private ActionData actionData;
        // [SerializeField] private ActionType actionType;
        [SerializeField] private bool       needWaitDone;

        protected override ObjectDemandPlayableBehaviour CreateBehaviour ()
        {
            var bhv = new TriggerActionPlayableBehaviour (actionData);
            bhv.SetWaitDoneListener (_workStateListener);
            return bhv;
        }

        protected override Playable CreatePlayable (ObjectDemandPlayableBehaviour bhv, PlayableGraph graph) =>
            ScriptPlayable<TriggerActionPlayableBehaviour>.Create (graph, bhv as TriggerActionPlayableBehaviour);


        private IWorkStateListener _workStateListener;

        public bool Require () => needWaitDone;

        public void SetWaitDoneListener (IWorkStateListener listener)
        {
            _workStateListener = listener;
        }
    }
}