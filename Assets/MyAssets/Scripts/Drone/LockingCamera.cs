using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LockingCamera : MonoBehaviour
{
    //자식으로 존재하는 드론카메라(카메라)
    public Transform drone, droneCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        FixCameraRot();
        FixCameraPos();        
    }

    void FixCameraPos()
    {
        //로테이션 돌기전 고치기
        Vector3 fixOne = drone.position - droneCamera.localPosition;

        transform.position = fixOne;

        //로테이션 후 로테이션에 의해 이동 된 droneCamera의 global 좌표
        fixOne += drone.position - droneCamera.position;

        transform.position = fixOne;

    }
    void FixCameraRot()
    {
        float rotX, rotY, rotZ, rotW;

        rotX = droneCamera.localRotation.x;
        rotY = droneCamera.localRotation.y;
        rotZ = droneCamera.localRotation.z;
        rotW = droneCamera.localRotation.w * -1f;
        transform.localRotation = new Quaternion(rotX, rotY, rotZ, rotW);
    }
}