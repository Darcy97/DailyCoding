/***
 * Created by Darcy
 * Date: Friday, 04 June 2021
 * Time: 18:38:13
 * Description: Description
 ***/

using System.Collections;
using DarcyStudio.Closure.ReusableAction;
using UnityEngine;

namespace DarcyStudio.Closure.Test
{
    public class TestReusableAction : MonoBehaviour
    {

        private readonly ObjectPool<SaySomethingAction> _objectPool = new ObjectPool<SaySomethingAction> ();

        private void Start ()
        {
            StartCoroutine (TestActionPool ());
        }

        private IEnumerator TestActionPool ()
        {
            TestLowGcAction ("Hello",   0.2f); //serial 2
            TestLowGcAction ("Hi",      0.1f); //serial 1
            TestLowGcAction ("Goodbye", 0.4f); // serial 3
            yield return new WaitForSeconds (0.11f);
            TestLowGcAction ("Nice to meet you", 0.5f); //serial 1
        }

        private void TestLowGcAction (string para, float waitTime)
        {
            var action = _objectPool.Pop ();
            action.Capture (para);
            Method (action, waitTime);
        }

        private void Method (SaySomethingAction action, float waitTime)
        {
            StartCoroutine (DelayDo (action, waitTime));
        }

        private IEnumerator DelayDo (SaySomethingAction action, float waitTime)
        {
            yield return new WaitForSeconds (waitTime);
            action?.Invoke ("Amy");
            _objectPool.Push (action);
        }
    }


}