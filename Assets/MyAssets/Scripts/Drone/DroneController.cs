using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DroneController : MonoBehaviour
{
    //드론 오브젝트의 Component
    public GameObject drone;
    Gyroscope gyroscope;
    Rigidbody drone_Rigidbody;
    PowerManagement powerManagement;

    public SteamVR_Action_Vector2 leftStick;    //왼쪽 컨트롤러의 터치패드의 터치 위치값을 받아올 변수
    public SteamVR_Action_Vector2 rightStick;   //오른쪽 컨트롤러의 터치패드의 터치 위치값을 받아올 변수

    //이동, 회전 속도
    float movSpeed = 10.0f;
    float udSpeed = 100.0f;
    float rotSpeed = 100.0f;

    //이전 프레임의 입력값을 저장해줄 변수
    float backFrameInputX = 0, backFrameInputY = 0;

    //입력중일때 true
    public bool inputBool;

    //각도 제한 On/Off
    public bool AngleLimite = true;

    //속도 제한 On/Off
    public bool speedLimite = true;

    //코루틴 시험 함수용 bool 변수
    bool rollbackCooltime = true;

    //인스펙터창에 표기될 각각의 로터에 적용된 AddPower
    public float helixO1_AddPowerL;
    public float helixO2_AddPowerL;
    public float helixV1_AddPowerL;
    public float helixV2_AddPowerL;

    public float helixO1_AddPowerR;
    public float helixO2_AddPowerR;
    public float helixV1_AddPowerR;
    public float helixV2_AddPowerR;

    private void Awake()
    {
        //드론 오브젝트의 Componenet 가져오기
        powerManagement = drone.GetComponent<PowerManagement>();
        drone_Rigidbody = drone.GetComponent<Rigidbody>();
        gyroscope = drone.GetComponent<Gyroscope>();

        //bool 변수들 초기화
        inputBool = false;

        movSpeed *= drone_Rigidbody.mass;
        udSpeed *= drone_Rigidbody.mass;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameMgr.instance.GameT >= 3f && GameMgr.instance.racing)
        {
            //회전속도 초기화, 스틱 입력 여부 확인
            ReadyFrame();

            //드론의 전후좌우 이동
            DorneMove();

            //드론 상승하강
            DroneUD();

            //드론 회전
            DroneRot();

            //자이로스코프 기능 활성화
            StartGyro();

            //인스펙터창에 표기된 각각의 로터에 적용된 AddPower값 갱신
            updateRotorPower();
        }
    }
       
    //매 Frame마다 검사
    void ReadyFrame()
    {
        //회전속도 초기화
        drone_Rigidbody.angularVelocity = new Vector3(drone_Rigidbody.angularVelocity.x, 0f, drone_Rigidbody.angularVelocity.z);

        //스틱 입력 확인
        if (leftStick.axis.x != 0 && leftStick.axis.y != 0)
            inputBool = true;
        else
            inputBool = false;
    }

    //전후좌우 이동
    void DorneMove()
    {
        float leftX = Mathf.Sqrt(Mathf.Pow(leftStick.axis.x, 2));
        float leftY = Mathf.Sqrt(Mathf.Pow(leftStick.axis.y, 2));
        //현제 프레임의 값이 (0, 0) 미 입력값일때
        if (leftStick.axis.x == 0 && leftStick.axis.y == 0)
        {
            powerManagement.DroneMove(0);
        }
        else
        {
            //드론 기울기
            //Debug.Log((leftX + leftY) * Time.deltaTime);
            powerManagement.DroneMove((leftX+leftY) * movSpeed * Time.deltaTime);
            drone.transform.Rotate(leftStick.axis.y, 0, -leftStick.axis.x);
        }
    }

    //상승하강
    void DroneUD()
    {
        float rightPowerY = rightStick.axis.y * udSpeed * Time.deltaTime;
        powerManagement.DronUD(rightPowerY);
        if (rightPowerY == 0)
        {
            if (drone_Rigidbody.velocity.y > 0)
                drone_Rigidbody.velocity = new Vector3(drone_Rigidbody.velocity.x, drone_Rigidbody.velocity.y - 0.05f, drone_Rigidbody.velocity.z);
            else if (drone_Rigidbody.velocity.y < 0)
                drone_Rigidbody.velocity = new Vector3(drone_Rigidbody.velocity.x, drone_Rigidbody.velocity.y + 0.05f, drone_Rigidbody.velocity.z);
        }
    }

    void DroneRot()
    {
        Vector3 dronRot = new Vector3(0, rightStick.axis.x, 0);
        if (dronRot.magnitude >= 0.1f)
        {
            drone.transform.Rotate(dronRot * rotSpeed * Time.deltaTime);
        }
    }

    void StartGyro()
    {
        if (AngleLimite && inputBool)    //각도제한 설정이 true일 때 각도제한
            gyroscope.LimiteAngle(leftStick.axis.x, leftStick.axis.y);
        if (!inputBool)     //미 입력 중일 때 수평 복원
            gyroscope.RecoverAngle();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Inspecter 출력용 로터값 업데이트
    void updateRotorPower()
    {
        //왼쪽 컨트롤러 입력에 따른 로터 추가 값들 표기
        helixO1_AddPowerL = powerManagement.helixO1.getAddPowerL();
        helixO2_AddPowerL = powerManagement.helixO2.getAddPowerL();
        helixV1_AddPowerL = powerManagement.helixV1.getAddPowerL();
        helixV2_AddPowerL = powerManagement.helixV2.getAddPowerL();
        //오른쪽 컨트롤러 입력에 따른 로터 추가 값들 표기
        helixO1_AddPowerR = powerManagement.helixO1.getAddPowerR();
        helixO2_AddPowerR = powerManagement.helixO2.getAddPowerR();
        helixV1_AddPowerR = powerManagement.helixV1.getAddPowerR();
        helixV2_AddPowerR = powerManagement.helixV2.getAddPowerR();
    }
}