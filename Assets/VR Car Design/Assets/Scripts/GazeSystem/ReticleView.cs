using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ReticleView : MonoBehaviour
{
    public Image fillImage;    
    public float duration;      
    private void Start()
    {        
        fillImage.color = Color.green;        
        fillImage.fillAmount = 0;        
    }   
    public void ResetReticle()
    {
        fillImage.fillAmount = 0;
    } 
}
