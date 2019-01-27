using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public Image fadeToBlack;

    private bool finish;
    private float opacity;
    private float duration = 5;
    private float realCount;
    private bool truefinish = false;
    private float fadeDuration = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fadeToBlack.color = new Color(0, 0, 0, opacity);
        if(finish == true) {
            realCount += Time.deltaTime;
            if(realCount > duration) {
                truefinish = true;
            }
            
        }

        if(truefinish == true) {
            opacity += Time.deltaTime / fadeDuration;
        }

        if(opacity > 1) {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            finish = true;
        }
    }
}
