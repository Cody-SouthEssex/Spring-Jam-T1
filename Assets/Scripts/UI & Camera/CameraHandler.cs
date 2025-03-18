using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject player;
    public Transform focus;
    public float lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            focus = player.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (focus)
        {
            transform.position = Vector3.Lerp(transform.position, focus.position, Time.deltaTime * lookSpeed);
        }
    }
}
