using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FaderController : MonoBehaviour
{
    public Image image;
    [SerializeField]
    private float duration;

    bool isTransition;
    bool isShowing;

    float alpha;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransition)
            return;
        alpha += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1/duration);

        image.color = Color.Lerp(new Color(1,1,1,0),Color.black,alpha);
        if (alpha > 1||alpha<0)
        {
            isTransition = false;
        }
        
    }
    void Fade(bool show)
    {
        isShowing = show;
        alpha = show ? 0 : 1;
    }
}
