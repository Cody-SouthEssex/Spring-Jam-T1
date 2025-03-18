using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    private SoundSystem soundSystem;
    private Animator animator;
    public Transform baseObj;
    public Text textUI;
    public TextAsset textAsset;
    [SerializeField] string text;

    [SerializeField] string[] lines;
    [SerializeField] int lineIndex = -1;

    [SerializeField] J_Timer charTimer;
    [SerializeField] int charIndex = 0;

    private InputHandler inputHandler;

    public bool canSkip = true;
    public bool complete = false;

    // Start is called before the first frame update
    void Start()
    {
        soundSystem = GetComponent<SoundSystem>();

        animator = GetComponent<Animator>();

        inputHandler = FindObjectOfType<InputHandler>();

        lines = textAsset.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        NextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        baseObj.LookAt(Camera.main.transform);
        //Vector3 euler = baseObj.eulerAngles;
        //euler.y = 0;
        //baseObj.eulerAngles = euler;

        if(lineIndex > -1 && lineIndex < lines.Length)
        {
            if (charIndex < lines[lineIndex].Length)
            {
                if (charTimer.IsComplete())
                {
                    charIndex += 1;
                    charTimer.Start();
                    soundSystem.PlaySound();
                }
            }

            text = lines[lineIndex].Substring(0, charIndex);
            textUI.text = text;
        }

        if (canSkip && !complete)
        {
            if (inputHandler.GetInput(Control.SkipDialogue, KeyPressType.Down))
            {
                NextDialogue();
            }
        }
    }

    public void NextDialogue()
    {
        lineIndex += 1;

        charIndex = 0;

        text = " ";

        if(lineIndex >= lines.Length)
        {
            animator.SetTrigger("End");
            complete = true;
        }
        else
        {
            animator.SetTrigger("Next");
        }
    }
}
