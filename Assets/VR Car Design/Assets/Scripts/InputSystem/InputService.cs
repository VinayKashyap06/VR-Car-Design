using UnityEngine;
using System.Collections;
using GazeSystem;
using Zenject;

namespace InputSystem
{
    public class InputService: MonoBehaviour, IInputSystem
    {
       
        private IGazeSystem gazeSystem;
        public void SetGazeSystem(IGazeSystem gazeSystem)
        {
            this.gazeSystem = gazeSystem;
        }
        private void FixedUpdate()
        {            
            gazeSystem.OnTick();
        }
    }
}