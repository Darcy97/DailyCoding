/***
 * Created by Darcy
 * Date: Tuesday, 22 June 2021
 * Time: 14:22:01
 * Description: Description
 ***/

using DarcyStudio.GameComponent.SortingLayerDrawerAttribute;
using UnityEngine;

namespace Test
{
    public class TestSortingLayerDrawer : MonoBehaviour
    {
        [SerializeField] [SortingLayer] private string sortingLayer = "Default";

    }
}