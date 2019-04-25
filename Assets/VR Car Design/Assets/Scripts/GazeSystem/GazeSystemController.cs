using InputSystem;
using System.Collections;
using UISystem;
using UnityEngine;
using Zenject;

namespace GazeSystem
{
    public class GazeSystemController : IInitializable, IGazeSystem
    {
        private ReticleView reticle;
        RaycastHit hitInfo;
        private Camera cam;
        Ray ray;
        private float counter;

        public void Initialize()
        {
            cam = Camera.main;
            ray.origin = cam.transform.position;
            ray.direction = cam.transform.forward;
            reticle = cam.GetComponentInChildren<ReticleView>();
           // SpawnInputSystem();
        }

        public void OnTick()
        {
            ray.origin = cam.transform.position;
            ray.direction = cam.transform.forward;
            PeformRaycast();
        }
        private void PeformRaycast()
        {
            if (Physics.Raycast(ray, out hitInfo, 500f))
            {
                if (hitInfo.collider == null)
                {
                    reticle.ResetReticle();
                    return;
                }                
                if (hitInfo.collider.GetComponent<IUIView>() == null)
                {
                    return;
                }
                IUIView uIView = hitInfo.collider.GetComponent<IUIView>();
                //if (!reticle.isAnimating)
                //{
                    reticle.isAnimating = true;
                    counter += Time.deltaTime;
                    reticle.fillImage.fillAmount = counter / reticle.duration;
                    if (counter > reticle.duration)
                    {                        
                        uIView.PerformAction();
                        counter = 0;
                    }
                //}
            }
            reticle.ResetReticle();
        }

        private void SpawnInputSystem()
        {
            GameObject inputService = new GameObject("Input Service");
            inputService.AddComponent<InputService>();
            inputService.GetComponent<InputService>().SetGazeSystem(this);
            GameObject.DontDestroyOnLoad(inputService);
            Debug.Log("spawn input system");
        }
    }
}