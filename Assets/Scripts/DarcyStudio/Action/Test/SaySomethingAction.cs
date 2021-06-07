/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 15:01:50
 * Description: Say some thing
 ***/

using DarcyStudio.Action.ReusableAction;
using UnityEngine;

namespace DarcyStudio.Action.Test
{
    public class SaySomethingAction : IAction<string>
    {

        private static int _curID = -1;

        private string _para;
        private int    _number;

        private readonly int _serialID; //用来记录序列 ID 验证是否重用 Action  

        public SaySomethingAction ()
        {
            _serialID =  _curID + 1;
            _curID    += 1;
        }

        public void SetPara (string str)
        {
            _para = str;
        }
         
        
        public void Invoke (string arg)
        {
            Debug.Log ($"Say: {_para} to: {arg}     -- Serial id: {_serialID}");
        }
    }
}