using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public GameObject dron;
    Rigidbody dron_Rigidbody;
    Transform dron_Transform;
    float time = 0f;
    bool stop = true;
    public bool windAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        if (windAnimation)
        {
            dron_Rigidbody = dron.GetComponent<Rigidbody>();
            dron_Transform = dron.GetComponent<Transform>();

            Wind();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (windAnimation)
        {
            time += Time.deltaTime;

            if (time >= 10)
            {
                Wind();
                time = 0f;
                stop = true;
            }
            else if (time >= 5 && stop)
            {
                dron_Rigidbody.velocity = new Vector3(0, 0, 0);
                stop = false;
            }
        }
    }

    void Wind()
    {
        dron_Rigidbody.velocity = new Vector3(0, 0, 0);
        float rand = Random.Range(-10, 11);
        dron_Rigidbody.AddForce(new Vector3(rand, 0, 0));
        dron_Transform.rotation = Quaternion.Euler(0, 0, rand / 10f);
    }
}
