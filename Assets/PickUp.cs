using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType { Jump }
    public PickupType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Player>() == null) {
            Player player = other.GetComponent<Player>();
            if(type == PickupType.Jump) {
                player.jumps += 1;
            }
        }
    }
}
