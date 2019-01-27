using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public SpriteRenderer light;
    bool doLight = false;
    float opacity = 0;
    float duration = 1;
    bool fadingOut = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doLight == true) {
            if (fadingOut) {
                opacity -= duration * Time.deltaTime;
                if (opacity < 0) {
                    doLight = false;
                }
            }
            else {
                opacity += duration * Time.deltaTime;
                if (opacity > 1) {
                    fadingOut = true;
                }
            }
            light.color = new Color(1, 1, 1, opacity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {

            doLight = true;
            fadingOut = false;
        }
    }


}
