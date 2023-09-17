using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PearAnswer : MonoBehaviour
{
    public PearAnimations pearAnimations;
    public GameObject answerWindow;
    public TextMeshProUGUI answerText;

    private Animator windowAnimator;

    // Start is called before the first frame update
    void Start()
    {
        windowAnimator = answerWindow.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveAnswer(string answer)
    {
        answerWindow.SetActive(true);
        answerText.text = answer;
        // Play animation on windowAnimator named "Grow Window"
        windowAnimator.Play("Grow Window");
        pearAnimations.PearAnswerAnimation();
    }
}
