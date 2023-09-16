using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;

public class HandTrigger : MonoBehaviour
{
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;

    public AudioClip sound;
    public Material redMaterial;
    public Material greenMaterial;

    private Note n;
    public string pitch;

    void Start() {
        // GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state) {
        if (state.NewInteractableState == InteractableState.ActionState) {
            actionEvent.Invoke();
        }

        if (state.NewInteractableState == InteractableState.Default) {
            defaultEvent.Invoke();
        }
    }

    void OnTriggerEnter(Collider other) {
        n.startTime = Time.time;
        n.pitch = pitch;

        // Find child GameObject named Block Trigger
        GameObject blockTrigger = transform.Find("Block Trigger").gameObject;

        // Change materail to green
        blockTrigger.GetComponent<Renderer>().material = greenMaterial;

    }

    void OnTriggerExit(Collider other) {
        n.endTime = Time.time;
        NoteTracker.instance.notes.Add(n);

        // Find child GameObject named Block Trigger
        GameObject blockTrigger = transform.Find("Block Trigger").gameObject;

        // Change materail to green
        blockTrigger.GetComponent<Renderer>().material = redMaterial;
    }
}
