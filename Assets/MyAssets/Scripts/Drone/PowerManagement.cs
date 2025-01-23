using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManagement : MonoBehaviour
{
    public DroneController droneController;

    Rigidbody rbody;
    float maxVelocity = 15.0f;
    public float speed = 2.4538f;
    public rotor helixO1, helixO2, helixV1, helixV2;

    public bool flight = true;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody != null)
            speed *= rbody.mass;
        StartMovement(speed);
    }

    private void FixedUpdate()
    {
        if (droneController != null)
        {
            if (droneController.speedLimite)
                LimiteSpeed();
        }

        //ControllSpeed();
    }

    public void StartMovement(float speed)
    {
        helixO1.setPower(speed);
        helixO2.setPower(speed);
        helixV1.setPower(speed);
        helixV2.setPower(speed);
    }

    //X자 모양 드론 무빙
    public void DronMove(float addPowerX, float addPowerY)
    {
        helixO1.setAddPowerL(-addPowerX - addPowerY);  //오른쪽 전방
        helixV2.setAddPowerL(addPowerY - addPowerX);   //오른쪽 후방
        helixO2.setAddPowerL(addPowerX + addPowerY);   //왼쪽 후방
        helixV1.setAddPowerL(addPowerX - addPowerY);   //왼쪽 전방
    }

    public void DroneMove(float addPower)
    {        
        helixV2.setAddPowerL(addPower);
        helixV1.setAddPowerL(addPower);
        helixO2.setAddPowerL(addPower);
        helixO1.setAddPowerL(addPower);        
    }

    public void DronUD(float addPower)
    {
        helixV2.setAddPowerR(addPower);
        helixV1.setAddPowerR(addPower);
        helixO2.setAddPowerR(addPower);
        helixO1.setAddPowerR(addPower);
    }


    //속도 제한
    void LimiteSpeed()
    {
        float xSpeed = rbody.velocity.x;
        float ySpeed = rbody.velocity.y;
        float zSpeed = rbody.velocity.z;

        if (xSpeed >= maxVelocity)
        {
            rbody.velocity = new Vector3(maxVelocity, rbody.velocity.y, rbody.velocity.z);
        }
        else if(xSpeed <= -maxVelocity)
        {
            rbody.velocity = new Vector3(-maxVelocity, rbody.velocity.y, rbody.velocity.z);
        }
        if (ySpeed >= maxVelocity)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, maxVelocity, rbody.velocity.z);
        }
        else if (ySpeed <= -maxVelocity)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, -maxVelocity, rbody.velocity.z);
        }
        if (zSpeed >= maxVelocity)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, maxVelocity);
        }
        else if (zSpeed <= -maxVelocity)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, -maxVelocity);
        }
    }
}






////기울기에 따른 속도조절
//void ControllSpeed()
//{
//    Vector3 localVelocity;
//    float speedX = 0, speedZ = 0;
//    Vector3 forward = transform.forward;
//    Vector3 right = transform.right;
//    Vector3 up = transform.up;

//    if (transform.localEulerAngles.z <= 180)
//        speedX = -(int)(transform.localEulerAngles.z * 100) / 100f;
//    else if (transform.localEulerAngles.z > 180)
//        speedX = (int)((360f - transform.localEulerAngles.z) * 100) / 100f;

//    if (transform.localEulerAngles.x <= 180)
//        speedZ = (int)(transform.localEulerAngles.x * 100) / 100f;
//    else if(transform.localEulerAngles.x > 180)
//        speedZ = -(int)((360f - transform.localEulerAngles.x) * 100) / 100f;

//    //Debug.Log("localEulerAngles.x, z [ " + transform.localEulerAngles.x + ", " + transform.localEulerAngles.z + "]");
//    //Debug.Log("speedX, Z [" + speedX +", " + speedZ + "]");
//    //Debug.Log("speedX, Z [" + speedX * transform.forward.x + ", " + speedZ * transform.forward.x + "]");
//    //Debug.Log("X = " + forward * speedX);
//    //Debug.Log("Z = " + forward * speedZ);

//    //localVelocity = new Vector3(speedX * transform.forward.x, rbody.velocity.y, speedZ * transform.forward.z);      

//    //rbody.velocity = new Vector3(rbody.velocity.x, speedY, rbody.velocity.z);

//    forward *= speedZ;
//    right *= speedX;
//    up *= dronController.rightStick.axis.y * 20f;

//    localVelocity = forward + right + up;
//    rbody.velocity = localVelocity;

//    /*
//    float speedY = rbody.velocity.y;

//    rbody.velocity = transform.forward * speedZ;
//    localVelocity.z = rbody.velocity.z;
//    rbody.velocity = transform.right * speedX;
//    localVelocity.x = rbody.velocity.x;

//    localVelocity.y = speedY;
//    rbody.velocity = localVelocity;
//    */
//}