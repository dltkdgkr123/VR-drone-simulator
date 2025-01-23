using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGuide : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameMgr.instance.checkPoints != null)
        {
            if (GameMgr.instance.nowCheckpoint != GameMgr.instance.checkPoints.Length)
            {
                transform.LookAt(GameMgr.instance.checkPoints[GameMgr.instance.nowCheckpoint].transform);
                transform.Rotate(90, 0, 0);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
