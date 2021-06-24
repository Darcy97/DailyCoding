/***
 * Created by Darcy
 * Date: Monday, 07 June 2021
 * Time: 18:10:26
 * Description:
 * 
 * string.Format 若参数类型为值类型，在调用时会多一次装箱操作
 * 如下为测试代码
 * 所以最佳调用方式为 传参数时手动调用一次 ToString 这样可以节省一次装箱操作 (见第三种调用方式)
 * Unity Editor 内测试结果为，遍历一千次，有装箱操作比无装箱操作的调用多 20k 左右的 GC
 * 看 IL 代码可以看到 第二种写法会多一次 Box （第一种写法其实可以第二种写法一样，测试结果也是一样的，GC 相同 Cpu 时间有一点点差异）
 * 理论上分析也是，string.format 参数类型为 Object 所以必然会有一次 int -> object 的装箱 
 ***/

using Unity.Profiling;
using UnityEngine;

namespace DarcyStudio.StudyNotes.CSharpInDepth.String
{
    public class TestStringFormat : MonoBehaviour
    {
        private ProfilerMarker _profilerMarker0 = new ProfilerMarker ("String.FormatInt.ToObj");

        private ProfilerMarker profilerMarker1 = new ProfilerMarker ("String.FormatInt.Directly");

        private ProfilerMarker profilerMarker2 = new ProfilerMarker ("String.FormatInt.ToString");

        private void Update ()
        {
            //按键盘 A 触发
            if (!Input.GetKeyDown (KeyCode.A))
                return;
            Debug.LogError ("Test");
            Test ();
        }

        /// <summary>
        /// string.Format 若参数类型为值类型，在调用时会多一次装箱操作
        /// 如下为测试代码
        /// 所以最佳调用方式为 传参数时手动调用一次 ToString 这样可以节省一次装箱操作
        /// Unity Editor 内测试结果为，遍历一千次，有装箱操作比无装箱操作的调用多 20k 左右的 GC
        /// 看 IL 代码可以看到 第二种调用会多一次 Box
        /// 理论上分析也是，string.format 参数类型为 Object 所以必然会有一次 int -> object 的装箱 
        /// </summary>
        private void Test ()
        {
            const int para = 222222;

            _profilerMarker0.Begin ();

            for (var i = 0; i < 1000; i++)
            {
                object d = para;
                var    a = $"Say hello {d}";
            }

            _profilerMarker0.End ();


            profilerMarker1.Begin ();

            for (var i = 0; i < 1000; i++)
            {
                var a = $"Say hello {para}";
            }

            profilerMarker1.End ();

            profilerMarker2.Begin ();

            for (var i = 0; i < 1000; i++)
            {
                var b = para.ToString ();
                var c = $"Say hello {b}";
            }

            profilerMarker2.End ();
        }
    }
}