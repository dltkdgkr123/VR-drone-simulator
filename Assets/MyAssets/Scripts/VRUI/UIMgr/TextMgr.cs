using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMgr : MonoBehaviour
{
    //Drone Object
    public GameObject drone;

    //Drone Sound Option 조절 UI
    AudioSource droneSound;
    public Text droneSoundValueTxt;
    float volume, saveVolume = 0;

    //Racing Time Text 표기 UI
    public Text racingTimeTxt;

    //Drone Speed Text 표기 UI
    Rigidbody droneRig;
    public Text droneSpeedTxt;

    
    // Start is called before the first frame update
    void Start()
    {
        //Drone Sound Setting
        droneSound = drone.GetComponent<AudioSource>();
        DroneSoundUpdate();

        //Drone Speed Text Setting
        droneRig = drone.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Racing Time Update
        RacingTimeUpdate();

        //Drone Speed Update
        DroneSoundUpdate();

        //Drone Speed Update
        DroneSpeedTexting();
    }

    //Time Text Update
    void RacingTimeUpdate()
    {
        float t = GameMgr.instance.GameT;
        t = (int)(t * 100) / 100f;  //전체 시간
        float m = (int)t / 60;      //분
        float s = t % 60;           //초
        float ms = s - (int)s;      //밀리초
        s -= ms;                    //초 - 밀리초
        ms = (int)(ms * 100);

        Debug.Log("t = " + t);
        Debug.Log("m = " + m);
        Debug.Log("s = " + s);
        Debug.Log("ms = " + ms);

        racingTimeTxt.text = "Racing Timer [";

        TimeTexting(m);

        racingTimeTxt.text += ".";

        TimeTexting(s);

        racingTimeTxt.text += ".";

        TimeTexting(ms);

        racingTimeTxt.text += "]";
    }

    //Texting Time
    void TextingTime(float t)
    {
        if (t >= 10)        //10(분,초,밀리초) 이상        
            racingTimeTxt.text += t;
        else if (t > 0)     //1(분,초,밀리초) 이상 
            racingTimeTxt.text += "0" + t;
        else                //1(분,초,밀리초) 미만
            racingTimeTxt.text += "00";
    }

    //Dorne Sound Setting 적용 및 표기
    void DroneSoundUpdate()
    {
        if (saveVolume != volume)
        {
            volume = (int)(droneSound.volume * 100) / 100f;
            droneSoundValueTxt.text = droneSound.volume.ToString();
            saveVolume = volume;
        }
    }

    //Drone Speed Text 표기
    void DroneSpeedTexting()
    {
        float speedX = droneRig.velocity.x; //현재 드론의 x축 velocity
        float speedZ = droneRig.velocity.z; //현재 드론의 z축 velocity

        Vector2 nowVelocity = new Vector2(speedX, speedZ);  //현재 velocity값을 2차원 좌표화
        float speed = Vector2.Distance(Vector2.zero, nowVelocity);  //원점에서부터 좌표화된 velocity값의 거리 측정 (절대값 측정)

        speed = (int)(speed * 100)/8f;  //가상의 속도 계산

        droneSpeedTxt.text = speed.ToString();  //Text 출력
    }
}
