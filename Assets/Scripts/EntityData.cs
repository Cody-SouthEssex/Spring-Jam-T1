using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    None,
    Player,
    Enemy,
}

public class EntityData : MonoBehaviour
{
    public Team team;
}
