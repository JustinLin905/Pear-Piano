using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public AudioSource soundPlayer;
    void OnTriggerEnter(Collider other) {
        if (other.name == "Block") {
            soundPlayer.Play();
        }
       
    }

    void OnTriggerExit(Collider other) {
        if (other.name == "Block") {
            soundPlayer.Stop();
        }
    }
}
