using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidebarMgr : MonoBehaviour
{
    public AudioSource droneSound;
    public Slider droneSoundSlider;

    private void Awake()
    {
        droneSoundSlider.value = droneSound.volume;
    }

    // Update is called once per frame
    public void droneSoundOption()
    {
        droneSound.volume = (int)(droneSoundSlider.value*100)/100f;
    }
}
