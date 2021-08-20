/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: 2021年8月18日 星期三
 * Time: 下午2:57:42
 ***/

using System;
using DarcyStudio.GameComponent.TimeLine.DemandObject;
using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine.RequireObject
{
    [Serializable]
    public class ObjectDemandInfo
    {
        public DemandType DemandType;

        public ObjectType ObjectType;

        [SerializeField] private GameObject _gameObject;

        private IObject _object;

        public IObject GetObject ()
        {
            if (_object != null)
            {
                return _object;
            }

            if (_gameObject == null)
                return InvalidObject.Default;
            
            _object = _gameObject.GetComponent<IObject> ();
            if (_object == null)
                _gameObject.AddComponent<DemandObject.DemandObject> ();
            return _object = _gameObject.GetComponent<IObject> ();

        }

    }

    public enum DemandType
    {
        Source,
        Target,
        Controlled,
    }
}