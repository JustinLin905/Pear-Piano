using System.Collections;
using UnityEngine;
using TMPro;

public class PlaybackMode : MonoBehaviour
{
    public RecordMode recordMode;

    public float currentTime = 0f;
    public TextMeshProUGUI timeText;
    public bool isPlaying = false;
    public bool isReversing = false;

    public GameObject playButtons;
    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;
            if (currentTime > recordMode.time)
            {
                currentTime = recordMode.time;
                isPlaying = false;
            }
            // UpdateTimeText();
            pauseButton.SetActive(true);
            playButtons.SetActive(false);
        }
        else if (isReversing)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
                isReversing = false;
            }
            // UpdateTimeText();
            pauseButton.SetActive(true);
            playButtons.SetActive(false);
        }
        else
        {
            pauseButton.SetActive(false);
            playButtons.SetActive(true);
        }

        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        int currentMinutes = Mathf.FloorToInt(currentTime / 60F);
        int currentSeconds = Mathf.FloorToInt(currentTime - currentMinutes * 60);
        int totalMinutes = Mathf.FloorToInt(recordMode.time / 60F);
        int totalSeconds = Mathf.FloorToInt(recordMode.time - totalMinutes * 60);

        timeText.text = string.Format("{0}:{1:00} / {2}:{3:00}", currentMinutes, currentSeconds, totalMinutes, totalSeconds);
    }

    public void Play()
    {
        isPlaying = true;
        isReversing = false;
    }

    public void Reverse()
    {
        isPlaying = false;
        isReversing = true;
    }

    public void Pause() 
    {
        isPlaying = false;
        isReversing = false;
    }
}
