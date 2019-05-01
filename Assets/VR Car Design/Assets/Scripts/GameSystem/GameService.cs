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
        private IGazeService gazeSystem;
        private PlayerScriptableObject playerScriptableObject;
        private SignalBus signalBus;
        private GameObject playerHolder;
        private Canvas menuButtonCanvas;
        private IUIService uIService;
        private PlayerController currentPlayerController;
        private IJumpable currentJumpPoint;
        private IJumpable previousJumpPoint;
        public GameService(IGazeService gazeSystem, IUIService uIService, UIScriptableObject uIScriptableObject, PlayerScriptableObject playerScriptableObject, SignalBus signalBus)
        {
            this.uIService = uIService;
            this.gazeSystem = gazeSystem;
            this.signalBus = signalBus;
            this.playerScriptableObject = playerScriptableObject;
            gazeSystem.SetGameServiceRef(this);
            SpawnPlayer();
            SpawnInputSystem();
        }


        private void SpawnPlayer()
        {            
            playerHolder = GameObject.Instantiate(playerScriptableObject.player);
            GameObject.DontDestroyOnLoad(playerHolder);
            gazeSystem.SetPlayerReference(playerHolder);
            currentPlayerController = playerHolder.GetComponentInChildren<PlayerController>();
            currentPlayerController.SetSignalBusRef(signalBus);
            currentPlayerController.SetUIRef(uIService);
            SpawnPlayerJumpPoint(playerHolder.transform.position);
        }

        private void SpawnPlayerJumpPoint(Vector3 position)
        {
            //  Debug.Log(" spawn jump point called ");
            GameObject playerSpawnPoint = GameObject.Instantiate(playerScriptableObject.jumpPointPrefab);
            playerSpawnPoint.transform.position = new Vector3(position.x, position.y - 2f, position.z);
            currentJumpPoint = null;
            previousJumpPoint = null;
            currentJumpPoint = playerSpawnPoint.GetComponent<IJumpable>();
            Debug.Log("currentJumpPoint" + currentJumpPoint);
            currentJumpPoint.DisableJumpPoint();
            previousJumpPoint = currentJumpPoint;
            GameObject.DontDestroyOnLoad(playerSpawnPoint);
        }

        private void SpawnInputSystem()
        {            
            Debug.Log("Spawn Input called");
            GameObject inputService = new GameObject("Input Service");
            inputService.AddComponent<InputService>();
            inputService.GetComponent<InputService>().SetGazeSystem(gazeSystem);
            GameObject.DontDestroyOnLoad(inputService);
        }

        public async void PerformJump(IJumpable jumpView)
        {
            //  Debug.Log("previous jump point" + previousJumpPoint);
            previousJumpPoint = currentJumpPoint;
            //Debug.Log("current jum[p ppoint2" + currentJumpPoint);
            //Debug.Log("previous jump point2 " + previousJumpPoint);
            previousJumpPoint.EnableJumpPoint();
            currentJumpPoint = jumpView;
            currentJumpPoint.DisableJumpPoint();
            currentPlayerController.FadeIn();
            Vector3 position = currentJumpPoint.GetPosition();
            await new WaitForSeconds(currentPlayerController.duration);
            Vector3 newPosition = new Vector3(position.x, playerHolder.transform.position.y, position.z);
            playerHolder.transform.position = newPosition;
            currentPlayerController.FadeOut();

        }       
    }
}