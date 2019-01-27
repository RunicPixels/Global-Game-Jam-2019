using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioSrcSwitcher : MonoBehaviour
{
    public StudioEventEmitter emitter;
    [EventRef]
    public string newSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {

            Debug.Log("Changing sound");
            emitter.ChangeEvent(newSound);
        }
    }
}
