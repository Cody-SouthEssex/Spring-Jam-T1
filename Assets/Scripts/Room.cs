using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform doorParent;
    public Transform spawnParent;
    public bool isDoorsLocked;

    public void Start()
    {
        SetDoorLocked(true);
    }

    private void Update()
    {
        if (spawnParent.childCount == 0 && isDoorsLocked)
        {
            SetDoorLocked(false);
        }
    }

    public void SetDoorLocked(bool locked)
    {
        isDoorsLocked = locked;
        foreach (Transform child in doorParent)
        {
            if (child.TryGetComponent(out Door door))
            {
                door.locked = locked;
            }
        }
    }

    public Vector3 SetSpawnDoor()
    {
        Transform spawnDoor = doorParent.GetChild(Random.Range(0, doorParent.childCount));

        Vector3 spawnPos = spawnDoor.position;

        Destroy(spawnDoor.gameObject);

        return spawnPos;
    }

    
}
