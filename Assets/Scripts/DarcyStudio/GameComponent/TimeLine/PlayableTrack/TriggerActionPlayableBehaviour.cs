/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:27:18
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAnimation;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class TriggerActionPlayableBehaviour : PlayableBehaviour, IActionTrigger, IRequireWaitDone
    {
        public TriggerActionPlayableBehaviour ()
        {
        }

        private readonly GameObject      _go;
        private readonly IActionReceiver _receiver;
        private readonly ActionType      _actionType;

        public TriggerActionPlayableBehaviour (GameObject go, ActionType actionType)
        {
            _go       = go;
            _receiver = _go.GetComponent<IActionReceiver> ();
            _receiver.RegisterTrigger (this);
            _actionType = actionType;
        }

        public override void OnGraphStart (Playable playable)
        {
            base.OnGraphStart (playable);
            Play ();
        }

        private void Play ()
        {
            _receiver.Do (_actionType);
        }

        public void OnPlayEnd ()
        {
            _waitDoneListener.NotifyDone ();
        }

        private IWaitDoneListener _waitDoneListener;

        public void SetWaitDoneListener (IWaitDoneListener listener)
        {
            _waitDoneListener = listener;
        }
    }
}