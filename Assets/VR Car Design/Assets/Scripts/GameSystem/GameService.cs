using UnityEngine;
using System.Collections;
using UISystem;
using GazeSystem;
using InputSystem;
using Zenject;

namespace GameSystem
{
    public class GameService : IInitializable, IGameService
    {
        private IGazeSystem gazeSystem;
        public GameService(IGazeSystem gazeSystem,UIScriptableObject uIScriptableObject)
        {
            this.gazeSystem = gazeSystem;
        }

        public void Initialize()
        {
            //spawn UI
            //spawn Player
            SpawnInputSystem();
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