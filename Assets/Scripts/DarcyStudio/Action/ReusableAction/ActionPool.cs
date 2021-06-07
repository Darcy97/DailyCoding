/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 14:23:43
 * Description: Description
 ***/

#define DEBUG_LOG

using System.Collections.Generic;
using UnityEngine;

namespace DarcyStudio.Action.ReusableAction
{
    public class ActionPool<T> where T : IAction, new ()
    {
        public ActionPool (int capacity = 4)
        {
            _stack = new Stack<T> (capacity);
        }

        private readonly Stack<T> _stack;

        public T Pop ()
        {
            return _stack.Count > 0 ? _stack.Pop () : new T ();
        }

        public void Push (T action)
        {
            if (action == null)
                return;
            _stack.Push (action);

#if DEBUG_LOG
            if (_stack.Count > 128)
            {
                Debug.LogError ("Too large pool, check if there is a problem");
            }    
#endif
        }
    }
}