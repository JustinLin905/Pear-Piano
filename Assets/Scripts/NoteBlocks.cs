using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlocks : MonoBehaviour
{
    public PlaybackMode playbackMode;

    NoteTracker noteTracker;
    public float speed = 1; // set negative when moving blocks
    public float zOffset = 0;
    public GameObject noteBlock;
    public float songStartTime;
    // Start is called before the first frame update
    void Start()
    {
        GameObject noteTrackerObject = GameObject.Find("Note Tracker");
        noteTracker = noteTrackerObject.GetComponent<NoteTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playbackMode.isPlaying) {
            MoveNoteBlocks();
        }

        else if (playbackMode.isReversing) {
            MoveNoteBlocksReverse();
        }
    }

    public void LoadNoteBlocks () {
        if (noteTracker == null) return;

        foreach (Note n in noteTracker.notes) {
            InstantiateNoteBlock(n);
        }

    }

    private void InstantiateNoteBlock(Note note) {
        GameObject newNoteBlock = Instantiate(noteBlock);
        float zPos = zOffset + (note.startTime * speed);
        float zScale = (note.endTime - note.startTime) * speed;

        Vector3 pos = newNoteBlock.transform.position;
        pos.z = zPos;
        pos.x = note.xPos;
        newNoteBlock.transform.position = pos;
        
        // Set Z scale of Note Block
        Vector3 scale = newNoteBlock.transform.localScale;
        scale.z = zScale;
        newNoteBlock.transform.localScale = scale;

        // Set tag to Note Block
        newNoteBlock.tag = "Note Block";

        Debug.Log("Instantiated Note Block at " + pos);
    }

    private void MoveNoteBlocks() {
        // Find all Note Blocks
        GameObject[] noteBlocks = GameObject.FindGameObjectsWithTag("Note Block");

        // Move each Note Block
        foreach (GameObject noteBlock in noteBlocks) {
            Vector3 pos = noteBlock.transform.position;
            pos.z -= speed * Time.deltaTime;
            noteBlock.transform.position = pos;
        }
    }

    private void MoveNoteBlocksReverse() {
        // Find all Note Blocks
        GameObject[] noteBlocks = GameObject.FindGameObjectsWithTag("Note Block");

        // Move each Note Block
        foreach (GameObject noteBlock in noteBlocks) {
            Vector3 pos = noteBlock.transform.position;
            pos.z += speed * Time.deltaTime;
            noteBlock.transform.position = pos;
        }
    }
}
