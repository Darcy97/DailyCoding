/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:23:31
 * Description: 
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class TriggerActionPlayableAsset : ObjectDemandPlayableAsset, IRequireWaitDone
    {
        [SerializeField] private ActionType actionType;
        [SerializeField] private bool       needWaitDone;

        protected override ObjectDemandPlayableBehaviour CreateBehaviour ()
        {
            var bhv = new TriggerActionPlayableBehaviour (actionType);
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