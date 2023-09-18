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
    public AudioSource soundPlayer;
    public float fadeDuration = 2.0f, startVolume;

    public Material redMaterial;
    public Material greenMaterial;
    public GameObject defaultAnchor;

    private Note currentNote;
    NoteTracker noteTracker;

    private RecordMode recordingMode;

    void Start() {
        // Find object called Note Tracker in scene, get component Note Tracker
        GameObject noteTrackerObject = GameObject.Find("Note Tracker");
        noteTracker = noteTrackerObject.GetComponent<NoteTracker>();
        pitch = transform.parent.GetComponent<KeyData>().pitch;
        //Debug.Log("PITCH: " + pitch);

        recordingMode = GameObject.Find("Recording Controller").GetComponent<RecordMode>();
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
        GetComponent<Renderer>().material = greenMaterial;
        GetComponent<Renderer>().material = greenMaterial;
        soundPlayer.Play();

        if (!recordingMode.isRecording) {
            return;
        }

        // Change the material of this gameobject
        currentNote = new Note(transform.position.x);
        currentNote.startTime = recordingMode.time; 
    }

    public void OnHandExit() {
        // Change the material of this gameobject
        GetComponent<Renderer>().material = redMaterial;

        //fading sound out
        StartCoroutine(FadeOut());

        // soundPlayer.Stop();

        if (!recordingMode.isRecording) {
            return;
        }  

        currentNote.endTime = recordingMode.time;
        if (noteTracker == null) {
            Debug.Log("Note Tracker is null");
        }
        noteTracker.notes.Add(currentNote);

        noteTracker.PrintNotes();
    }

    IEnumerator FadeOut() {
        float currentTime = 0;
        while (currentTime < fadeDuration) {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
            soundPlayer.volume = newVolume;
            yield return null;
        }
        soundPlayer.volume = 0;
        soundPlayer.Stop();
    }

    public void ResetPosition() {
        // Reset the position of this gameobject
        transform.position = defaultAnchor.transform.position;

        // Reset rotation
        transform.rotation = defaultAnchor.transform.rotation;
    }
}
