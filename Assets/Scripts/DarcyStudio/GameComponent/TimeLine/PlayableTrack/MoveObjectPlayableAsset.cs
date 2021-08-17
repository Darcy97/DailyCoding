/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月16日 星期一
 * Time: 下午3:44:01
 * Description:
 ***/

using UnityEngine;
using UnityEngine.Playables;

namespace DarcyStudio.GameComponent.TimeLine.PlayableTrack
{

    public class MoveObjectPlayableAsset : PlayableAsset, IRequireTarget, IRequireSource, IRequireControlledObject
    {
        private                  GameObject     _relatedGo;
        private                  Vector3        _targetPos;
        private                  Vector3        _sourcePos;
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private int            targetIndex;


        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var bhv = new MoveObjectPlayableBehaviour (_relatedGo.transform, _sourcePos, _targetPos, animationCurve);
            return ScriptPlayable<MoveObjectPlayableBehaviour>.Create (graph, bhv);
        }

        public void SetSource (GameObject go)
        {
            _sourcePos = go.transform.position;
        }

        public void SetTarget (GameObject go)
        {
            _targetPos = go.transform.position;
        }

        public int GetTargetIndex () => targetIndex; // TODO // 

        public void SetControlled (GameObject go)
        {
            _relatedGo = go;
        }
    }
}