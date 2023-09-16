using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTracker : MonoBehaviour
{
    public List<Note> notes;

    public void PrintNotes() {
        foreach (Note n in notes) {
            Debug.Log("LOGGED NOTE: " + n.pitch + " " + n.startTime + " " + n.endTime);
        }
    }
}
