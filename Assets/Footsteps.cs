using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Footsteps : MonoBehaviour
{
    [EventRef]
    public string Event = "";
    // Start is called before the first frame update

    public void PlayOnce() {
        RuntimeManager.PlayOneShot(Event);
    }
}
