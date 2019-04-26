using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GazeSystem
{
    public class FaderController : MonoBehaviour
    {
        public Image image;
        [SerializeField]
        private float duration;

        bool isTransition;
        bool isShowing = false;

        float alpha;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fade(!isShowing);
            }
            if (!isTransition)
                return;
            alpha += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);

            image.color = Color.Lerp(new Color(1, 1, 1, 0), Color.black, alpha);
            if (alpha > 1 || alpha < 0)
            {
                isTransition = false;
            }

        }
        public void Fade(bool show)
        {
            isShowing = show;
            alpha = show ? 0 : 1;
            isTransition = true;
        }
    }
}