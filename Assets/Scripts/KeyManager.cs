using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public AudioClip sound;

    private Note n;
    public string pitch;
    void OnTriggerEnter(Collider other) {
        n.startTime = Time.time;
        n.pitch = pitch;
    }

    void OnTriggerExit(Collider other) {
        n.endTime = Time.time;
        NoteTracker.instance.notes.Add(n);
    }
}
