using UnityEngine;
using System.Collections;
using UISystem;
using GazeSystem;
using InputSystem;
using Zenject;

namespace GameSystem
{
    public class GameController : IGameController
    {
        private IGazeSystem gazeSystem;
        public GameController(IGazeSystem gazeSystem,UIScriptableObject uIScriptableObject)
        {
            this.gazeSystem = gazeSystem;
        }

        public void Initialize()
        {
                        
        }       
    }
}