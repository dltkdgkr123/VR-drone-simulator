using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    Rigidbody rbody;
    PowerManagement pwrMgr;

    //각도별 수평 복원 Rotate 속도
    float recoverAngleSpeed1 = 0.05f;   //0~recoverAngle1
    float recoverAngleSpeed2 = 0.1f;    //recoverAngle1~recoverAngle2
    float recoverAngleSpeed3 = 0.5f;      //recoverAngle2 이상

    //수평 복원 각도
    float recoverAngle1 = 5f;
    float recoverAngle2 = 20f;

    //수평 복원시 속도 감소 속도
    float recoverSpeed = 0.05f;

    //기울기 최대각
    float maxAngle = 20.0f;

    //rbody 초기화
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        pwrMgr = GetComponent<PowerManagement>();
    }

    //각도 제한
    public void LimiteAngle(float inputX, float inputZ)
    {
        //maxAngle * 입력값(-1.0 ~ 1.0의 절대값 = 0.0 ~ 1.0)
        float angleX = maxAngle * Mathf.Abs(inputZ);
        float angleZ = maxAngle * Mathf.Abs(inputX);

        //x축이 angleX ~ 170 도 일 때
        if (transform.eulerAngles.x >= angleX && transform.eulerAngles.x <= 180f - angleX)
        {
            rbody.angularVelocity = new Vector3(0f, rbody.angularVelocity.y, rbody.angularVelocity.z);
            transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        //x축이 190 ~ 360-angleX도 일 때
        else if (transform.eulerAngles.x <= 360f - angleX && transform.eulerAngles.x >= 180f + angleX)
        {
            rbody.angularVelocity = new Vector3(0, rbody.angularVelocity.y, rbody.angularVelocity.z);
            transform.eulerAngles = new Vector3(360f - angleX, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        //z축이 angleZ ~ 170 도 일 때
        if (transform.eulerAngles.z >= angleZ && transform.eulerAngles.z <= 180f - angleZ)
        {
            rbody.angularVelocity = new Vector3(rbody.angularVelocity.x, rbody.angularVelocity.y, 0f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f + angleZ);
        }
        //z축이 190 ~ 360-angleZ도 일 때
        else if (transform.eulerAngles.z <= 360f - angleZ && transform.eulerAngles.z >= 180f + angleZ)
        {
            rbody.angularVelocity = new Vector3(rbody.angularVelocity.x, rbody.angularVelocity.y, 0f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 360f - angleZ);
        }
    }

    //수평 복원
    public void RecoverAngle()
    {
        //각도 복원을 위해 회전속도 초기화
        rbody.angularVelocity = Vector3.zero;

        //Debug.Log(transform.eulerAngles);
        //Debug.Log("수평 복원");

        //X축 평행 조절
        if (transform.eulerAngles.x >= 0.1f && transform.eulerAngles.x <= 179.9f)
        {
            //Debug.Log("X축 양수");
            if (transform.eulerAngles.x >= recoverAngle2)
            {
                //Debug.Log("x축 20도 이상");
                transform.Rotate(-recoverAngleSpeed3, 0, 0);
            }
            else if (transform.eulerAngles.x >= recoverAngle1)
            {
                transform.Rotate(-recoverAngleSpeed2, 0, 0);
                //Debug.Log("x축 5도 이상");
            }
            else
            {
                //Debug.Log("x축 마무리 단계1");
                transform.Rotate(-recoverAngleSpeed1, 0, 0);
            }
        }
        else if (transform.eulerAngles.x < 359.9f && transform.eulerAngles.x >= 180.1f)
        {
            //Debug.Log("X축 음수");
            if (transform.eulerAngles.x <= 360f - recoverAngle2)
            {
                //Debug.Log("x축 -20도 이하");
                transform.Rotate(recoverAngleSpeed3, 0, 0);
            }
            else if (transform.eulerAngles.x <= 360f - recoverAngle1)
            {
                //Debug.Log("x축 -5도 이하");
                transform.Rotate(recoverAngleSpeed2, 0, 0);
            }
            else
            {
                //Debug.Log("x축 마무리 단계2");
                transform.Rotate(recoverAngleSpeed1, 0, 0);
            }
        }
        else
        {
           // Debug.Log("X축 수평");
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);

            //속도 감소
            if (rbody.velocity.x > 0)
            {
                //Debug.Log("x 속도 감소");
                rbody.velocity = new Vector3(rbody.velocity.x - recoverSpeed, rbody.velocity.y, rbody.velocity.z);
            }
            else if (rbody.velocity.x < 0)
            {
               // Debug.Log("x 속도 증가");
                rbody.velocity = new Vector3(rbody.velocity.x + recoverSpeed, rbody.velocity.y, rbody.velocity.z);
            }
        }

        //Z축 평행 조절
        if (0.1f <= transform.eulerAngles.z && transform.eulerAngles.z <= 179.9f)   //0.1 <= z <= 179.9
        {
            //Debug.Log("Z축 양수");
            if (transform.eulerAngles.z >= recoverAngle2)
            {
                //Debug.Log("Z축 20도 이상");
                transform.Rotate(0, 0, -recoverAngleSpeed3);
            }
            else if (transform.eulerAngles.z >= recoverAngle1)
            {
                //Debug.Log("Z축 5도 이상");
                transform.Rotate(0, 0, -recoverAngleSpeed2);
            }
            else
            {
                //Debug.Log("Z축 마무리 단계1");
                transform.Rotate(0, 0, -recoverAngleSpeed1);
            }
        }
        else if (180.1f <= transform.eulerAngles.z && transform.eulerAngles.z <= 359.9f)   //180.1 <= z <= 359.9
        {
            //Debug.Log("Z축 음수");
            if (transform.eulerAngles.z <= 360f - recoverAngle2)
            {
                //Debug.Log("Z축 -20도 이하");
                transform.Rotate(0, 0, recoverAngleSpeed3);
            }
            else if (transform.eulerAngles.z <= 360f - recoverAngle1)
            {
                //Debug.Log("Z축 -5도 이하");
                transform.Rotate(0, 0, recoverAngleSpeed2);
            }
            else
            {
                //Debug.Log("Z축 마무리 단계2");
                transform.Rotate(0, 0, recoverAngleSpeed1);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);   //x의 값이 변동될 가능성 o

            if (rbody.velocity.z > 0)
            {
                //Debug.Log("z 속도 감소");
                rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, rbody.velocity.z - recoverSpeed);
            }
            else if (rbody.velocity.z < 0)
            {
                //Debug.Log("z 속도 증가");
                rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, rbody.velocity.z + recoverSpeed);
            }
        }
    }
}
