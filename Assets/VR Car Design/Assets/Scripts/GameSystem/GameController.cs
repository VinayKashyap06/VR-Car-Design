using UnityEngine;
using System.Collections;
using GazeSystem;
using ScriptableObjects;
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