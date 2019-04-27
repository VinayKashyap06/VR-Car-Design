using UnityEngine;
using System.Collections;
using GazeSystem;
using ScriptableObjects;
using Zenject;

namespace GameSystem
{
    public class GameController : IGameController
    {
        private IGazeService gazeSystem;
        public GameController(IGazeService gazeSystem,UIScriptableObject uIScriptableObject)
        {
            this.gazeSystem = gazeSystem;
        }

        public void Initialize()
        {
                        
        }       
    }
}