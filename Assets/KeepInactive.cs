using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class KeepInactive : MonoBehaviour {
    public static KeepInactive Instance;

    public StudioEventEmitter emitter;

    public bool FirstMenuMusic;

    public bool firstScene;
    // Start is called before the first frame update

    void Awake() {

        if (Instance == null){
            Instance = this;
            FirstMenuMusic = true;
        }

        else if (Instance != this) {
            Destroy(gameObject);
        }

        if (FirstMenuMusic && firstScene) {
            emitter.Play();
        }
        DontDestroyOnLoad(this);
    }

    void Update() {
        if (firstScene) {
            if (SceneManager.GetActiveScene().buildIndex != 0) {
                DisableMusic();
            }
        }
    }
    
    public void DisableMusic() {
        firstScene = false;
        StartCoroutine(TurnOffYield());
    }

    public void TurnOff() {
        emitter.AllowFadeout = true;
        
    }

    public IEnumerator TurnOffYield() {
        while (SceneManager.GetActiveScene().buildIndex == 0) {
            StudioListener listener = GetComponent<StudioListener>();
            yield return new WaitForSeconds(0.1f);
        }

        emitter.Stop();
    }
}