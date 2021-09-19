using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 moveDir;
    private Vector3 newPoz, gravity;
    public float laneDistance;
    public float firstLane;
    private int laneNumber = 1;
    private int lanesCount = 2;
    public float LaneSpeed = 50;
    public float speedForward = 10f;
    public float jumpForce;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        moveDir = new Vector3(0, 0, 1);
    }

    void Update()
    {
        if (cc.isGrounded)
        {
            Debug.Log("isGrounede");
            gravity = Vector3.zero;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                gravity.y = jumpForce;
            }
        }
        else
        {
            Debug.Log("isNOTGrounede");

            gravity += Physics.gravity * 3 * Time.deltaTime;
        }


        moveDir = new Vector3(0, 0, 1);
        moveDir.y += gravity.y;
        moveDir.z = speedForward;
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > .1f)
        {
            laneNumber += (int)Mathf.Sign(input);
            laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
        }
        cc.Move(moveDir * Time.deltaTime);
        newPoz = transform.position;
        newPoz.x = Mathf.Lerp(newPoz.x, firstLane + (laneNumber * laneDistance), LaneSpeed * Time.deltaTime);
        transform.position = newPoz;


    }
}
