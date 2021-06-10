/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 15:43:49
 * Description: UnityExtension
 ***/

using UnityEngine;

namespace DarcyStudio.Extension
{
    public static class UnityExtension
    {

        /// <summary>
        /// 分解 Vector2
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Deconstruct (this Vector2 vector2, out float x, out float y)
        {
            (x, y) = (vector2.x, vector2.y);
        }

        /// <summary>
        /// 分解 Vector3
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void Deconstruct (this Vector3 vector3, out float x, out float y, out float z)
        {
            (x, y, z) = (vector3.x, vector3.y, vector3.z);
        }
    }
}