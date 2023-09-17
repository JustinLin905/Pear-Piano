using System.Collections;
using UnityEngine;
using TMPro;

public class RecordMode : MonoBehaviour
{
    public NoteBlocks noteBlocks;

    public GameObject beforeRecording;
    public GameObject afterRecording;
    public GameObject playbackUI;
    public float time = 0f;
    public TextMeshProUGUI header;
    public TextMeshProUGUI timerText;
    public GameObject mainMenu;

    public bool isRecording = false;

    // Start is called before the first frame update
    void Start()
    {
        beforeRecording.SetActive(true);
        afterRecording.SetActive(false);
        playbackUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If recording, increment the time and update the timerText
        if (isRecording)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void Record() 
    {
        beforeRecording.SetActive(false);
        afterRecording.SetActive(true);
        mainMenu.SetActive(false);
        time = 0f;
        isRecording = true;

        header.text = "Recording";
    }

    public void StopRecording()
    {
        isRecording = false;
        header.text = "Playback";
        afterRecording.SetActive(false);
        playbackUI.SetActive(true);

        // GENERATE BLOCKS
        noteBlocks.LoadNoteBlocks();
    }
}
