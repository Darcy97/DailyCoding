/***
 * Created by Darcy
 * Date: Thursday, 10 June 2021
 * Time: 11:42:18
 * Description: Description
 ***/

using System;

namespace DarcyStudio.StudyNotes.CSharpInDepth.PatternMatching
{
    public class VarMatching
    {
        private void Test ()
        {
            if (GetStr () is var x)
            {
            }
        }

        private string GetStr ()
        {
            return "22";
        }

        private void Perimeter (Shape shape)
        {
            switch (shape ?? Shape.CreateRandomShape ())
            {
                case Rectangle rect:
                    // Do something;
                    break;
                case Circle circle:
                    // DO something;
                    break;
                case Triangle tri:
                    // Do something;
                    break;
                
                // Var matching 的用处， 通过这种方式引入一个变量 不然还需要在 switch 之前定义一个 变量 var tempShape = shape ?? CreateRandomShape ()
                // 追求极致
                case var actualShape:
                    throw new InvalidOperationException ($"Shape type {actualShape.GetType ()} perimeter unknown");
            }
        }
    }
}