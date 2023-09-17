using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNoteBlock : MonoBehaviour
{
    private PlaybackMode playbackMode;

    private float defaultY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playbackMode = GameObject.Find("Playback Controller").GetComponent<PlaybackMode>();
        defaultY = transform.position.y;
    }

    public void DropNoteBlock()
    {
        // Set this note block's X position to nearest 0.01f
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x * 100f) / 100f;
        pos.z = Mathf.Round(pos.z * 100f) / 100f;
        pos.y = defaultY;
        transform.position = pos;

        // Set rotation to 0 in x, y, z
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Set this note block's Y position to default
        pos.y = defaultY;
    }

}
