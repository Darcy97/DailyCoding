/***
 * Created by Darcy
 * Date: Friday, 04 June 2021
 * Time: 18:38:13
 * Description: Description
 ***/

using System.Collections;
using DarcyStudio.Action.ReusableAction;
using UnityEngine;

namespace DarcyStudio.Action.Test
{
    public class TestReusableAction : MonoBehaviour
    {

        private readonly ActionPool<SaySomethingAction> _actionPool = new ActionPool<SaySomethingAction> ();

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

        private void Method (SaySomethingAction action, float waitTime)
        {
            StartCoroutine (DelayDo (action, waitTime));
        }

        private IEnumerator DelayDo (SaySomethingAction action, float waitTime)
        {
            yield return new WaitForSeconds (waitTime);
            action?.Invoke ();
            _actionPool.Push (action);
        }
    }


}