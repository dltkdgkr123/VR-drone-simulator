using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeMgr : MonoBehaviour
{
    public ChangeDisplayMgr changeDisplayMgr;
    public Image[] life;
    float lifeTimer = 3f;
    bool confirmDamage = true;


    private void Update()
    {
        if (!confirmDamage)
            lifeTimer -= Time.deltaTime;

        if (lifeTimer < 0)
            confirmDamage = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            if (confirmDamage)
            {
                if (GameMgr.instance.Health > 0)
                {
                    GameMgr.instance.Health--;
                    lifeTimer = 3f;
                    confirmDamage = false;
                    life[GameMgr.instance.Health].enabled = false;
                }
                else
                {
                    GameMgr.instance.racing = false;
                    Debug.Log("죽었습니다.");
                    //changeDisplayMgr.Dead();
                }
            }
        }
    }
}