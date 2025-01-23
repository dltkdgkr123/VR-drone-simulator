using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMgr : MonoBehaviour
{
    public ChangeDisplayMgr changeDisplayMgr;
    
    public void GoMainScene()
    {
        Destroy(changeDisplayMgr.droneObj);
        SceneManager.LoadScene("MainMenuScenes");
    }


    public void UnPauseGame()
    {
        changeDisplayMgr.menuOff();
    }

    public void OptionOn()
    {
        changeDisplayMgr.pausePanel.SetActive(false);  //일시정지 메뉴 판넬 off  
        changeDisplayMgr.optionPanel.SetActive(true);  //옵션 메뉴 판넬 on   
    }
    public void OptionOff()
    {
        changeDisplayMgr.optionPanel.SetActive(false);  //옵션 메뉴 판넬 off
        changeDisplayMgr.pausePanel.SetActive(true);    //일시정지 메뉴 판넬 on  
    }

    public void restart() { 
        
        SceneManager.LoadScene("MapScene");
    }
}
