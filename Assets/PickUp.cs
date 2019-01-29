using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType { Jump }
    public PickupType type;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Heeeeey");
        if(other.GetComponent<Player>() != null && done == false) {
            Player player = other.GetComponent<Player>();
            if(type == PickupType.Jump) {
                player.jumps += 1;
                player.ResetJumpCounter();
                done = true;
            }
            gameObject.SetActive(false);
        }
    }
}
