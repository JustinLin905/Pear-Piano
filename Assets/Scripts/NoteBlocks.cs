using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBlocks : MonoBehaviour
{
    NoteTracker noteTracker;
    public float speed = 1; // set negative when moving blocks
    public float zOffset = 1;
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
        
    }

    private void LoadNoteBlocks () {
        if (noteTracker == null) return;

        foreach (Note n in noteTracker.notes) {
            InstantiateNoteBlock(n);
        }

    }

    private void InstantiateNoteBlock(Note note) {
        GameObject newNoteBlock = Instantiate(noteBlock);
        float zPos = zOffset + note.startTime * speed;
        float zScale = note.endTime - note.startTime * speed;

        Vector3 pos = newNoteBlock.transform.position;
        pos.z = zPos;
        pos.x = note.xPos;
        newNoteBlock.transform.position = pos;
    }
}
