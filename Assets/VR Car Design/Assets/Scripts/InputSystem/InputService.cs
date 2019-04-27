using UnityEngine;
using System.Collections;
using GazeSystem;
using Zenject;

namespace InputSystem
{
    public class InputService: MonoBehaviour, IInputSystem
    {
       
        private IGazeService gazeSystem;
        public void SetGazeSystem(IGazeService gazeSystem)
        {
            this.gazeSystem = gazeSystem;
        }
        private void FixedUpdate()
        {            
            gazeSystem.OnTick();
        }
    }
}