using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using LitJson;
using System.IO;
public class TestSave : MonoBehaviour
{
    int z;
    int Timing=-1;
    Quaternion ch;                  //main drone의 Quaternion rotation
    Vector3 Temp;                   //main drone의 eulerAngles
    public GameObject Ghost;        //리플레이될 드론
    string Pos;                     //Main drone의 Pos값 저장할 string
    string Rot;                     //Main drone의 Rot값 저장할 string
    public bool IsTrigger = false;
    public bool Del = false;
    StreamWriter Writer;
    int i = 0;

    void Start()
    {
        z = GameMgr.instance.fps * 3; // 프레임 카운트위함

        ch = this.transform.rotation;   
        Temp = ch.eulerAngles;          
        Rot = Temp.x + "/" + Temp.y + "/" + Temp.z + "/";   // x,y,z값을 '/'로 split하기 위해 사이사이에 저장

        Pos = Ghost.transform.position.x + "/" + Ghost.transform.position.y + "/" + Ghost.transform.position.z + "/";   //

        Writer = File.CreateText("Data.txt");
        Writer.Write(Pos);

        Writer.Write(Rot);

        Writer.Close();
        Writer = File.AppendText("Data.txt");


    }

    // Update is called once per frame
    void Update()
    {        
        if (GameMgr.instance.GameT > 3)  // 3초 딜레이 (카운트)
        {
            if (GameMgr.instance.GameT * GameMgr.instance.fps >z)
            {
                if (Timing != (int)GameMgr.instance.GameT)
                {
                    Pos = Ghost.transform.position.x + "/" + Ghost.transform.position.y + "/" + Ghost.transform.position.z + "/";
                    ch = this.transform.rotation;
                    Temp = ch.eulerAngles;
                    Rot = Temp.x + "/" + Temp.y + "/" + Temp.z + "/";
                    Writer.Write(Pos);
                    Writer.Write(Rot);
                    if (Del == true)
                        File.Delete("Data2.txt");
                    
                }
                else
                {
                    if (i == 0 && Timing == (int)GameMgr.instance.GameT)
                    {
                        Pos = Ghost.transform.position.x + "/" + Ghost.transform.position.y + "/" + Ghost.transform.position.z + "/";
                        ch = this.transform.rotation;
                        Temp = ch.eulerAngles;
                        Rot = Temp.x + "/" + Temp.y + "/" + Temp.z + "/";
                        Writer.Write(Pos);
                        Writer.Write(Rot);
                        Writer.Close();
                        File.Delete("Data2.txt");
                        File.Copy("Data.txt", "Data2.txt", true);
                        i++;
                        PlayerPrefs.SetFloat("Ghost lapTime", GameMgr.instance.GameT); // 고스트의 시간저장 
                    }
                }
                z+=2;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "1")
        {
            IsTrigger = true;
            Timing = (int)GameMgr.instance.GameT + 2;
            
            // Debug.Log("1");
        }
    }
}