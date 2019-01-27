using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour
{
    private SpriteRenderer image;
    public Sprite active, inactive;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            image.sprite = active;
            light.SetActive(true);

        }
    }
}
