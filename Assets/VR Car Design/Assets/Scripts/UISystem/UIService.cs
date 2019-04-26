using Commons;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

        public UIService(CarScriptableObject carScriptableObject, MaterialScriptableObject materialScriptableObject, SignalBus signalBus)
        {
            this.carScriptableObject = carScriptableObject;
            this.materialScriptableObject = materialScriptableObject;
            this.materialList = materialScriptableObject.materials;
            this.signalBus = signalBus;
            signalBus.Subscribe<PerformButtonFunctionSignal>(PerformButtonFunction);

            SpawnCar(carScriptableObject.car);
        }

        private void PerformButtonFunction(PerformButtonFunctionSignal performButtonFunctionSignal)
        {
            switch (performButtonFunctionSignal.buttonFunction)
            {
                case ButtonFunctionEnum.CAPTURE:
                    break;
                case ButtonFunctionEnum.HOME:
                    break;
                case ButtonFunctionEnum.SHOW_MENU:
                    ShowMenu();
                    break;
                case ButtonFunctionEnum.SCREENSHOT:
                    ScreenCapture.CaptureScreenshot("Capture1.png");
                    break;
                case ButtonFunctionEnum.EXIT_GAME:
                    break;
                case ButtonFunctionEnum.CHANGE_SCENE:
                    break;
                case ButtonFunctionEnum.CLOSE:
                    break;
            }

        }

        private void SpawnCar(GameObject car)
        {
            carHolder = new GameObject("Car Holder");
            carHolder.transform.position = new Vector3(0f, 1f, 20f);

            this.car = GameObject.Instantiate(car, Vector3.zero, Quaternion.identity);
            this.car.transform.SetParent(carHolder.transform);
            this.car.transform.localPosition = Vector3.zero;
            defaultCarMaterial = this.car.GetComponent<Renderer>().material;
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
            this.car.GetComponent<Renderer>().material = defaultCarMaterial;
        }

        public void ShowMenu()
        {
            Debug.Log("Show Menu Called");
        }
    }
}