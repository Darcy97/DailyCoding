/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 16:54:56
 * Description: 测试闭包捕获 ValueType Variable 的行为
 ***/

using System.Collections;
using UnityEngine;

namespace DarcyStudio.Action.Test
{
    public class TestClosure : MonoBehaviour
    {
        private void Start ()
        {
            StartCoroutine (Test ());
        }

        private IEnumerator Test ()
        {
            var a = 2;

            //这个 会延迟 1s 执行闭包
            Do (() =>
            {
                Debug.Log ($"A = {a}");
                a = 3;
            });

            //这个会在闭包执行前被执行，所以闭包输出时候 a = 4
            a = 4;

            //这里延迟 1.1s 
            yield return new WaitForSeconds (1.1f);

            //所以执行这里时闭包已经被执行，a = 3
            Debug.Log ($"A = {a}");
        }

        private void Do (System.Action action)
        {
            StartCoroutine (DelayOneSecond (action));
        }

        private IEnumerator DelayOneSecond (System.Action action)
        {
            yield return new WaitForSeconds (1);
            action?.Invoke ();
        }
    }
}