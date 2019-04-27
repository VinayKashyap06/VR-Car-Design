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
            uIViews=GameObject.FindObjectsOfType<UIView>();
            for (int i = 0; i < uIViews.Length; i++)
            {               
                uIViews[i].SetUIServiceRef(this);
                uIViews[i].SetSignalBusRef(signalBus);
            }
            this.carScriptableObject = carScriptableObject;
            this.materialScriptableObject = materialScriptableObject;
            this.materialList = materialScriptableObject.materials;
            this.signalBus = signalBus;
            signalBus.Subscribe<PerformButtonFunctionSignal>(PerformButtonFunction);

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SpawnCar(carScriptableObject.car);
            }
        }


        private void SpawnCar(GameObject car)
        {
            carHolder = new GameObject("Car Holder");
            carHolder.transform.position = new Vector3(0f, 0f, 20f);
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
            Material[] mats = car.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = defaultCarMaterial;
            }
            car.transform.GetChild(0).GetComponent<MeshRenderer>().materials = mats;
            //this.car.GetComponentInChildren<Renderer>().material = defaultCarMaterial;
        }

        private void ShowMenu()
        {
            playerController.ShowMenu();
            Debug.Log("Show Menu Called");
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
                    ShowMenu();
                    break;
                case ButtonFunctionEnum.SCREENSHOT:
                    ScreenCapture.CaptureScreenshot("Capture1.png");
                    break;
                case ButtonFunctionEnum.EXIT_GAME:
                    Application.Quit();
                    break;
                case ButtonFunctionEnum.CHANGE_SCENE:
                    signalBus.TryFire(new SceneChangeSignal());                   
                    SceneManager.LoadSceneAsync(1);
                    break;
                case ButtonFunctionEnum.CLOSE:
                    //performButtonFunctionSignal.uIView.transform.parent.gameObject.SetActive(false);
                    break;
            }

        }

        public void SetCurrentPlayerControllerRef(PlayerController playerController)
        {
           this.playerController = playerController;
        }
    }
}