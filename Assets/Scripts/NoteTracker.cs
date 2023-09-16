using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteTracker : MonoBehaviour
{
    
    public List<Note> notes = new List<Note>();

    void Start() {
        //notes = new List<Note>();
    }

    public void PrintNotes() {
        foreach (Note n in notes) {
            Debug.Log("LOGGED NOTE: " + n.xPos + " " + n.startTime + " " + n.endTime);
        }
    }
}
