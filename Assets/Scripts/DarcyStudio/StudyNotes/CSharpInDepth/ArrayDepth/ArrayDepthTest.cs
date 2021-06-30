/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Wednesday, 30 June 2021
 * Time: 15:40:05
 * Description: 
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