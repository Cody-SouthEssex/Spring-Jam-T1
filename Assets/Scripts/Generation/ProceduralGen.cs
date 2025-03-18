using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{
    public GameObject[] battleRooms;
    // itemRooms
    public GameObject[] eventRooms;
    public int[] eventIndexes;
    // Current
    public int princessIndex;
    public GameObject princessRoom;

    // Current
    public GameObject currentRoom;
    public int currentIndex;
    [HideInInspector] public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapRoom()
    {
        currentIndex++;

        Destroy(currentRoom);

        Debris[] debris = FindObjectsOfType<Debris>();
        foreach (Debris obj in debris)
        {
            Destroy(obj.gameObject);
        }

        // Spawn Room
        if (eventIndexes.Contains(currentIndex))
        {
            currentRoom = Instantiate(eventRooms[Random.Range(0, eventRooms.Length)], Vector3.zero, Quaternion.identity, transform);
        }
        else if (currentIndex >= princessIndex)
        {
            currentRoom = Instantiate(princessRoom, Vector3.zero, Quaternion.identity, transform);
        }
        else
        {
            currentRoom = Instantiate(battleRooms[Random.Range(0, battleRooms.Length)], Vector3.zero, Quaternion.identity, transform);
        }
        if (currentRoom.TryGetComponent(out Room room))
        {
            room.GetDoors();
            room.SetDoorsLocked(true);
            Door door = room.doors[Random.Range(0, room.doors.Length)];
            player.position = door.spawnPoint.position;
            door.wasEntered = true;
        }
    }
}
