/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:44:01
 * Description:
 ***/

using DarcyStudio.GameComponent.TimeLine.RequireObject;
using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{

    public class MoveObjectPlayableAsset : ObjectDemandPlayableAsset
    {
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear (0, 0, 1, 1);
        [SerializeField] private float          delayDisappearTime = 0;

        protected override ObjectDemandPlayableBehaviour CreateBehaviour ()
        {
            var bhv = new MoveObjectPlayableBehaviour (animationCurve, delayDisappearTime);
            return bhv;
        }

        protected override Playable CreatePlayable (
            ObjectDemandPlayableBehaviour bhv, PlayableGraph graph)
        {
            return ScriptPlayable<MoveObjectPlayableBehaviour>.Create (graph, bhv as MoveObjectPlayableBehaviour);
        }

    }
}