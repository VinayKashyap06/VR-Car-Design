using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UISystem;
using Zenject;

namespace GazeSystem
{
    public class PlayerController : MonoBehaviour
    {
        public Image image;
       
        public float duration;
        public Canvas menuButtonCanvas;

        //[Inject]
        private SignalBus signalBus;

        bool isTransition;
        bool isShowing = false;
        private Color originalColor;

        float alpha;
        private void Start()
        {
           // signalBus.Subscribe<SceneChangeSignal>(FadeIn);
            SceneManager.sceneLoaded += SceneLoaded;
            originalColor = image.color;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadMode)
        {           
            FadeOut();      
        }

        void FixedUpdate()
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
            signalBus.Subscribe<SceneChangeSignal>(FadeIn);
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
    }
}