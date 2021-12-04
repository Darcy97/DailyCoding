/***
 * Created by Darcy
 * Date: Wednesday, 09 June 2021
 * Time: 16:49:04
 * Description: ValueAssign
 ***/

namespace DarcyStudio.StudyNotes.CSharpInDepth.Tuple
{
    public class SuperValueAssign
    {

    }

    public class Point
    {
        public readonly double X;
        public readonly double Y;

        public Point (double x, double y)
        {
            (X, Y) = (x, y);
        }

        /*
        /// <summary>
        /// Spit
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void Deconstruct (out double x, out double y)
        {
            (x, y) = (X, Y);
        }
        */

    }

    public static class PointExtension
    {

        /// <summary>
        /// 若目标类型无法添加 Deconstruct 
        /// 也可以通过拓展实现
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Deconstruct (this Point point, out double x, out double y)
        {
            (x, y) = (point.X, point.Y);
        }

        //  下面这段字不用 Rider 的可以略过 
        //  这里有个有意思的事情
        //  Rider 2020.3 会推荐我写成下面这样
        //  通过隐式调用分解方法获取 x，y
        //  初一看没问题，但是仔细一看 😧
        //  该方法本身就是分解方法，所以就会自己调用自己，造成死循环 😨
        //  我还不死心的测试了一下，确实会死循环，断着点敲下了这断字，看来不能无脑信奉 Rider 😬（
        //  Rider 的推荐也不是万能的哈哈哈 🤡🤡
        //  这几个小绿点真是逼死强迫症啊🥶
        // public static void Deconstruct (this Point point, out double x, out double y)
        // {
        //     var (d, d1) = point; //The requested operation caused a stack overflow.
        //     (x, y)      = (d, d1);
        // }

    }
}