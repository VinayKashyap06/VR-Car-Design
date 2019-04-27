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
        private  Canvas menuButtonCanvas;
        private IUIService uIService;
        private PlayerController currentPlayerController;
        private IJumpable currentJumpPoint;
        private IJumpable previousJumpPoint;
        public GameService(IGazeService gazeSystem, IUIService uIService,UIScriptableObject uIScriptableObject, PlayerScriptableObject playerScriptableObject, SignalBus signalBus)
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

            if (GameObject.FindObjectOfType<PlayerController>() != null)
            {
                playerHolder = GameObject.FindObjectOfType<PlayerController>().gameObject;
                currentPlayerController=playerHolder.GetComponentInChildren<PlayerController>();
                SpawnPlayerJumpPoint(playerHolder.transform.position);
                
                return;
            }
            else
            {
                playerHolder = GameObject.Instantiate(playerScriptableObject.player);
                GameObject.DontDestroyOnLoad(playerHolder);
                gazeSystem.SetPlayerReference(playerHolder);
                currentPlayerController = playerHolder.GetComponentInChildren<PlayerController>();
                SpawnPlayerJumpPoint(playerHolder.transform.position);
                playerHolder.GetComponent<PlayerController>().SetSignalBusRef(signalBus);                
                playerHolder.GetComponentInChildren<UIView>().SetSignalBusRef(signalBus);                
                playerHolder.GetComponentInChildren<UIView>().SetUIServiceRef(uIService);                
            }
            Debug.Log("Spawn Player called");
            
        }

        private void SpawnPlayerJumpPoint(Vector3 position)
        {
            GameObject playerSpawnPoint = GameObject.Instantiate(playerScriptableObject.jumpPointPrefab);            
            playerSpawnPoint.transform.position = new Vector3(position.x, position.y - 2f, position.z);
            currentJumpPoint=playerSpawnPoint.GetComponent<IJumpable>();
            currentJumpPoint.DisableJumpPoint();
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

        public async void PerformJump(IJumpable jumpView)
        {
            previousJumpPoint = currentJumpPoint;
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