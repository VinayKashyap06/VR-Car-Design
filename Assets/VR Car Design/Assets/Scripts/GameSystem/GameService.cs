using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using ScriptableObjects;
using GazeSystem;
using InputSystem;
using UISystem;
using Zenject;
using System;

namespace GameSystem
{
    public class GameService : IGameService
    {
        private IGazeSystem gazeSystem;
        private PlayerScriptableObject playerScriptableObject;
        private SignalBus signalBus;
        private GameObject playerHolder;
        private  Canvas menuButtonCanvas;
        private IUIService uIService;
        public GameService(IGazeSystem gazeSystem, IUIService uIService,UIScriptableObject uIScriptableObject, PlayerScriptableObject playerScriptableObject, SignalBus signalBus)
        {
            this.uIService = uIService;
            this.gazeSystem = gazeSystem;
            this.signalBus = signalBus;
            this.playerScriptableObject = playerScriptableObject;
            SpawnPlayer();
            SpawnInputSystem();
        }

        private void SpawnPlayer()
        {

            if (GameObject.FindObjectOfType<PlayerController>() != null)
            {
                return;
            }
            else
            {
                playerHolder = GameObject.Instantiate(playerScriptableObject.player);
                GameObject.DontDestroyOnLoad(playerHolder);
                gazeSystem.SetPlayerReference(playerHolder);
                playerHolder.GetComponent<PlayerController>().SetSignalBusRef(signalBus);                
                playerHolder.GetComponentInChildren<UIView>().SetSignalBusRef(signalBus);                
                playerHolder.GetComponentInChildren<UIView>().SetUIServiceRef(uIService);                
            }
            Debug.Log("Spawn Player called");
            
        }

        private void SpawnInputSystem()
        {
            if (GameObject.FindObjectOfType<InputService>() != null)
            {
                return;
            }
            Debug.Log("Spawn Input called");
            GameObject inputService = new GameObject("Input Service");
            inputService.AddComponent<InputService>();
            inputService.GetComponent<InputService>().SetGazeSystem(gazeSystem);
            GameObject.DontDestroyOnLoad(inputService);
        }
    }
}