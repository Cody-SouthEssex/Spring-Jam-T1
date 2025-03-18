using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform focus;
    public float lookSpeed;

    // Start is called before the first frame update
    void Start()
    {
        focus = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, focus.position, Time.deltaTime * lookSpeed);
    }
}
