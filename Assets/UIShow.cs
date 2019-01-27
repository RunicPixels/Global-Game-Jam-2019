using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShow : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public Image[] m_Image;
    //Use this to tell if the toggle returns true or false
    private bool m_Fading = false;
    public float duration = 2f;
    public float alpha = 0;

    private void Start() {
        foreach(Image image in m_Image) {
            image.canvasRenderer.SetAlpha(alpha);
        }
    }

    void Update() {
        //If the toggle returns true, fade in the Image
        if (m_Fading == true) {
            //Fully fade in Image (1) with the duration of 2
            foreach(Image image in m_Image) {
                image.canvasRenderer.SetAlpha(alpha);
            }
            alpha += Time.deltaTime * duration;
            
        }
    }
    public void Begin() {
        m_Fading = true;
    }
}
