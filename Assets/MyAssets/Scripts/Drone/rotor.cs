using UnityEngine;
using System.Collections;

public class rotor : MonoBehaviour {
    Rigidbody rBody;
    PowerManagement powerMgr;
    public float power;
    float addPowerL = 0f;
    float addPowerR = 0f;
    
    /// <summary>
    /// Specify the verse of the rotation
    /// <para> Set this in the editor
    /// </summary>
    public bool counterclockwise = false;
    
    /// <summary>
    /// Specify is the rotors are animated or static. Just a visual effect
    /// <para> Set this in the editor
    /// </summary>
    public bool animationActivated = false;

    /// <summary>
    /// Function called before of the first update
    /// </summary>
    void Start()
    {        
        Transform t = this.transform;
        while (t.parent != null && t.tag != "Drone") t = t.parent;
        rBody = t.GetComponent<Rigidbody>();

        while (t.parent != null && t.tag != "Drone") t = t.parent;
        powerMgr = t.GetComponent<PowerManagement>();
    }

    /// <summary>
    /// Sets the rotating power of the rotor
    /// </summary>
    /// <param name="intensity"> The rotating power of the rotor </param>
    public void setPower(float intensity) { power = intensity; }

    /// <summary>
    /// Gets the rotating power of the rotor
    /// </summary>
    /// <returns>the actual rotating power of the rotor</returns>
    public float getPower() { return power; }
    
    public void setAddPowerL(float intensity) { addPowerL = intensity; }
    public float getAddPowerL() { return addPowerL; }
    public void setAddPowerR(float intensity) { addPowerR = intensity; }
    public float getAddPowerR() { return addPowerR; }

    /// <summary>
    /// Function called once per frame
    /// </summary>
    void Update() {
        if (animationActivated)
            if (powerMgr.flight)
                transform.Rotate(0, 0, (power + addPowerL + addPowerR) / rBody.mass * 1000 * Time.deltaTime * (counterclockwise ? -1 : 1));
            else            
                transform.Rotate(0, 0, (power + addPowerL + addPowerR) * 1000 * Time.deltaTime * (counterclockwise ? -1 : 1));
    }

    /// <summary>
    /// Function at regular time interval
    /// </summary>
    void FixedUpdate()
    {
        if(powerMgr.flight)
            rBody.AddForceAtPosition(transform.forward * (power + addPowerL + addPowerR), transform.position);
        //lr.SetPosition(1, new Vector3(0, 0, power / 3f));        
    }
}
