using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearAnimations : MonoBehaviour
{
    public GameObject pear;
    public GameObject askPearUI;
    private Animator pearAnimator;
    private bool pearActive = false;

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
}
