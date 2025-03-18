using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private InputHandler ih;

    private ProceduralGen pg;
    private Transform player;
    public Transform spawnPoint;

    public bool wasEntered = false;
    public bool locked = true;
    public float range = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ih = FindObjectOfType<InputHandler>();
        pg = GetComponentInParent<ProceduralGen>();
        player = pg.player;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOpen", !locked && !wasEntered);

        EnterDoor();
    }

    public void EnterDoor()
    {
        if (ih.GetInput(Control.Interact, KeyPressType.Down))
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= range)
            {
                if (!locked && !wasEntered)
                {
                    pg.SwapRoom();
                }
                else
                {
                    // Play noise
                }
            }
        }
    }
}
