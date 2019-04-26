using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Zenject;
using System;

namespace UISystem
{
    public class UIView : MonoBehaviour,IUIView
    {        
        private IUIService uIService;

        private SignalBus signalBus;

        private Button thisButton;
        public MaterialTypeEnum materialType;
        public Color colorType;
        public ButtonTypeEnum buttonType;
        public ButtonFunctionEnum buttonFunctionEnum;

        public bool ShouldBeDeactivated;
        
        private void Awake()
        {           
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(() =>PerformAction());          
            if (ShouldBeDeactivated)
            {
                this.gameObject.SetActive(false);
            }
        }
        public void SetSignalBusRef(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        public void PerformAction()
        {           
            if (buttonType != ButtonTypeEnum.MENU)
            {
                if (materialType != MaterialTypeEnum.NONE)
                {
                    uIService.SetMaterial(materialType);
                }
                else if (colorType.a !=0)
                {
                    uIService.SetColor(colorType);
                }
            }
            else
            {
                signalBus.TryFire(new PerformButtonFunctionSignal() { buttonFunction = buttonFunctionEnum ,uIView=this});
            }            
        }

        public void SetUIServiceRef(IUIService uIService)
        {
            this.uIService = uIService;
        }
    }
}