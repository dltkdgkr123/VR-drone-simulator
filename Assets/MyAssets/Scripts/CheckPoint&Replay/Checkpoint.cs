using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //각각의 Checkpoint Object에 들어감 Game Manager에서 현재 들어가야하는 체크포인트 검사(따로 체크포인트 검사 전용 Manager 제작해도 무관)
    public int checkpointID = 0;
    private void OnTriggerEnter(Collider drone)
    {
        GameMgr gameMgr = GameMgr.instance;
        if (drone.tag == "Drone")
        {
            float TimeF = gameMgr.GameT;
            Debug.Log("T!! =" + TimeF);

            if (gameMgr.nowCheckpoint == checkpointID)
            {
                Debug.Log("체크포인트 도달");
                gameMgr.checkPoints[checkpointID].SetActive(false);
                gameMgr.nowCheckpoint++;
                if (gameMgr.checkPoints.Length > gameMgr.nowCheckpoint + 1)
                    gameMgr.checkPoints[gameMgr.nowCheckpoint + 1].SetActive(true);
                else if (gameMgr.checkPoints.Length > gameMgr.nowCheckpoint) { }    //아무것도 안하기 위함
                else
                    gameMgr.racing = false;
            }
        }

        // 추가요구된 우승판정을 위한 두가지 조건문 1패배 2우승 
        if (gameMgr.checkPoints.Length - 1 == gameMgr.nowCheckpoint)  // 최종 목적지인지 판별.
        {
            if (drone.tag == "Ghost")
            {
                gameMgr.isGhost = 1; //수정필요  최종 목적지인가 체크.

            }
            else if (drone.tag == "Drone")
            {
                gameMgr.isGhost = 2;
            }
        }
    }

}