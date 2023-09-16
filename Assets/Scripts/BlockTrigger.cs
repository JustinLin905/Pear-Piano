using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.name == "Block") {
            // other.playNote
        }
       
    }

    void OnTriggerExit(Collider other) {
        if (other.name == "Block") {
        }
    }
}
