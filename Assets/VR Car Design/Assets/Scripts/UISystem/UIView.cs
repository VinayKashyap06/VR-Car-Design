using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;
using System;

namespace UISystem
{
    public class UIView : MonoBehaviour,IUIView
    {


        [Inject]
        private IUIService uIService;

        [Inject]
        private SignalBus signalBus;

        private Button thisButton;
        public MaterialTypeEnum materialType;
        public Color colorType;
        public ButtonTypeEnum buttonType;
        public ButtonFunctionEnum buttonFunctionEnum;
        
        private void Start()
        {
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(() =>PerformAction());
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
                signalBus.TryFire(new PerformButtonFunctionSignal() { buttonFunction = buttonFunctionEnum });
            }            
        }      
    }
}