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
        private                  GameObject     _relatedGo;
        private                  Vector3        _targetPos;
        private                  Vector3        _sourcePos;
        [SerializeField] private AnimationCurve animationCurve;

        protected override ObjectDemandPlayableBehaviour CreateBehaviour ()
        {
            var bhv = new MoveObjectPlayableBehaviour (animationCurve);
            return bhv;
        }

        protected override Playable CreatePlayable (
            ObjectDemandPlayableBehaviour bhv, PlayableGraph graph)
        {
            return ScriptPlayable<MoveObjectPlayableBehaviour>.Create (graph, bhv as MoveObjectPlayableBehaviour);
        }

    }
}