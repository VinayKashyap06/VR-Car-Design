using UnityEngine;
using System.Collections.Generic;
using Commons;
using System;

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

        
        public UIService(CarScriptableObject carScriptableObject, MaterialScriptableObject materialScriptableObject)
        {
            this.carScriptableObject = carScriptableObject;
            this.materialScriptableObject = materialScriptableObject;
            this.materialList = materialScriptableObject.materials;
            SpawnCar(carScriptableObject.car);
        }

        private void SpawnCar(GameObject car)
        {
            carHolder = new GameObject("Car Holder");
            carHolder.transform.position = new Vector3(50f,1f,15f);

            this.car = GameObject.Instantiate(car, Vector3.zero,Quaternion.identity);
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

        public void SetColor(ColorOptionsEnum colorOptionsEnum)
        {
            switch (colorOptionsEnum)
            {
                case ColorOptionsEnum.BLACK:
                    defaultCarMaterial.color = Color.black;
                    break;
                case ColorOptionsEnum.RED:
                    defaultCarMaterial.color = Color.red;
                    break;
                case ColorOptionsEnum.BLUE:
                    defaultCarMaterial.color = Color.blue;
                    break;
            }
            SetCarMaterial();
        }

        private void SetCarMaterial()
        {
            Debug.Log("SettingMaterial");
            this.car.GetComponent<Renderer>().material = defaultCarMaterial;
        }

        public void ShowMenu()
        {
            
        }
    }
}