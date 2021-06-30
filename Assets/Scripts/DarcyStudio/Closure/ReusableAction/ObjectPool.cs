/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 14:23:43
 * Description: Description
 ***/

#define DEBUG_LOG

using System.Collections.Generic;
using UnityEngine;

namespace DarcyStudio.Closure.ReusableAction
{
    public class ObjectPool<T> where T : new ()
    {
        private readonly List<T> _objects;
        private readonly int     _maxCapacity;

        /// <summary>
        /// 默认初始化容量 4
        /// 默认最大容量 128
        /// 超过容量的不回收
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="maxCapacity"></param>
        public ObjectPool (int capacity = 4, int maxCapacity = 128)
        {
            _objects     = new List<T> (capacity);
            _maxCapacity = maxCapacity;
        }

        /// <summary>
        /// 取出一个 T 
        /// </summary>
        /// <returns></returns>
        public T Pop ()
        {
            while (_objects.Count > 0)
            {
                var endIndex = _objects.Count - 1;
                var obj      = _objects[endIndex];

                // 这里为什么要取最后一个呢，因为只有取最后一个才是效率最高的
                // 因为数组数据是连续的，所以如果从中间取或者取最后一个，需要将后面所有元素向前移动一位，从 C# 反编译后的源码来看也是这样
                // 其实数据量小的时候差别几乎可以忽略，不过总是要有一点点追求😬
                // 有时间可以简单测试下 unity 编辑器内测试了下 见 DarcyStudio.StudyNotes.CSharpInDepth.ArrayDepth.ArrayDepthTest

                // 这部分是 Rider 反编译的源码 从下面第六行可以看出来 如果移除的不是最后一个元素
                // 会进行一次 Array.copy 没有仔细研究里面的逻辑，底层应该是有优化的，不过肯定效率会低一点
                // public void RemoveAt(int index)
                // {
                //     if ((uint) index >= (uint) this._size)
                //         ThrowHelper.ThrowArgumentOutOfRangeException();
                //     --this._size;
                //     if (index < this._size)
                //         Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
                //     this._items[this._size] = default (T);
                //     ++this._version;
                // }
                
                _objects.RemoveAt (endIndex);
                if (obj == null)
                {
                    continue;
                }

                return obj;
            }

            return new T ();
        }

        /// <summary>
        /// Push 时会判断最大容量
        /// 超过最大容量不回收
        /// </summary>
        /// <param name="action"></param>
        public void Push (T action)
        {
            if (action == null)
                return;

            if (_objects.Count >= _maxCapacity)
            {
#if DEBUG_LOG
                Debug.LogError ("Maximum capacity exceeded, check if there is a problem");
#endif
                return;
            }

            _objects.Add (action);
        }

        public void Clear ()
        {
            _objects.Clear ();
        }
    }
}