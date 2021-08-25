/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月17日 星期二
 * Time: 下午8:27:18
 ***/

using DarcyStudio.GameComponent.TimeLine.ForAction;
using DarcyStudio.GameComponent.TimeLine.ForAction.Receiver;
using DarcyStudio.GameComponent.TimeLine.ForAction.Sender;
using DarcyStudio.GameComponent.TimeLine.RequireObject;
using DarcyStudio.GameComponent.TimeLine.WorkState;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{
    public class TriggerActionPlayableBehaviour : ObjectDemandPlayableBehaviour
    {
        public TriggerActionPlayableBehaviour ()
        {
        }

        private          GameObject      _go;
        private          IActionReceiver _receiver;
        private readonly ActionData      _actionData;

        public TriggerActionPlayableBehaviour (ActionData actionData)
        {
            _actionData = actionData;
        }

        public override void OnGraphStart (Playable playable)
        {
            base.OnGraphStart (playable);

            if (!IsPlaying ())
                return;

            _go       = GetObject (DemandType.Controlled).GetGameObject ();
            _receiver = _go.GetComponent<IActionReceiver> ();
        }

        public override void OnBehaviourPlay (Playable playable, FrameData info)
        {
            base.OnBehaviourPlay (playable, info);
            Play ();
        }

        private void Play ()
        {
            _workStateListener?.StartWorking ();
            _receiver.Do (_actionData, OnWorkDone);
        }

        private void OnWorkDone ()
        {
            _workStateListener?.OnWorkDone ();
        }

        private IWorkStateListener _workStateListener;

        public void SetWaitDoneListener (IWorkStateListener listener)
        {
            _workStateListener = listener;
        }
    }
}