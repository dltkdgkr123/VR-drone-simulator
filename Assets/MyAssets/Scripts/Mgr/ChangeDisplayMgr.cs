using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;
public class ChangeDisplayMgr : MonoBehaviour
{
    public Hand leftHand, rightHand;
    public SteamVR_Action_Boolean menuOpen;

    //사람 시점
    public Camera humanCam;
    public AudioListener humanAudioListener;
    

    //드론 시점
    public GameObject droneObj;
    Camera droneCam;
    AudioListener droneAudioListener;
    AudioSource droneSound;

    //Pannel
    public GameObject racingUIPanel;//레이싱중 UI 판넬
    public GameObject pausePanel;   //일시정지 메뉴 판넬
    public GameObject optionPanel;  //일시정지 메뉴 판넬
    public GameObject daedPanel;    //죽었을 때 UI 판넬

    void Start()
    {
        ////시작할땐 드론 시점
        //드론 시점
        droneCam = droneObj.GetComponentInChildren<Camera>();
        droneAudioListener = droneObj.GetComponentInChildren<AudioListener>();
        droneSound = droneObj.GetComponent<AudioSource>();

        droneCam.enabled = true;
        droneAudioListener.enabled = true;

        //사람 시점
        humanCam.enabled = false;
        humanAudioListener.enabled = false;
    }
    void OnEnable()
    {
        menuOpen.AddOnChangeListener(OpenMenu, leftHand.handType);
        menuOpen.AddOnChangeListener(OpenMenu, rightHand.handType);
    }

    //메뉴 열기(ChangingView, PuaseMenuPanel Open)
    void OpenMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool value)
    {
        Debug.Log("시점 변환 함수 진입");
        if (value && GameMgr.instance.racing)
        {
            menuOn();
        }
        else if(value && !GameMgr.instance.racing)
        {
            menuOff();
        }

    }

    //시점 변환
    public void ChangingView()
    {
        //카메라 우선 교체
        humanCam.enabled = !humanCam.enabled;
        droneCam.enabled = !droneCam.enabled;

        ////사람 시점
        humanAudioListener.enabled = !humanAudioListener.enabled;

        ////드론 시점
        droneAudioListener.enabled = !droneAudioListener.enabled;
    }

    public void menuOn()
    {
        droneSound.Stop();
        racingUIPanel.SetActive(false);
        pausePanel.SetActive(true);
        GameMgr.instance.racing = false;
        ChangingView(); //시점 변환  
    }
    public void menuOff()
    {
        droneSound.Play();
        pausePanel.SetActive(false);
        racingUIPanel.SetActive(true);
        GameMgr.instance.racing = true;
        ChangingView(); //시점 변환  
    }

    public void Dead()
    {
        droneSound.Stop();
        racingUIPanel.SetActive(false);
        daedPanel.SetActive(true);
        GameMgr.instance.racing = false;
        ChangingView();
    }
}