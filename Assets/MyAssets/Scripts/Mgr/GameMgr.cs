using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr: MonoBehaviour
{
    public static GameMgr instance = null;

    //Game Time(Racing Time Counter)
    public float GameT = 0;

    public int fps = 100;
    
    //Victory Check
    public int isGhost = 0; // 0 아직 미확인 , 1 고스트 , 2 드론

    //CheckPoint
    public GameObject[] checkPoints;
    public int nowCheckpoint = 0;   //현재 지나가야하는 체크포인트 ID

    //Life
    public int Health = 2; // 기본 제공 체력
    //public Slider HealthBar; 배터리 (이전버전)

    //2020-10-31 추가 작성자: 김영호
    public bool racing;    //true = 게임 진행중 / false = 게임 일시정지
    ////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        // instance.fps = 100;
        // instance.GameT = 0;

        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 newGameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 newGameMgr)을 삭제해준다.
            //Destroy(this.gameObject);
        }

        racing = true;

        //StartCoroutine(Health());배터리 (이전버전)
        //2020-10-31 추가 작성자: 김영호
        if (checkPoints.Length != 0)
            SettingCheckpointID();
        ////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        if (racing)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;

        GameT += Time.deltaTime;
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static GameMgr Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
   
    //GameObject Type의 배열에 들어가있는 Checkpoint에 순서대로 checkpointID값 설정
    void SettingCheckpointID()
    {
        
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].transform.GetComponent<Checkpoint>().checkpointID = i;
        }
    }
    
    //Call Ghost Record
    float GetGhostLapTime()
    {
        return PlayerPrefs.GetFloat("Ghost lapTime");
    }
    
}