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
    public class ObjectPool<T> where T : new ()
    {
        private readonly Stack<T> _stack;
        private readonly int      _maxCapacity;

        /// <summary>
        /// 默认初始化容量 4
        /// 默认最大容量 128
        /// 超过容量的回收时不入栈
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="maxCapacity"></param>
        public ObjectPool (int capacity = 4, int maxCapacity = 128)
        {
            _stack       = new Stack<T> (capacity);
            _maxCapacity = maxCapacity;
        }

        /// <summary>
        /// 取出一个 T 
        /// </summary>
        /// <returns></returns>
        public T Pop ()
        {
            return _stack.Count > 0 ? _stack.Pop () : new T ();
        }

        /// <summary>
        /// Push 时会判断最大容量
        /// 超过最大容量不入栈
        /// </summary>
        /// <param name="action"></param>
        public void Push (T action)
        {
            if (action == null)
                return;

            if (_stack.Count >= _maxCapacity)
            {
#if DEBUG_LOG
                Debug.LogError ("Maximum capacity exceeded, check if there is a problem");
#endif
                return;
            }

            _stack.Push (action);
        }

        public void Clear ()
        {
            _stack.Clear ();
        }
    }
}