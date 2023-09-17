using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialUI : MonoBehaviour
{
    public GameObject recordingUI, pearUI, answerWindow, comingSoonWindow;
    // Start is called before the first frame update
    void Start()
    {
        recordingUI.SetActive(false);
        pearUI.SetActive(false);    
        answerWindow.SetActive(false);
        comingSoonWindow.SetActive(false);
    }
}
