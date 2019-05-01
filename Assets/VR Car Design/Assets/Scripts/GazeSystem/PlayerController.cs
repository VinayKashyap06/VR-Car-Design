using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UISystem;
using Zenject;
using System;

namespace GazeSystem
{
    public class PlayerController : MonoBehaviour
    {
        public Image image;
       
        public float duration;
       // public Canvas menuButtonCanvas;
        public MenuCanvasView menuCanvas;
        public UIView menuButton;

        //[Inject]
        private SignalBus signalBus;

        bool isTransition;
        bool isShowing = false;
        private Color originalColor;

        float alpha;
        private void Start()
        {           
            SceneManager.sceneLoaded += SceneLoaded;
            originalColor = image.color;
            menuCanvas.gameObject.SetActive(false);            

        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadMode)
        {           
            FadeOut();      
        }

        void Update()
        {
            if (!isTransition)
                return;
            alpha += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
            image.color = Color.Lerp(originalColor, Color.black, alpha);
            if (alpha > 1 || alpha < 0)
            {
                isTransition = false;
            }
        }

        public void SetSignalBusRef(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            UIView[] components=menuCanvas.GetComponentsInChildren<UIView>();
            foreach (var item in components)
            {
                item.SetSignalBusRef(signalBus);
            }          
            this.signalBus.Subscribe<SceneChangeSignal>(FadeIn);
            menuButton.SetSignalBusRef(signalBus);
        }

        public void FadeIn()
        {
            isShowing = true;
            alpha = 0;
            isTransition = true;            
        }
        public void FadeOut()
        {
            Debug.Log("fading out");
            isShowing = false;
            alpha = 1;
            isTransition = true;
        }      
        public void SetUIRef(IUIService uIService)
        {           
            UIView[] components = menuCanvas.GetComponentsInChildren<UIView>();
            foreach (var item in components)
            {
                item.SetUIServiceRef(uIService);
            }
            uIService.SetCurrentPlayerControllerRef(this);
            menuButton.SetUIServiceRef(uIService);

        }
        public void ShowMenu()
        {
            menuCanvas.gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            menuCanvas.gameObject.SetActive(false);
        }
    }
}