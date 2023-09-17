using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PearAnimations : MonoBehaviour
{
    public GameObject pear;
    public GameObject askPearUI;
    private Animator pearAnimator;
    private bool pearActive = false;
    public Image pearFace;
    public Sprite answerFace;
    public Sprite neutralFace;

    // Start is called before the first frame update
    void Start()
    {
        pear.SetActive(true);
        pearAnimator = pear.GetComponent<Animator>();
        pear.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (askPearUI.activeSelf && !pearActive)
        {
            StartCoroutine(appearPear());
        }
        else if (!askPearUI.activeSelf && pearActive)
        {
            pearActive = false;
            pear.SetActive(false);
        }

    }

    IEnumerator appearPear()
    {
        pear.SetActive(true);
        pearAnimator.Play("Pear Grow");
        yield return new WaitForSeconds(1.05f);
        pearAnimator.Play("Pear Idle");
        pearActive = true;
    }

    IEnumerator pearAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        pearFace.sprite = answerFace;
        pearAnimator.Play("Pear Jump");
        yield return new WaitForSeconds(3f);
        pearFace.sprite = neutralFace;
        pearAnimator.Play("Pear Idle");
    }

    public void PearAnswerAnimation()
    {
        StartCoroutine(pearAnswer());
    }
}
