using Commons;
using ScriptableObjects;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GazeSystem;

namespace UISystem
{
    public class UIService : IUIService
    {
        private CarScriptableObject carScriptableObject;
        private MaterialScriptableObject materialScriptableObject;
        private GameObject car;
        private Material defaultCarMaterial;
        private GameObject carHolder;
        private List<MaterialListStructure> materialList = new List<MaterialListStructure>();
        private SignalBus signalBus;
        private UIView[] uIViews;
        private PlayerController playerController;

        public UIService(CarScriptableObject carScriptableObject, MaterialScriptableObject materialScriptableObject, SignalBus signalBus)
        {
          
            this.carScriptableObject = carScriptableObject;
            this.materialScriptableObject = materialScriptableObject;
            this.materialList = materialScriptableObject.materials;
            this.signalBus = signalBus;          
           // signalBus.Subscribe<PerformButtonFunctionSignal>(PerformButtonFunction);
            SceneManager.sceneLoaded += OnNewSceneLoaded;
        }

        private void OnNewSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            signalBus.TryUnsubscribe<PerformButtonFunctionSignal>(PerformButtonFunction);
            uIViews = GameObject.FindObjectsOfType<UIView>();
            for (int i = 0; i < uIViews.Length; i++)
            {
                uIViews[i].SetUIServiceRef(this);
                uIViews[i].SetSignalBusRef(signalBus);
            }
            if (scene.buildIndex != 0)
            {
                SpawnCar(carScriptableObject.car);
            }
            signalBus.Subscribe<PerformButtonFunctionSignal>(PerformButtonFunction);
        }

        private void SpawnCar(GameObject car)
        {
            carHolder = new GameObject("Car Holder");
            carHolder.transform.position = new Vector3(0f, 1f, 15f);
            this.car = GameObject.Instantiate(car, Vector3.zero, car.transform.rotation);
            this.car.transform.SetParent(carHolder.transform);
            this.car.transform.localPosition = Vector3.zero;
            defaultCarMaterial = this.car.GetComponentInChildren<Renderer>().material;
            
        }
        public void SetMaterial(MaterialTypeEnum materialTypeEnum)
        {
            for (int i = 0; i < materialList.Count; i++)
            {
                if (materialList[i].materialType == materialTypeEnum)
                {
                    defaultCarMaterial = materialList[i].material;
                    break;
                }
            }
            SetCarMaterial();
        }

        public void SetColor(Color color)
        {
            defaultCarMaterial.color = color;
            SetCarMaterial();
        }

        private void SetCarMaterial()
        {
            Debug.Log("SettingMaterial");
            this.car.GetComponentInChildren<Renderer>().material = defaultCarMaterial;
        }
      

        public SignalBus GetSignalBus()
        {
            return signalBus;
        }
        private  void PerformButtonFunction(PerformButtonFunctionSignal performButtonFunctionSignal)
        {
            switch (performButtonFunctionSignal.buttonFunction)
            {              
                case ButtonFunctionEnum.HOME:
                    signalBus.TryFire(new SceneChangeSignal());                   
                    SceneManager.LoadSceneAsync(0);
                    break;
                case ButtonFunctionEnum.SHOW_MENU:
                    Debug.Log("Show menu called");
                    playerController.ShowMenu();
                    break;
                case ButtonFunctionEnum.SCREENSHOT:
                    ScreenCapture.CaptureScreenshot("Capture1.png");
                    break;
                case ButtonFunctionEnum.EXIT_GAME:
                    Application.Quit();
                    break;
                case ButtonFunctionEnum.GARAGE:
                    signalBus.TryFire(new SceneChangeSignal());                   
                    SceneManager.LoadSceneAsync(1);
                    break;
                case ButtonFunctionEnum.CLOSE:
                    playerController.HideMenu();
                    break;
            }

        }

        public void SetCurrentPlayerControllerRef(PlayerController playerController)
        {
           this.playerController = playerController;
            
        }
    }
}