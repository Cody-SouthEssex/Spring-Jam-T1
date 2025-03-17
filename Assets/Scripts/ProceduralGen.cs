using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject currentRoom;
    [HideInInspector] public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapRoom()
    {
        ExitRoom();

        // Spawn Room
        currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], Vector3.zero, Quaternion.identity, transform);
        if(currentRoom.TryGetComponent(out Room room))
        {
            player.position = room.SetSpawnDoor();
        }
    }

    private void ExitRoom()
    {
        // Cleanup
        Destroy(currentRoom);
    }
}
