using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public GameObject player;
    private Vector3 velocity = Vector3.zero;
    public float speedDelay = 0.4f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, speedDelay) - (Vector3.forward);
	}
}
