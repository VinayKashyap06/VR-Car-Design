using UnityEngine;
using System.Collections;
using ScriptableObjects;
using GazeSystem;
using InputSystem;
using Zenject;
using System;

namespace GameSystem
{
    public class GameService : IInitializable, IGameService
    {
        private IGazeSystem gazeSystem;
        public GameService(IGazeSystem gazeSystem,UIScriptableObject uIScriptableObject,PlayerScriptableObject playerScriptableObject)
        {
            this.gazeSystem = gazeSystem;
        }

        public void Initialize()
        {
            //spawn UI
            //spawn Player
            SpawnPlayer();
            SpawnInputSystem();
        }

        private void SpawnPlayer()
        {
            
        }

        private void SpawnInputSystem()
        {
            GameObject inputService = new GameObject("Input Service");
            inputService.AddComponent<InputService>();
            inputService.GetComponent<InputService>().SetGazeSystem(gazeSystem);
            GameObject.DontDestroyOnLoad(inputService);            
        }
    }
}