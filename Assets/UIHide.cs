using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHide : MonoBehaviour
{
    public Image[] m_Image;
    public float duration = 1f;
    private bool hasTriggered = false;
    public UIShow show;
    private bool m_Fading = false;
    public float alpha = 0;

    private void Start() {
        foreach (Image image in m_Image) {
            image.canvasRenderer.SetAlpha(0f);
        }
    }

    void Update() {
        //If the toggle is false, fade out to nothing (0) the Image with a duration of 2

        if (m_Fading == false) {
            //Fully fade in Image (1) with the duration of 2
            foreach (Image image in m_Image) {
                image.canvasRenderer.SetAlpha(alpha);

            }
            if(alpha > 2.05f) {
                m_Fading = true;
            }
            alpha += Time.deltaTime / duration;

        }
        if (m_Fading == true) {
            foreach (Image image in m_Image) {
                image.canvasRenderer.SetAlpha(alpha);
                if (image.canvasRenderer.GetAlpha() < 0.15f && !hasTriggered) {
;
                }
            }
            if(alpha < 0) {
                hasTriggered = true;
                show.gameObject.SetActive(true);
                show.Begin();
            }
            alpha -= Time.deltaTime / duration;
        }
    }
}
