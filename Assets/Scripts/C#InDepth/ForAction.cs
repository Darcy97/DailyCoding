/***
 * Created by Darcy
 * Date: Friday, 04 June 2021
 * Time: 18:38:13
 * Description: Description
 ***/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarcyStudio
{
    public class ForAction : MonoBehaviour
    {

        private readonly StringActionPool _actionPool = new StringActionPool ();

        private void Start ()
        {
            StartCoroutine (TestActionPool ());
        }

        private IEnumerator TestActionPool ()
        {
            TestLowGcAction ("Hello Darcy", 0.2f); //serial 2
            TestLowGcAction ("Hello John",  0.1f); //serial 1
            TestLowGcAction ("Hello Lucy",  0.4f); // serial 3
            yield return new WaitForSeconds (0.11f);
            TestLowGcAction ("Hello Mike", 0.5f); //serial 1
        }

        private void TestLowGcAction (string para, float waitTime)
        {
            var action = _actionPool.Pop ();
            action.SetPara (para);
            Method (action, waitTime);
        }

        private void Method (StringAction action, float waitTime)
        {
            StartCoroutine (DelayDo (action, waitTime));
        }

        private IEnumerator DelayDo (StringAction action, float waitTime)
        {
            yield return new WaitForSeconds (waitTime);
            action?.Invoke ();
            _actionPool.Push (action);
        }

        private void Func (Action<string> action)
        {
            action?.Invoke ("Hello");
        }
    }

    public class StringActionPool
    {

        private readonly Stack<StringAction> _stack = new Stack<StringAction> (4);

        public StringAction Pop ()
        {
            return _stack.Count > 0 ? _stack.Pop () : new StringAction ();
        }

        public void Push (StringAction action)
        {
            if (action == null)
                return;
            _stack.Push (action);
        }
    }


    public class StringAction
    {

        private static int _curID = -1;

        private string para;
        private int    _number;

        private readonly int _serialID; //用来记录序列 ID 验证是否重用 Action 

        public StringAction ()
        {
            _serialID =  _curID + 1;
            _curID    += 1;
        }

        public void SetPara (string str)
        {
            para = str;
        }

        public void Invoke ()
        {
            Debug.Log ($"Say: {para} -- Serial id: {_serialID}");
        }
    }
}