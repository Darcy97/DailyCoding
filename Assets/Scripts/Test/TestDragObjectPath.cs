/***
 * Created by Darcy
 * Date: Saturday, 26 June 2021
 * Time: 16:30:29
 * Description: Description
 ***/

using DarcyStudio.GameComponent.Attribute.SelectObjectAttribute;
using UnityEngine;

namespace Test
{
    public class TestDragObjectPath:MonoBehaviour
    {
        [DragObjectPath (SearchType.AudioClip)] public string Prefab;

    }
}