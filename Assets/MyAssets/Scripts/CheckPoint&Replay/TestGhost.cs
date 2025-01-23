using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestGhost : MonoBehaviour
{
    //public GameObject Ghost;
    int z;
    int Timing = -1;
    string Pos;
    string Rot;
    string[] TmpPos;
    string[] TmpRot;
    StreamReader R;
    StreamReader P;
    int i = 0;

    //2020-10-31 추가분 작성자:김영호
    bool reading = true;

    // Start is called before the first frame update
    void Start()
    {
        z = GameMgr.instance.fps * 3;

        File.Delete("Data3.txt");

        ////////////////////
      //  File.Delete("Rot3.txt");
        //////////////////////

        File.Copy("Data2.txt", "Data3.txt", true);

        /////////////////
     //   File.Copy("Rot2.txt", "Rot3.txt", true);
        ///////////////////

        // P = new StreamReader(new FileStream("Rot2.txt", FileMode.Open));
        R = new StreamReader(new FileStream("Data3.txt", FileMode.Open));
       if (R != null)
        {
            Pos = R.ReadToEnd();
            TmpPos = Pos.Split('/');
        }

        ////////////
   //     P = new StreamReader(new FileStream("Rot3.txt", FileMode.Open));
        if (P != null)
        {
            Rot = P.ReadToEnd();
            TmpRot = Rot.Split('/');
        }

        ////////////

    }

    // Update is called once per frame
    void Update()
    {
        //2020-10-31 추가분 작성자:김영호
        //추가한 부분 if (R != null && reading)
        if (R != null && reading)
        {
            if (i >= TmpPos.Length - 1)
            {
                R.Close();
                reading = false;
                Debug.Log("ghost end");
            }
            else if (GameMgr.instance.GameT > 3)
            {
                if (GameMgr.instance.GameT * GameMgr.instance.fps > z)
                {
                    {
                        //Debug.Log("Test" + transform.position);
                        if (R != null)
                        {
                            Vector3 Position = new Vector3(float.Parse(TmpPos[i]), float.Parse(TmpPos[i + 1]), float.Parse(TmpPos[i + 2]));
                            Vector3 Rotation = new Vector3(float.Parse(TmpPos[i + 3]), float.Parse(TmpPos[i + 4]), float.Parse(TmpPos[i + 5]));
                            i += 6;

                            this.transform.position = Position;
                            this.transform.rotation = Quaternion.Euler(Rotation);
                        }
                        if (Timing == (int)GameMgr.instance.GameT)
                            R.Close();
                    }
                    z += 2;
                }
            }
        }
        
    }
        /////////////////////////

      /*  if (P != null)
        {
            Vector3 Rotation = new Vector3(float.Parse(TmpRot[i]), float.Parse(TmpRot[i + 1]), float.Parse(TmpRot[i + 2]));
            i += 3;

            this.transform.rotation = Quaternion.Euler(Rotation);
        }*/
        ///////////////////////
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
           // R.Close();
            Timing = (int)GameMgr.instance.GameT + 2;
              // Destroy(gameObject);
        }

    }
}