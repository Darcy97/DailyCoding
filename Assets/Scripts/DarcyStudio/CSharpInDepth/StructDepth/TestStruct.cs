/***
 * Created by Darcy
 * Date: Friday, 11 June 2021
 * Time: 15:58:19
 * Description: Description
 ***/

using System;
using System.Runtime.InteropServices;
using DarcyStudio.CSharpInDepth.PatternMatching;
using UnityEngine;

namespace DarcyStudio.CSharpInDepth.StructDepth
{
    public class TestStruct : MonoBehaviour
    {

        private void Start ()
        {
            Test ();
        }

        private void Test ()
        {
            var a = new MyStruct {Str = "111"};
            var b = a;
            a.Str = "222";
            Debug.Log (b.Str);

            var shape = new Circle {Radius = 2};

            a.Obj        = shape;
            shape.Radius = 4;
            Debug.Log ($"A : {((Circle) a.Obj).Radius}");

            var c = a;

            var shapeCopy = c.Obj as Circle;

            shape.Radius = 6;


            Debug.Log ($"C : {shapeCopy.Radius}");
            Debug.Log ($"A : {((Circle) a.Obj).Radius}");

            // 打印 内存地址
            var shapeHandle  = GCHandle.Alloc (shape);
            var shapeAddress = GCHandle.ToIntPtr (shapeHandle);

            var aHandle  = GCHandle.Alloc (a.Obj);
            var aAddress = GCHandle.ToIntPtr (aHandle);

            var cHandle  = GCHandle.Alloc (c.Obj);
            var cAddress = GCHandle.ToIntPtr (cHandle);

            Debug.Log ($"Shape address: {shapeAddress}");
            Debug.Log ($"A address: {aAddress}");
            Debug.Log ($"C address: {cAddress}");

            var str1 = "222";
            var str2 = str1; // 字符串特殊，这里复习一下，竟然给忘了，字符串是引用类型，这里 str2 与 str1 指向同一个内存地址
            str1 = "111";    // 但是字符串是不可变的，所以这里修改 str1 的值时 实际上 str1 指向了新的地址，所以str2 不会变
            Debug.Log ($"Str2: {str2}");
        }
    }

    struct MyStruct
    {
        public string Str;
        public object Obj;

    }
}