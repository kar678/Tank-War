using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    /////*******************************************/////
    /////                   VARS                    /////  
    /////*******************************************/////

    //Movement Vars

    bool moveForward = false;
    bool moveReverse = false;
    float moveSpeed = 0f;
    float moveSpeedReverse = 0f;
    public float moveAcceleration = 0.1f;
    public float moveDeceleration = 0.20f;
    public float moveSpeedForwardMax = 2.5f;
    public float moveSpeedReverseMax = -1.0f;
    float actualMaxForwardSpeed;
    float actualMaxReverseSpeed;

    bool rotateRight = false;
    bool rotateLeft = false;
    float rotateSpeedRight = 0f;
    float rotateSpeedLeft = 0f;
    public float rotateAcceleration = 4f;
    public float rotateDeceleration = 10f;
    public float rotateSpeedMax = 130f;
    public float rotateSpeedMaxInverted = -130f;
    float actualMaxRotateSpeedLeft;
    float actualMaxRotateSpeedRight;

    Rigidbody2D rb;

    //Health Vars

    public float maxHitPoints = 100;
    public float currentHitPoints = 100;

    //Sound Vars

    public bool oneEngineSound;
    AudioSource sourceSound;
    public AudioClip engineSound;
    public AudioClip idleEngineSound;
    public AudioClip activeEngineSound;
    public bool disableControls = false;

    // Misc Vars

    Vector2 d;

    void Start()
    {
       sourceSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    /////*******************************************/////
    /////                 UPDATE                    /////  
    /////*******************************************/////

    void Update()
    {
        d = transform.up;

        actualMaxForwardSpeed = moveSpeedForwardMax * Input.GetAxis("MoveForwardBackward");
        actualMaxReverseSpeed = moveSpeedReverseMax * Input.GetAxis("MoveForwardBackward");
        actualMaxRotateSpeedLeft = rotateSpeedMax * Input.GetAxis("TurnLeftRight");
        actualMaxRotateSpeedRight = rotateSpeedMaxInverted * Input.GetAxis("TurnLeftRight");

        if (!disableControls)
        {
            rotateLeft = (Input.GetAxis("TurnLeftRight") > 0) ? true : rotateLeft;
            rotateLeft = (Input.GetAxis("TurnLeftRight") <= 0) ? false : rotateLeft;

            rotateRight = (Input.GetAxis("TurnLeftRight") < 0) ? true : rotateRight;
            rotateRight = (Input.GetAxis("TurnLeftRight") >= 0) ? false : rotateRight;

            moveForward = (Input.GetAxis("MoveForwardBackward") > 0) ? true : moveForward;
            moveForward = (Input.GetAxis("MoveForwardBackward") <= 0) ? false : moveForward;

            moveReverse = (Input.GetAxis("MoveForwardBackward") < 0) ? true : moveReverse;
            moveReverse = (Input.GetAxis("MoveForwardBackward") >= 0) ? false : moveReverse;

            if (moveForward | moveReverse)
            {
                if (!oneEngineSound)
                {
                    if (idleEngineSound)
                    {
                        if (sourceSound.isPlaying && sourceSound.clip == idleEngineSound)
                        {
                            sourceSound.Stop();
                        }
                    }

                    if (activeEngineSound)
                    {
                        if (!sourceSound.isPlaying)
                        {
                            sourceSound.clip = activeEngineSound;
                            sourceSound.Play();
                        }
                    }
                }
                else if (oneEngineSound)
                {
                    if (engineSound)
                    {
                        if (!sourceSound.isPlaying)
                        {
                            sourceSound.clip = engineSound;
                            sourceSound.Play();
                        }

                        if (moveReverse && !moveForward)
                        {
                            sourceSound.pitch = (moveSpeedReverse / moveSpeedForwardMax - 1) * -1;
                        }

                        if (moveForward && !moveReverse)
                        {
                            sourceSound.pitch = moveSpeed / moveSpeedForwardMax + 1;
                        }
                    }
                }
            }
            else
            {
                if (!oneEngineSound)
                {
                    if (activeEngineSound)
                    {
                        if (sourceSound.isPlaying && sourceSound.clip == activeEngineSound)
                        {
                            sourceSound.Stop();
                        }
                    }

                    if (idleEngineSound)
                    {
                        if (!sourceSound.isPlaying)
                        {
                            sourceSound.clip = idleEngineSound;
                            sourceSound.Play();
                        }
                    }
                }
                else if (oneEngineSound)
                {
                    if (engineSound)
                    {
                        if (!sourceSound.isPlaying)
                        {
                            sourceSound.clip = engineSound;
                            sourceSound.Play();
                        }

                        if (sourceSound.pitch != 1)
                        {
                            sourceSound.pitch = 1;
                        }
                    }
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if(!disableControls)
        {
            if (rotateLeft)
            {
                rotateSpeedLeft = (rotateSpeedLeft < actualMaxRotateSpeedLeft) ? rotateSpeedLeft + rotateAcceleration : actualMaxRotateSpeedLeft;
            }
            else
            {
                rotateSpeedLeft = (rotateSpeedLeft > 0) ? rotateSpeedLeft - rotateDeceleration : 0;
            }
            transform.Rotate(0f, 0f, rotateSpeedLeft * Time.deltaTime);

            if (rotateRight)
            {
                rotateSpeedRight = (rotateSpeedRight < actualMaxRotateSpeedRight) ? rotateSpeedRight + rotateAcceleration : actualMaxRotateSpeedRight;
            }
            else
            {
                rotateSpeedRight = (rotateSpeedRight > 0) ? rotateSpeedRight - rotateDeceleration : 0;
            }
            transform.Rotate(0f, 0f, rotateSpeedRight * Time.deltaTime * -1);

            if (moveForward)
            {
                moveSpeed = (moveSpeed < actualMaxForwardSpeed) ? moveSpeed + moveAcceleration : actualMaxForwardSpeed;
                rb.AddForce(d * moveSpeed * Time.deltaTime);

            }
            else
            {
                moveSpeed = (moveSpeed > 0) ? moveSpeed - moveDeceleration : 0;
            }

            if (moveReverse)
            {
                moveSpeedReverse = (moveSpeedReverse < actualMaxReverseSpeed) ? moveSpeedReverse + moveAcceleration : actualMaxReverseSpeed;
                rb.AddForce(d * moveSpeedReverse * Time.deltaTime);
            }
            else
            {
                moveSpeedReverse = (moveSpeedReverse > 0) ? moveSpeedReverse - moveDeceleration : 0;
            }
        }
    }

    /////*******************************************/////
    /////             Health Functions              /////  
    /////*******************************************/////

    public void TakeDamage(float damage)
    {
        currentHitPoints = (damage > 0) ? currentHitPoints - damage : 0;
    }

    public void Heal(float healPoints)
    {
        currentHitPoints = (healPoints > 0) ? currentHitPoints + healPoints : maxHitPoints;
    }
}
