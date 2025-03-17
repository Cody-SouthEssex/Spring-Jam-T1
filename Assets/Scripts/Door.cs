using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private ProceduralGen pg;
    public Room roomRoot;
    private Transform player;

    public bool locked = true;
    public float range = 5;

    // Start is called before the first frame update
    void Start()
    {
        pg = roomRoot.GetComponentInParent<ProceduralGen>();
        player = pg.player;
    }

    // Update is called once per frame
    void Update()
    {
        EnterDoor();
    }

    public void EnterDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= range)
            {
                if (!locked)
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
