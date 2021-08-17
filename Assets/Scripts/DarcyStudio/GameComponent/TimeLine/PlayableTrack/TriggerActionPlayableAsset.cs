/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:23:31
 * Description: 
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAnimation;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class TriggerActionPlayableAsset : PlayableAsset, IRequireObject, IRequireWaitDone
    {
        [SerializeField] private ObjectType requireObjectType;
        [SerializeField] private ActionType actionType;

        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var bhv = new TriggerActionPlayableBehaviour (_target, actionType);
            bhv.SetWaitDoneListener (_waitDoneListener);
            return ScriptPlayable<TriggerActionPlayableBehaviour>.Create (graph, bhv);
        }

        private GameObject        _target;
        private IWaitDoneListener _waitDoneListener;

        public void SetObject (GameObject go)
        {
            _target = go;
        }

        public ObjectType GetRequireType () => requireObjectType;

        public void SetWaitDoneListener (IWaitDoneListener listener)
        {
            _waitDoneListener = listener;
        }
    }
}