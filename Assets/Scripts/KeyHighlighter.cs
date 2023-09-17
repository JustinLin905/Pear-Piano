using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHighlighter : MonoBehaviour
{
    public GameObject[] sharps = new GameObject[7];
    public GameObject[] flats = new GameObject[7];
    public Material highlight;
    public Material normal;
    

    public void HighlightKeys(string answer) {
        // if we're highlighting chords
        if (answer.Substring(0, 1) != "T") {
            GameObject trigger;
            if (answer.Contains("#")) {
                int [] indices = getSharpKeySigIndices(answer);
                foreach (int i in indices) {
                    Debug.Log("INDEX: " + i);
                    // Find trigger in children of sharps[i]
                    trigger = sharps[i].transform.Find("Hand Trigger").gameObject;
                    StartCoroutine(HighlightTrigger(trigger));
                }
            } else {
                int [] indices = getFlatKeySigIndices(answer);
                Debug.Log("FLAT INDICES: " + indices.Length);
                foreach (int i in indices) {
                    trigger = flats[i].transform.Find("Hand Trigger").gameObject;
                    StartCoroutine(HighlightTrigger(trigger));
                }
            }
        }
    }

    private int[] getSharpKeySigIndices(string s) {
        if (s.Contains("B#")) return new int[] {0, 1, 2, 3, 4, 5, 6};
        if (s.Contains("E#")) return new int[] {0, 1, 2, 3, 4, 5};
        if (s.Contains("A#")) return new int[] {0, 1, 3, 4, 5};
        if (s.Contains("D#")) return new int[] {0, 1, 3, 4};
        if (s.Contains("G#")) return new int[] {0, 3, 4};
        if (s.Contains("C#")) return new int[] {0, 3};
        if (s.Contains("F#")) return new int[] {0};
        return new int[] {};
    }

    private int[] getFlatKeySigIndices(string s) {
        if (s.Contains("F♭")) return new int[] {0, 1, 2, 3, 4, 5, 6};
        if (s.Contains("C♭")) return new int[] {0, 1, 2, 4, 5, 6};
        if (s.Contains("G♭")) return new int[] {1, 2, 4, 5, 6};
        if (s.Contains("D♭")) return new int[] {1, 2, 5, 6};
        if (s.Contains("A♭")) return new int[] {2, 5, 6};
        if (s.Contains("E♭")) return new int[] {2, 6};
        if (s.Contains("B♭")) return new int[] {6};
        return new int[] {};
    }

    IEnumerator HighlightTrigger(GameObject trigger) {
        Debug.Log("HIGHLIGHTING TRIGGER");
        trigger.GetComponent<Renderer>().material = highlight;
        yield return new WaitForSeconds(10f);
        trigger.GetComponent<Renderer>().material = normal;
    }
}
