using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessCutscene : MonoBehaviour
{
    public GameObject dialogue;

    public TextAsset heroTextAsset;
    public TextAsset princessTextAsset;

    private Transform player;
    private PlayerMovement playerMovement;
    public Transform princess;

    public Dialogue currentDialogue;
    public int cutsceneIndex = 0;

    private CameraHandler cameraHandler;

    public Transform playerProposePosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDialogue)
        {
            if (currentDialogue.complete)
            {
                cutsceneIndex += 1;
                Cutscene();
            }
        }
    }

    public void Cutscene()
    {
        switch (cutsceneIndex)
        {
            default:
                cameraHandler.focus = player;
                playerMovement.lockMovement = false;
                if (currentDialogue)
                {
                    Destroy(currentDialogue.gameObject);
                }
                Destroy(gameObject);
                break;
            case 0:
                playerMovement.lockMovement = true;
                player.position = playerProposePosition.position;
                CreateDialogue(player, heroTextAsset, 0.5f);
                break;
            case 1:
                CreateDialogue(princess, princessTextAsset, 1f);
                cameraHandler.focus = princess;
                break;
        };
    }

    public void CreateDialogue(Transform target, TextAsset textAsset, float pitch)
    {
        if (currentDialogue)
        {
            Destroy(currentDialogue.gameObject);
        }

        GameObject newDialogue = Instantiate(dialogue, target.position, target.rotation, target);
        if (newDialogue.TryGetComponent(out Dialogue dialogueScript))
        {
            dialogueScript.textAsset = textAsset;
            currentDialogue = dialogueScript;
            newDialogue.GetComponent<AudioSource>().pitch = pitch;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Cutscene();
    }
}
