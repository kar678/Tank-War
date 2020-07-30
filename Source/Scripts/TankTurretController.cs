using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretController : MonoBehaviour
{
    public int rotationOffset = 0;    // This will offset the rotation of the object as it tracks the mouse. This is required to correctly set rotation values
    public int rotationSpeed = 200;
    public bool disableControls = false;

    void FixedUpdate()
    {
        if (!disableControls)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // This will calculate the distance between the mouse in the game and the position of the tank turret
            difference.Normalize();    // This returns simplified values which makes it easier to work with


            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;    // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle + rotationOffset), rotationSpeed * Time.deltaTime); // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
        }
    }
}
