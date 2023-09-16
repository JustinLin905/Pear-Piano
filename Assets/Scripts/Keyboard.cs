using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public GameObject blackKey;
    public GameObject whiteKey;

    // (pitch, relativeOffset)
    private (string, float) [] keyRef = {
        ("C", 0), ("C#", 0.5f), ("D", 1), ("D#", 1.5f), ("E", 2), ("F", 3), ("F#", 3.5f), ("G", 4), 
        ("G#", 4.5f), ("A", 5), ("A#", 5.5f), ("B", 6)
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateOctave(int octave) {
        for (int i = 0; i < 7; i++) {
            
            GameObject keyPrefab;
            if (i == 1 || i == 3 || i == 6 || i == 8 || i == 10) keyPrefab = blackKey;
            else keyPrefab = whiteKey; 

            GameObject key = instantiateKey(keyPrefab, keyRef[i].Item1, octave);
            float offset = keyRef[i].Item2 + (octave * 7);
        }
    }

    private GameObject instantiateKey(GameObject key, string pitch, int octave) {
        GameObject newKey = Instantiate(key);
        newKey.GetComponent<HandTrigger>().pitch = pitch + octave;
        // TODO: set audioclip
        return newKey;
    }
}
