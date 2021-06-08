/***
 * Created by Darcy
 * Date: Tuesday, 08 June 2021
 * Time: 14:46:13
 * Description: Description
 ***/

using System;
using UnityEngine;

namespace DarcyStudio.GameComponent.Turntable
{
    public class TestTurntable : MonoBehaviour
    {
        [SerializeField] private Turntable _turntable;

        private void Update ()
        {
            if (Input.GetKeyDown (KeyCode.A))
            {
                _turntable.StartSpin (1);
                Debug.LogError ("KeyDown");
            }

            if (Input.GetKeyDown (KeyCode.D))
            {
                _turntable.StartSpin (4);
            }
        }

    }
}