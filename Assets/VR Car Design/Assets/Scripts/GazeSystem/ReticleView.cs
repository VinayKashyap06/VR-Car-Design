using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ReticleView : MonoBehaviour
{
    //[SerializeField]
    public Image fillImage;
   // [SerializeField]
    public Image defaultImage;

   // [SerializeField]
    public float duration;

   
    public bool isAnimating=false;
    private void Start()
    {
        defaultImage.color = Color.white;
        fillImage.color = Color.green;
        defaultImage.fillAmount = 1f;
        fillImage.fillAmount = 0;
        
    }
    public void PerformAnimation(IUIView uIView)
    {

                
        isAnimating = false;
    }
    public void ResetReticle()
    {
        fillImage.fillAmount = 0;
    }
    public bool IsAnimating()
    {
        return isAnimating;
    }
}
