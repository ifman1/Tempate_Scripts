using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer3DCamera : MonoBehaviour
{
    public Transform player;
    float heading = 0;
    float tilt = 15;
    float camDist = 10;
    float playerHeight = 1;

    void LateUpdate()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * 180;

        tilt = Mathf.Clamp(tilt, -80, 80);

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = player.position - transform.forward * camDist + Vector3.up*playerHeight;

    }

}
