/***
 * Created by Darcy
 * Date: Friday, 04 June 2021
 * Time: 16:20:02
 * Description: Tuple
 ***/

namespace DefaultNamespace
{
    public class ForTuple
    {
        private void Test ()
        {
            var a = (0, 0);
            var b = a.Item1;
            var c = a.Item2;

            var age  = 24;
            var name = "Darcy";

            var he = (name, age);
            var n  = he.name;
            var ag = he.age;

            (byte, object) t0 = (0, "Str");
            (byte, object) t1 = (255, "Str");
            // (byte, object) t2 = (256, "Str");

            var (e, f) = (2, c: 2);
            // var s = (e,f).

            var cc = (2, 2);
        }

        private (int min, int max) Re ()
        {
            const int min = 1;
            const int max = 2;
            return (min, max);
            // return (max: max, min: min);
            // return (min: min, max: max);
        }

        private void Test2 ()
        {
        }
    }
}

