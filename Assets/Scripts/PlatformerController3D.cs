using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController3D : MonoBehaviour {


    //OBJECTS
    public Transform cam;
    CharacterController mover;

    //CAMERA
    Vector3 camF;
    Vector3 camR;

    //PHYSICS
    Vector3 intent;
    public Vector3 velocity;
    Vector3 velocityXZ;
    float speed = 5;
    float accel = 11;
    float turnSpeed = 5;
    float turnSpeedLow = 7;
    float turnSpeedHigh = 20;

    //INPUT
    Vector2 input;

    //Gravity
    float grav = 10f;
    public bool grounded = false;

    // Use this for initialization
    void Start()
    {
        mover = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        mover.Move(velocity * Time.deltaTime);
        DoJump();

    }

    void DoInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        camF = cam.forward;
        camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, .2f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void DoMove()
    {
        intent = (camF * input.y + camR * input.x);

        float tS = velocity.magnitude/5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);

        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed*Time.deltaTime);
        }

        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward*input.magnitude*speed, accel*Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);



    }

    void DoGravity()
    {
        if (grounded)
        {
            //velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }
        velocity.y = Mathf.Clamp(velocity.y, -10, 10);


    }

    void DoJump()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = 7;
                Debug.Log("Jump");
            }
        }
    }

}

