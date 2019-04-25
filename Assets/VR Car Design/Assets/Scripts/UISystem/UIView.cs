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

        private Button thisButton;
        public MaterialTypeEnum materialType;
        public ColorOptionsEnum colorType;
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
                else if (colorType != ColorOptionsEnum.NONE)
                {
                    uIService.SetColor(colorType);
                }
            }
            else
            {
                switch (buttonFunctionEnum) {
                    case ButtonFunctionEnum.CAPTURE:
                        break;
                    case ButtonFunctionEnum.HOME:
                        break;
                    case ButtonFunctionEnum.SHOW_MENU:
                        uIService.ShowMenu();
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

                uIService.ShowMenu();
            }            
        }      
    }
}