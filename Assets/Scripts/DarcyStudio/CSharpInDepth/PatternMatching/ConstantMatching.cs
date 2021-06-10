/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 11:11:09
 * Description: Constant matching
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.CSharpInDepth.PatternMatching
{
    public class ConstantMatching : MonoBehaviour
    {
        // 常量匹配 若 is 左右都是整型表达式 则调用 == 进行比较，否则会调用 静态方法 object.equals 进行比较


        private void Start ()
        {
            Test ();
        }

        private void Match (object input)
        {
            switch (input)
            {
                case "hello":
                    Debug.Log ($"Input: {input} is string hello");
                    break;
                case 5L:
                    Debug.Log ($"Input: {input} is long 5");
                    break;
                case 10:
                    Debug.Log ($"Input: {input} is int 10");
                    break;
                default:
                    Debug.Log ($"Input: {input} didn't match hello, long 5 or int 10");
                    break;
            }
        }

        private void Test ()
        {
            Match ("hello");
            Match (5L);
            Match (9);
            Match (10);

            // 下面这个调用 OutPut: Input: 10 didn't match hello, long 5 or int 10
            // 因为 Match 的参数是 object, 传参时会装箱，所以编译时 input 时 object 类型 所以会调用 Object.Equals 来进行比较
            Match (10L);


            long x = 10L;
            if (x is 10) // 该表达式返回 true 因为会调用 == 进行比较
            {
                Debug.Log ("10L is 10");
            }
        }
    }
}