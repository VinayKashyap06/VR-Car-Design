using GameSystem;
using InputSystem;
using System.Collections;
using UISystem;
using UnityEngine;
using Zenject;

namespace GazeSystem
{
    public class GazeService : IGazeService
    {
        private ReticleView reticle;
        RaycastHit hitInfo;
        private Camera cam;
        Ray ray;
        private float counter;
        private IGameService gameService;

        public void OnTick()
        {
           // Debug.Log("ticking");
            ray.origin = cam.transform.position;
            ray.direction = cam.transform.forward;
            PeformRaycast();
        }

        public void SetGameServiceRef(IGameService gameService)
        {
            this.gameService = gameService;
            //gameService.PerformJump();
        }

        public void SetPlayerReference(GameObject player)
        {
            cam = player.GetComponentInChildren<Camera>();
            reticle = cam.GetComponentInChildren<ReticleView>();
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
                if (hitInfo.collider.GetComponent<IUIView>() != null)
                {
                    IUIView uIView = hitInfo.collider.GetComponent<IUIView>();
                   // Debug.Log("uiView found");
                    counter += Time.deltaTime;
                    reticle.fillImage.fillAmount = counter / reticle.duration;
                    if (counter > reticle.duration)
                    {
                        uIView.PerformAction();
                        counter = 0;
                    }
                }
                else if (hitInfo.collider.GetComponent<IJumpable>() != null)
                {
                    IJumpable jumpView = hitInfo.collider.GetComponent<IJumpable>();
                    counter += Time.deltaTime;
                    reticle.fillImage.fillAmount = counter / reticle.duration;
                    if (counter > reticle.duration)
                    {
                       // gameService.CheckIfSame(gameService);
                        gameService.PerformJump(jumpView);                       
                        counter = 0;
                    }
                }
                else
                {
                    counter = 0;
                    reticle.ResetReticle();
                }
            }
            else
            {
                reticle.ResetReticle();
            }
        }
    }
}