using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    public Note(string p) {
        pitch = p;
    }
    public string pitch;
    public float startTime;
    public float endTime;
}
