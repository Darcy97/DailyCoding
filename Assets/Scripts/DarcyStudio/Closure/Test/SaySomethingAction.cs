/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 15:01:50
 * Description: Say some thing
 ***/

using DarcyStudio.Closure.ReusableAction;
using UnityEngine;

namespace DarcyStudio.Closure.Test
{
    public class SaySomethingAction : VariableCapture<string>, IAction<string>
    {

        private static int _curID = -1;

        private int    _number;

        private readonly int _serialID; //用来记录序列 ID 验证是否重用 Action  

        public SaySomethingAction ()
        {
            _serialID =  _curID + 1;
            _curID    += 1;
        }

        public void Invoke (string arg)
        {
            Debug.Log ($"Say: {Arg} to: {arg}     -- Serial id: {_serialID}");
        }
    }
}