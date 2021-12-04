/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 18:03:08
 * Description: Description
 ***/

using System.Collections.Generic;
using UnityEngine;

namespace DarcyStudio.StudyNotes.CSharpInDepth.RefKeyWord
{
    public class RefLocalVariable : MonoBehaviour
    {

        private void Test ()
        {
            var     x = 2;
            ref var y = ref x;
            x++;
            y++; // 此时 y = 4 x = 4

            // ref int z; //非法 必须在声明时初始化
            // z = 2;

            var     x1 = 10;
            ref var y1 = ref RefReturn (ref x1);
            y1++;
            Debug.Log (x1); //  结果是 11 
            // 如上调用 等价于 ref int y = ref x;
        }

        /// <summary>
        /// RefReturn
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static ref int RefReturn (ref int p)
        {
            return ref p;
        }

        private void TestArrayHolder ()
        {
            var     holder = new ArrayHolder ();
            ref var x      = ref holder[0];
            ref var y      = ref holder[0];

            x = 20;
            Debug.Log (y); //  y = 20
        }

        private void Start ()
        {
            // TestArrayHolder ();

            var (even, odd) = CountEvenAndOdd (new[] {1, 2, 3, 10, 5});
            Debug.Log ($"Even num {even}, Odd num {odd}");
        }

        /// <summary>
        /// 这个用法很有趣
        /// 虽然实际情况不会这么做
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private (int even, int odd) CountEvenAndOdd (IEnumerable<int> values)
        {
            var result = (even: 0, odd: 0);
            foreach (var value in values)
            {
                ref var counter = ref (value & 1) == 0 ? ref result.even : ref result.odd;
                counter++;
            }

            return result;
        }

    }

    internal class ArrayHolder
    {
        private readonly int[] array = new int[10];
        public ref int this [int index] => ref array[index];
    }
}