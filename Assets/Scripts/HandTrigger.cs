 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OculusSampleFramework;

public class HandTrigger : MonoBehaviour
{
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;
    public string pitch;

    public AudioClip sound;
    public Material redMaterial;
    public Material greenMaterial;
    public GameObject defaultAnchor;

    private Note currentNote;
    NoteTracker noteTracker;

    void Start() {
        // Find object called Note Tracker in scene, get component Note Tracker
        GameObject noteTrackerObject = GameObject.Find("Note Tracker");
        noteTracker = noteTrackerObject.GetComponent<NoteTracker>();
        pitch = transform.parent.GetComponent<KeyData>().pitch;
        Debug.Log("PITCH: " + pitch);
    }


    void InitiateEvent(InteractableStateArgs state) {
        if (state.NewInteractableState == InteractableState.ActionState) {
            actionEvent.Invoke();
        }

        if (state.NewInteractableState == InteractableState.Default) {
            defaultEvent.Invoke();
        }
    }

    public void OnHandEnter() {
        // Change the material of this gameobject
        currentNote = new Note(pitch);

        GetComponent<Renderer>().material = greenMaterial;
        currentNote.startTime = Time.time;
    }

    public void OnHandExit() {
        // Change the material of this gameobject
        GetComponent<Renderer>().material = redMaterial;
        currentNote.endTime = Time.time;
        if (noteTracker == null) {
            Debug.Log("Note Tracker is null");
        }
        noteTracker.notes.Add(currentNote);

        noteTracker.PrintNotes();
    }

    public void ResetPosition() {
        // Reset the position of this gameobject
        transform.position = defaultAnchor.transform.position;

        // Reset rotation
        transform.rotation = defaultAnchor.transform.rotation;
    }
}
