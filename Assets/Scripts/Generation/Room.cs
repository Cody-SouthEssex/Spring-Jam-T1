using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform doorParent;
    [HideInInspector] public Door[] doors;
    public Transform enemyParent;
    public bool isDoorsLocked;

    private void Start()
    {
        GetDoors();
        SetDoorsLocked(true);
    }

    private void Update()
    {
        if (enemyParent.childCount == 0 && isDoorsLocked)
        {
            SetDoorsLocked(false);
        }
    }

    public void GetDoors()
    {
        doors = doorParent.GetComponentsInChildren<Door>();
    }

    public void SetDoorsLocked(bool locked)
    {
        isDoorsLocked = locked;
        foreach (Door door in doors)
        {
            door.locked = locked;
        }
    }
}
