/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 30 June 2021
 * Time: 15:40:05
 * Description:
 * 因为数组数据是连续的，所以如果从中间取或者取最后一个，需要将后面所有元素向前移动一位，从 C# 反编译后的源码来看也是这样
 * 其实数据量小的时候差别几乎可以忽略，不过总是要有一点点追求😬
 *
 * 这部分是 Rider 反编译的源码 从下面第六行可以看出来 如果移除的不是最后一个元素
 * 会进行一次 Array.copy 没有仔细研究里面的逻辑，底层应该是有优化的，不过肯定效率会低一点
 * public void RemoveAt(int index)
 * {
 *   if ((uint) index >= (uint) this._size)
 *      ThrowHelper.ThrowArgumentOutOfRangeException();
 *   --this._size;
 *   if (index < this._size) // 这一行判断移除的元素的 Index 是不是最后一个 如果不是则要将后面元素全体向前移动
 *      Array.Copy((Array) this._items, index + 1, (Array) this._items, index, this._size - index);
 *   this._items[this._size] = default (T);
 *   ++this._version;
 * }
 ***/

using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace DarcyStudio.StudyNotes.CSharpInDepth.ArrayDepth
{
    public class ArrayDepthTest : MonoBehaviour
    {
        private ProfilerMarker _profilerMarkerRemoveFirst = new ProfilerMarker ("Array.Remove first");

        private ProfilerMarker _profilerMarkerRemoveLast = new ProfilerMarker ("Array.Remove last");

        private ProfilerMarker _profilerMarkerRemoveMiddle = new ProfilerMarker ("Array.Remove middle");

        [SerializeField] private int testTimes = 1000;

        private void Start ()
        {
            for (var i = 0; i < testTimes; i++)
            {
                Debug.LogError ("tsfsdafasfsafsdaf" + "sdfsdafasdfsdafsadfsda");

                var list1 = new List<int> ();

                var list2 = new List<int> ();

                var list3 = new List<int> ();

                for (var j = 0; j < 10; j++)
                {
                    list1.Add (0);
                    list2.Add (0);
                    list3.Add (0);
                }

                //测试结果 
                // Remove First  0.8ms
                // Remove Middle 0.44ms
                // Remove Last   0.25ms

                _profilerMarkerRemoveFirst.Begin ();

                list1.RemoveAt (0);

                _profilerMarkerRemoveFirst.End ();


                _profilerMarkerRemoveLast.Begin ();

                list2.RemoveAt (9);

                _profilerMarkerRemoveLast.End ();


                _profilerMarkerRemoveMiddle.Begin ();

                list3.RemoveAt (4);

                _profilerMarkerRemoveMiddle.End ();
            }
        }

    }
}