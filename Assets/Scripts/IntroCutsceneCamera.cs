using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneCamera : MonoBehaviour
{
    private InputHandler inputHandler;
    private CameraHandler cameraHandler;
    public Transform imageParent;

    public int imageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        cameraHandler = FindObjectOfType<CameraHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputHandler.GetInput(Control.SkipDialogue, KeyPressType.Down))
        {
            imageIndex += 1;
            if (imageIndex >= imageParent.childCount)
            {
                SceneManager.LoadScene("Gameplay");
                return;
            }
            cameraHandler.focus = imageParent.GetChild(imageIndex);
        }
    }
}
