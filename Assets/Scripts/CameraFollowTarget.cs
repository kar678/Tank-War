using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    //What we are following
    private GameObject playerTank;

    //Zeros out the velocity
    Vector3 velocity = Vector3.zero;

    //Time to follow target
    public float smoothTime = .15f;


    //Enable and set the maximum Y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0f;

    //Enable and set the min Y value
    public bool YMinEnabled = false;
    public float YMinValue = 0f;

    //Enable and set the maximum X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0f;

    //Enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0f;

    void Start()
    {
        playerTank = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //target position
        Vector3 targetPos = playerTank.transform.position;

        //Vertical Clamp
        if (YMinEnabled && YMaxEnabled)
            targetPos.y = Mathf.Clamp(playerTank.transform.position.y, YMinValue, YMaxValue);

        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(playerTank.transform.position.y, YMinValue, playerTank.transform.position.y);

        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(playerTank.transform.position.y, playerTank.transform.position.y, YMaxValue);

        //Horizontal Clamp

        if (XMinEnabled && XMaxEnabled)
            targetPos.x = Mathf.Clamp(playerTank.transform.position.x, XMinValue, XMaxValue);

        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(playerTank.transform.position.x, XMinValue, playerTank.transform.position.x);

        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(playerTank.transform.position.x, playerTank.transform.position.x, XMaxValue);




        targetPos.z = playerTank.transform.position.z + -10;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
