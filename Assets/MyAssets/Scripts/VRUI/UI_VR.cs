using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VR : MonoBehaviour
{
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(cam);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
