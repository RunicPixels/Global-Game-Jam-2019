using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AdventureMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance dragonGlide;
    FMOD.Studio.ParameterInstance level2;
    FMOD.Studio.ParameterInstance level3;
    FMOD.Studio.ParameterInstance end;
    FMOD.Studio.EventDescription description;



    // Use this for initialization
    void Awake() {
        dragonGlide = RuntimeManager.CreateInstance("event:/Adventure_Music");
        dragonGlide.getParameter("LEVEL 2", out level2);
        dragonGlide.getParameter("LEVEL 3", out level3);
        dragonGlide.getParameter("END", out end);
    }

    private void Start() {
        dragonGlide.start();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Adventure2") {
            Debug.Log("Enabling Adventure 2");
            level2.setValue(1);
        }
        if(collision.tag == "Adventure3") {
            Debug.Log("Enabling Adventure 3");
            level3.setValue(1);
        }
        if(collision.tag == "End") {
            KeepInactive.Instance.FirstMenuMusic = false;
            KeepInactive.Instance.firstScene = false;
            Debug.Log("Enabling End");
            end.setValue(1);
        }
    }

}
