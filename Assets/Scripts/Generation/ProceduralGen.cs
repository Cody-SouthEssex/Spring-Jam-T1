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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapRoom()
    {
        Destroy(currentRoom);

        // Spawn Room
        currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)], Vector3.zero, Quaternion.identity, transform);
        if(currentRoom.TryGetComponent(out Room room))
        {
            room.GetDoors();
            room.SetDoorsLocked(true);
            Door door = room.doors[Random.Range(0, room.doors.Length)];
            player.position = door.spawnPoint.position;
            door.wasEntered = true;
        }
    }
}
